using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class LockB : MonoBehaviour
{
    //public Transform playerTransform;
    public Transform playerGameObject;
    public Transform ponto1;
    public Transform ponto2;
    public Transform ponto3;
    public Transform ponto4;
    public Transform FaceNorte;
    public Transform FaceSul;
    public Transform FaceOeste;
    public Transform FaceLeste;

    GameObject caixa;

    bool noNorte;
    bool noSul;
    bool noOeste;
    bool noLeste;
    bool insideMe;

    public LayerMask Player;

    public enum DirecaoForca
    {
        normal,
        cima
    };

    //boxMovement boxMove;
    //Rigidbody rb;

    public DirecaoForca movimento;

    // Start is called before the first frame update
    private void Awake()
    {
        playerGameObject = GameObject.FindWithTag("Player").transform.parent;
    }

    // Update is called once per frame
    void Update()
    {        
        noNorte = Physics.Linecast(ponto3.position, ponto4.position, Player);
        noSul = Physics.Linecast(ponto1.position, ponto2.position, Player);
        noOeste = Physics.Linecast(ponto2.position, ponto4.position, Player);
        noLeste = Physics.Linecast(ponto1.position, ponto3.position, Player);

        if (noNorte)
        {
            PlayerEntity.setIslockedInNorth(true);
            PlayerEntity.setIslockedInSouth(false);
            PlayerEntity.setIslockedInWest(false);
            PlayerEntity.setIslockedInEast(false);
            PlayerEntity.setPositionToLock(FaceNorte);
        }
        else if (noSul)
        {
            PlayerEntity.setIslockedInNorth(false);
            PlayerEntity.setIslockedInSouth(true);
            PlayerEntity.setIslockedInWest(false);
            PlayerEntity.setIslockedInEast(false);
            PlayerEntity.setPositionToLock(FaceSul);
        }
        else if (noOeste)
        {
            PlayerEntity.setIslockedInNorth(false);
            PlayerEntity.setIslockedInSouth(false);
            PlayerEntity.setIslockedInWest(true);
            PlayerEntity.setIslockedInEast(false);
            PlayerEntity.setPositionToLock(FaceOeste);
        }
        else if (noLeste)
        {
            PlayerEntity.setIslockedInNorth(false);
            PlayerEntity.setIslockedInSouth(false);
            PlayerEntity.setIslockedInWest(false);
            PlayerEntity.setIslockedInEast(true);
            PlayerEntity.setPositionToLock(FaceLeste);
        }
        else
        {
            // FIX ME
            // Box face is memoized on the next interection
            return;
        }

        if (PlayerEntity.getWantToLock() == true && PlayerEntity.getLocked() == false)
        {
            FindObjectOfType<AudioManager>().stopAll();
            playerGameObject.SetParent(PlayerEntity.getBoxLocked().transform);
            PlayerEntity.setLocked(true);
            AnimatorManager.setStateChanneling();
            FindObjectOfType<AudioManager>().Play("channeling");
            GameObject obj = GameObject.FindGameObjectWithTag("charge");
            obj.GetComponent<Animator>().SetBool("charge", true);

            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = true;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().isKinematic = false;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            if (PlayerEntity.getIsInsideOfContinuous() == true)
            {
                PlayerEntity.setIsLockedInContinuous(true);
            }

            if (PlayerEntity.getIsInsideOfSimple() == true)
            {
                PlayerEntity.setIsLockedInSimple(true);
            }

        }

        if (PlayerEntity.getWantToLock() == false && PlayerEntity.getLocked() == true)
        {

            playerGameObject.SetParent(null);
            PlayerEntity.setLocked(false);
            GameObject obj = GameObject.FindGameObjectWithTag("charge");
            obj.GetComponent<Animator>().SetBool("charge", false);
            FindObjectOfType<AudioManager>().stopAll();
            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = false;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().isKinematic = true;
            PlayerEntity.setIsInsideOfSimple(false);
            PlayerEntity.setIsInsideOfContinuous(false);
            if (PlayerEntity.getIsInsideOfContinuous() == false)
            {
                PlayerEntity.setIsLockedInContinuous(false);
            }

            if (PlayerEntity.getIsInsideOfSimple() == false)
            {
                PlayerEntity.setIsLockedInSimple(false);
            }
        }

        if (PlayerEntity.getLocked() == true && PlayerEntity.getThrewTheBox() == true)
        {

            playerGameObject.SetParent(null);
            PlayerEntity.setLocked(false);
            PlayerEntity.setWantToThrow(false);
            PlayerEntity.setWantToLock(false);
            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = false;
            PlayerEntity.setThrewTheBox(false);
            GameObject obj = GameObject.FindGameObjectWithTag("charge");
            obj.GetComponent<Animator>().SetBool("charge", false);

        }

        if (insideMe == true)
        {
            if (PlayerEntity.getWantToThrow() == true)
            {
                FindObjectOfType<AudioManager>().stopAll();
                FindObjectOfType<AudioManager>().Play("throw");
                if (this.gameObject.GetComponent<LockB>().movimento == DirecaoForca.normal)
                {

                    if (PlayerEntity.getIsLockedInNorth())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.right * 5000 * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInSouth())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.right * 5000 * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInWest())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.forward * 5000 * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInEast())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.forward * 5000 * Time.deltaTime;
                    }
                    PlayerEntity.setWantToThrow(false);
                    PlayerEntity.setThrewTheBox(true);
                }
                else
                {
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.up * 5000 * Time.deltaTime;
                    PlayerEntity.setWantToThrow(false);
                    PlayerEntity.setThrewTheBox(true);
                }
            }
        }
    }

    //verificar aqui se o player está dentro dela

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !PlayerEntity.getIsInside())
        {
            caixa = gameObject;
            PlayerEntity.setBoxLocked(caixa);
            insideMe = true;
            PlayerEntity.setIsInside(true);
            if (caixa.CompareTag("ContinuosBox"))
            {
                PlayerEntity.setIsInsideOfContinuous(true);
            }
            if (caixa.CompareTag("SimpleBox"))
            {
                PlayerEntity.setIsInsideOfSimple(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && caixa != null)
        {
            caixa = null;
            insideMe = false;
            PlayerEntity.setBoxLocked(caixa);
            PlayerEntity.setIsInside(false);
        }
    }
}
