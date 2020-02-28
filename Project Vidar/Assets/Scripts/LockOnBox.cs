using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnBox : MonoBehaviour
{

    [SerializeField] directionForceApply dirForce;
    public Transform checaChao;
    public Transform checaChaoCenter;
    public Transform playerAsChild;
    public Transform ponto1;
    public Transform ponto2;
    public Transform ponto3;
    public Transform ponto4;
    public Transform FaceNorte;
    public Transform FaceSul;
    public Transform FaceOeste;
    public Transform FaceLeste;

    bool isLocked = false;
    bool readyToPush = false;
    bool veloContinua;//
    bool colidiu = false;
    bool isInside = false;//
    bool isGrounded = true;
    bool noNorte;
    bool noSul;
    bool noOeste;
    bool noLeste;

    public LayerMask Player;

    Vector3 wayToGo;
    GameObject player;

    Rigidbody rb;
    InputController inputController;
    PlayerController playerController;
    boxMovement boxMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        inputController = player.GetComponent<InputController>();
        playerController = player.GetComponent<PlayerController>();
        boxMovement = GetComponent<boxMovement>();

    }

    //arrumar como ta funcionando a ativação do lock na caixa e arrumar como os objetos estão sendo desativados.

    private void Update()
    {
        isInside = playerController.CheckPlayerStates("isInside");

        isGrounded = Physics.Linecast(checaChaoCenter.position, checaChao.position);
        noNorte = Physics.Linecast(ponto1.position, ponto4.position, Player);
        noSul = Physics.Linecast(ponto2.position, ponto3.position, Player);
        noOeste = Physics.Linecast(ponto1.position, ponto2.position, Player);
        noLeste = Physics.Linecast(ponto3.position, ponto4.position, Player);
        //Debug.Log("Ele está dentro: "+isInside); 


        #region controlador lock
        //ele não tem que verificar se os inputs foram apertados aqui tem que ser no playerController
        //ele só tem que saber que ele tem que lockar
        if (inputController.CheckInputE() && !isLocked && isInside)
        {
            isLocked = true;
        }
        else if (inputController.CheckInputE() && isLocked == true && isInside)
        {
            isLocked = false;
        }
        #endregion
        if (isGrounded)
        {
            colidiu = false;

        }

        //ele não tem que verificar se os inputs foram apertados aqui tem que ser no playerController
        if (veloContinua)//se bater no objeto
        {
            if (colidiu == false)
            {
                rb.velocity = wayToGo * 5000 * Time.deltaTime;
            }
            else
            {

                if (Mathf.Abs(rb.velocity.x) < 1 || Mathf.Abs(rb.velocity.y) < 1 || Mathf.Abs(rb.velocity.z) < 1)
                {
                    Vector3 parar = new Vector3(0, Physics.gravity.y, 0);
                    rb.velocity = parar;
                    if (Mathf.Abs(rb.velocity.x) < 1 && Mathf.Abs(rb.velocity.y) < 1 && Mathf.Abs(rb.velocity.z) < 1)
                    {
                        rb.velocity = Vector3.zero;
                    }

                }

            }
        }

        
        if (isLocked == true && isInside)
        {
            /*player.*/transform.parent = transform;
            //não desligar o código aqui desligar no player
            playerController.CheckScripts(false, "Movement");
            playerController.CheckScripts(false, "playerJump");
            //desligar o código de dash quando ele estiver lockado na caixa
            boxMovement.enabled = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            if (noNorte == true)
            {
                playerAsChild.transform.position = FaceNorte.position;                
                //rb.velocity = Vector3.zero;
            }
            if (noSul == true)
            {
                playerAsChild.transform.position = FaceSul.position;
                //rb.velocity = Vector3.zero;
            }
            if (noOeste == true)
            {
                playerAsChild.transform.position = FaceOeste.position;
                //transform.position = FaceOeste.position;
                //rb.velocity = Vector3.zero;
            }
            if (noLeste == true)
            {
                playerAsChild.transform.position = FaceLeste.position;
                //rb.velocity = Vector3.zero;
            }

            #region
            //ele não tem que verificar se os inputs foram apertados aqui tem que ser no playerController
            if (inputController.CheckInputQ())
            {
                // forceCounter += 0.1f;
                readyToPush = true;
                // streghtForce += 100;

            }
            if (!inputController.CheckInputQ() && readyToPush == true)
            {
                //transform.DetachChildren();
                playerAsChild.parent = null;

                boxMovement.enabled = false;
                //não ligar o código aqui desligar no player
                playerController.CheckScripts(true, "Movement");
                playerController.CheckScripts(true, "playerJump");
                StartCoroutine(timeToApplyForce());
            }
            #endregion

        }
        if (isLocked == false && isInside)
        {
            player.GetComponent<Movement>().enabled = true;
            player.GetComponent<playerJump>().enabled = true;
            //ligar o código de dash quando ele sair do lock
            this.gameObject.GetComponent<boxMovement>().enabled = false;
            //transform.DetachChildren();
            playerAsChild.parent = null;
            if (Mathf.Abs(rb.velocity.x) < 1 && Mathf.Abs(rb.velocity.y) < 1 && Mathf.Abs(rb.velocity.z) < 1)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.velocity = Vector3.zero;
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("paraBloco"))
        {
            colidiu = true;
            veloContinua = false;
            //readyToPush = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("paraBloco"))
        {
            colidiu = false;
            //veloContinua = true;

        }
    }

    IEnumerator timeToApplyForce()
    {
        yield return new WaitForSeconds(0.6f);
        switch (dirForce)
        {
            //exemplo de como fazer a velocidade continua
            case directionForceApply.foward:
                veloContinua = true;
                wayToGo = -transform.forward;
                break;
            //exemplo de como fazer a velocidade aumentar e diminuir dps de um tempo
            case directionForceApply.up:
                //rb.velocity = Vector3.up * streghtForce * Time.deltaTime;
                veloContinua = true;
                wayToGo = transform.up;
                break;

        }

        yield return new WaitForSeconds(0.6f);
        readyToPush = false;
        isLocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Saiu");

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
        }
    }

    enum directionForceApply
    {

        foward,
        up,
    };


}
