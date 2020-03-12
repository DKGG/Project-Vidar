using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBox : MonoBehaviour
{
    enum directionForce
    {
        up,
        normal,
    }

    public Transform checaChaoCenter;
    public Transform checaChao;

    [SerializeField] directionForce dirf;

    bool colidiu = false;
    bool isGrounded;
    public bool push = false;    
    public float strength;

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
        Debug.Log("strength no throwBox" + strength);
        isGrounded = Physics.Linecast(checaChaoCenter.position, checaChao.position);
        if (push==true)
        {
            ApplyForce();
        }

    }

    private void ApplyForce()
    {
        push = false;
        //if (push == true)
        //{
            if (dirf.Equals(directionForce.normal))
            {
                switch (lockSide)
                {
                    case "norte":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = transform.right * strength /* *Time.deltaTime*/;
                        break;
                    case "sul":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = -transform.right * strength /* *Time.deltaTime*/;
                        break;
                    case "oeste":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = -transform.forward * strength /* *Time.deltaTime*/;
                        break;
                    case "leste":
                        rb.constraints = RigidbodyConstraints.None;
                        rb.constraints = RigidbodyConstraints.FreezeRotation;
                        rb.velocity = transform.forward * strength /** Time.deltaTime*/;
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
                rb.velocity = transform.up * 50  /* * Time.deltaTime*/;
            }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
