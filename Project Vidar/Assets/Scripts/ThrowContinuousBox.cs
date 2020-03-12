using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowContinuousBox : MonoBehaviour
{
    enum directionForce
    {
        up,
        normal,
    }

    public Transform checaChaoCenter;
    public Transform checaChao;

    [SerializeField] directionForce dirf;

    bool readyToPush = false;
    bool colidiu = false;
    bool isGrounded;
    public bool push = false;

    public string lockSide = "";

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lockSide = GetComponent<LockB>().side;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Push é: " + push);
        //Debug.Log(colidiu);
        //Debug.Log(lockSide);
        isGrounded = Physics.Linecast(checaChaoCenter.position, checaChao.position);

        if (push == true && colidiu == false)
        {
            if (dirf.Equals(directionForce.normal))
            {
                switch (lockSide)
                {
                    case "norte":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = transform.right * 5000 * Time.deltaTime;
                        break;
                    case "sul":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = -transform.right * 5000 * Time.deltaTime;
                        break;
                    case "oeste":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = -transform.forward * 5000 * Time.deltaTime;
                        break;
                    case "leste":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = transform.forward * 5000 * Time.deltaTime;
                        break;
                    case "":
                        break;
                    default:
                        break;

                }
            }

            if (dirf.Equals(directionForce.up))
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = transform.up * 5000 * Time.deltaTime;
            }
        }
        if (colidiu == true)
        {
            if (Mathf.Abs(rb.velocity.x) < 1 || Mathf.Abs(rb.velocity.y) < 1 || Mathf.Abs(rb.velocity.z) < 1)
            {
                Vector3 parar = new Vector3(0, Physics.gravity.y, 0);
                rb.velocity = parar;
                if (Mathf.Abs(rb.velocity.x) < 1 && Mathf.Abs(rb.velocity.y) < 1 && Mathf.Abs(rb.velocity.z) < 1)
                {
                    rb.velocity = Vector3.zero;
                    push = false;
                }

            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("paraBloco"))
        {
            colidiu = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colidiu = false;
        }
    }
}
