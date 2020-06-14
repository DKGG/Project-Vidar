using System.Collections;
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

    public BoxCollider b1;

    public float pushSpeed;


    GameObject caixa;

    bool noNorte;
    bool noSul;
    bool noOeste;
    bool noLeste;
    bool insideMe;
    bool collided;    
    bool Threw;
    public static bool noChao;

    public LayerMask Player;
    private AlphaShaderAnimation shader;
    private MeshRenderer chargeMesh;

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
        shader = GameObject.FindWithTag("charge").GetComponent<AlphaShaderAnimation>();
        chargeMesh = GameObject.FindWithTag("charge").GetComponent<MeshRenderer>();

        shader.spellUp = false;
        shader.spellDown = true;
        chargeMesh.enabled = false;
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
            // Box face is memorized on the next interection
            return;
        }       

        //if (!PlayerEntity.getBoxLocked().GetComponent<FreezableBox>().isFrozen){ }

        if (PlayerEntity.getWantToLock() && !PlayerEntity.getLocked())
        {
            FindObjectOfType<AudioManager>().stopAll();            
            playerGameObject.SetParent(PlayerEntity.getBoxLocked().transform);
            PlayerEntity.setLocked(true);            
            AnimatorManager.setStateChanneling();
            FindObjectOfType<AudioManager>().Play("channeling");
            StartCoroutine(ToggleChargeAnimation());

            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = true;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().isKinematic = false;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = true;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            if (PlayerEntity.getIsInsideOfContinuous())
            {
                PlayerEntity.setIsLockedInContinuous(true);
            }

            if (PlayerEntity.getIsInsideOfSimple())
            {
                PlayerEntity.setIsLockedInSimple(true);
            }
        }

        if (!PlayerEntity.getWantToLock() && PlayerEntity.getLocked())
        {

            playerGameObject.SetParent(null);
            PlayerEntity.setLocked(false);
            StartCoroutine(ToggleChargeAnimation());
            FindObjectOfType<AudioManager>().stopAll();
            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = false;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().isKinematic = true;
            PlayerEntity.setIsInsideOfSimple(false);
            PlayerEntity.setIsInsideOfContinuous(false);
            if (!PlayerEntity.getIsInsideOfContinuous() )
            {
                PlayerEntity.setIsLockedInContinuous(false);
            }

            if (!PlayerEntity.getIsInsideOfSimple())
            {
                PlayerEntity.setIsLockedInSimple(false);
            }
        }

        if (PlayerEntity.getLocked() && PlayerEntity.getThrewTheBox())
        {

            playerGameObject.SetParent(null);
            PlayerEntity.setLocked(false);
            PlayerEntity.setWantToThrow(false);
            PlayerEntity.setWantToLock(false);
            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = false;
            StartCoroutine(ToggleChargeAnimation());

        }

        if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            PlayerEntity.setThrewTheBox(false);
        }

    }

    private void FixedUpdate()
    {
        if (insideMe)
        {
            if (PlayerEntity.getWantToThrow())
            {                
                FindObjectOfType<AudioManager>().stopAll();
                FindObjectOfType<AudioManager>().Play("throw");
                if (this.gameObject.GetComponent<LockB>().movimento == DirecaoForca.normal)
                {                  

                    if (PlayerEntity.getIsLockedInNorth())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.right * pushSpeed * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInSouth())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.right * pushSpeed * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInWest())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.forward * pushSpeed * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInEast())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.forward * pushSpeed * Time.deltaTime;
                    }
                    PlayerEntity.setWantToThrow(false);
                    PlayerEntity.setThrewTheBox(true);
                }
                else
                {
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.up * pushSpeed * Time.deltaTime;
                    PlayerEntity.setWantToThrow(false);
                    PlayerEntity.setThrewTheBox(true);
                }
                Threw = true;
            }
        }
    }

    //verificar aqui se o player está dentro dela

    private void OnCollisionEnter(Collision collision)
    {      

        if (collision.gameObject.CompareTag("grass") && BoxRespawn.Respawning == false)
        {
            noChao = true;
            
        }

        if(collision.gameObject.CompareTag("paraBloco") && !noChao)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        else if(collision.gameObject.CompareTag("paraBloco") && noChao)
        {
            if (PlayerEntity.getLocked())
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            else
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("grass") && BoxRespawn.Respawning == false && PlayerEntity.getThrewTheBox())
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("grass"))
        {
            noChao = false;            
        }        
    }


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

    IEnumerator ToggleChargeAnimation()
    {
        chargeMesh.enabled = !chargeMesh.enabled;
        shader.spellDown = !shader.spellDown;
        shader.spellUp = !shader.spellUp;
        yield return null;
    }

}
