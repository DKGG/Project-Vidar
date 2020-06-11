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

    bool isGrounded;
    public bool push = false;
    public float strength;

    public string lockSide = "";

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //lockSide = GetComponent<LockB>().side;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("strength no throwBox" + strength);
        isGrounded = Physics.Linecast(checaChaoCenter.position, checaChao.position);
        if (push == true)
        {
            ApplyForce();
        }

    }

    private void ApplyForce()
    {
        push = false;
        if (dirf.Equals(directionForce.normal))
        {

            if (PlayerEntity.getIsLockedInNorth())
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = transform.right * strength;                
            }
            if (PlayerEntity.getIsLockedInSouth())
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = -transform.right * strength;
            }
            if (PlayerEntity.getIsLockedInWest())
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = -transform.forward * strength;
            }
            if (PlayerEntity.getIsLockedInEast())
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.velocity = transform.forward * strength;
            }


            //switch (lockSide)
            //{
            //    case "norte":
            //        rb.constraints = RigidbodyConstraints.None;
            //        rb.constraints = RigidbodyConstraints.FreezeRotation;
            //        rb.velocity = transform.right * strength;
            //        break;
            //    case "sul":
            //        rb.constraints = RigidbodyConstraints.None;
            //        rb.constraints = RigidbodyConstraints.FreezeRotation;
            //        rb.velocity = -transform.right * strength;
            //        break;
            //    case "oeste":
            //        rb.constraints = RigidbodyConstraints.None;
            //        rb.constraints = RigidbodyConstraints.FreezeRotation;
            //        rb.velocity = -transform.forward * strength;
            //        break;
            //    case "leste":
            //        rb.constraints = RigidbodyConstraints.None;
            //        rb.constraints = RigidbodyConstraints.FreezeRotation;
            //        rb.velocity = transform.forward * strength;
            //        break;
            //    case "":
            //        break;
            //    default:
            //        break;

            //}
        }

        if (dirf.Equals(directionForce.up))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = transform.up * strength;
        }

    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //}

    //private void OnCollisionExit(Collision collision)
    //{

    //}
}
