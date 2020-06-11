using TMPro.Examples;
using UnityEditor.Hardware;
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
    public Transform ChecaChao;
    public Transform ChecaChao2;
    public Transform ChecaChao3;
    public Transform ChecaChao4;
    public Transform ChecaChao5;


    GameObject caixa;

    bool noNorte;
    bool noSul;
    bool noOeste;
    bool noLeste;
    bool insideMe;
    bool collided;
    bool noChao;
    bool noChao2;
    bool noChao3;
    bool noChao4;
    bool noChao5;
    bool Threw;

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
        noChao = Physics.Linecast(gameObject.transform.position, ChecaChao.position);
        noChao2 = Physics.Linecast(gameObject.transform.position, ChecaChao2.position);
        noChao3 = Physics.Linecast(gameObject.transform.position, ChecaChao3.position);
        noChao4 = Physics.Linecast(gameObject.transform.position, ChecaChao4.position);
        noChao5 = Physics.Linecast(gameObject.transform.position, ChecaChao5.position);
        

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
            GameObject obj = GameObject.FindGameObjectWithTag("charge");
            obj.GetComponent<Animator>().SetBool("charge", true);

            PlayerEntity.getBoxLocked().GetComponentInParent<boxMovement>().enabled = true;
            PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().isKinematic = false;
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
            GameObject obj = GameObject.FindGameObjectWithTag("charge");
            obj.GetComponent<Animator>().SetBool("charge", false);
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
            PlayerEntity.setThrewTheBox(false);
            GameObject obj = GameObject.FindGameObjectWithTag("charge");
            obj.GetComponent<Animator>().SetBool("charge", false);

        }      

        if (insideMe)
        {
            if (PlayerEntity.getWantToThrow())
            {
                //Debug.Log(colidiu);
                FindObjectOfType<AudioManager>().stopAll();
                FindObjectOfType<AudioManager>().Play("throw");
                if (this.gameObject.GetComponent<LockB>().movimento == DirecaoForca.normal)
                {

                    if (PlayerEntity.getIsLockedInNorth())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.right * 2500 * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInSouth())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.right * 2500 * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInWest())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.forward * 2500 * Time.deltaTime;
                    }
                    if (PlayerEntity.getIsLockedInEast())
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.forward * 2500 * Time.deltaTime;
                    }
                    PlayerEntity.setWantToThrow(false);
                    PlayerEntity.setThrewTheBox(true);
                }
                else
                {
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().useGravity = false;
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.up * 2500 * Time.deltaTime;
                    PlayerEntity.setWantToThrow(false);
                    PlayerEntity.setThrewTheBox(true);
                }
                Threw = true;
            }
        } 
        //if(Threw && (noChao || noChao2 || noChao3 || noChao4 || noChao5) && collided)
        //{
        //    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //    this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        //    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    Threw = false;
        //}

    }

    //verificar aqui se o player está dentro dela

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("paraBloco"))
        {
            if ((noChao || noChao2 || noChao3 || noChao4 || noChao5))
            {
                
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Threw = false;                
            }
            else
            {
                //this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Threw = false;
            }
           
        }

        //if (Threw && gameObject.GetComponent<Rigidbody>().velocity.y == 0)
        //{
        //    if ((noChao || noChao2 || noChao3 || noChao4 || noChao5))
        //    {
        //        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        //        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        Threw = false;
        //    }
        //}
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
}
