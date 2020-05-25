using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowContinuousBox1 : MonoBehaviour
{
  
    public enum DirectionForce
    {
        normal,
        up
    };

    
    bool colidiu;

    [SerializeField] DirectionForce dirf;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dirf);
        if (PlayerEntity.getWantToThrow() == true && colidiu == false)
        {

            switch (dirf)
            {
                case DirectionForce.normal:
                    Debug.Log("Entrei no caso normal");
                    PlayerEntity.setWantToThrow(false);
                    break;
                case DirectionForce.up:
                    Debug.Log("Entrei no caso up");
                    PlayerEntity.setWantToThrow(false);
                    break;
                default:
                    break;
            } 

            //if (dirf.Equals(directionForce.normal))
            //{
            //    if (PlayerEntity.getIsLockedInNorth())
            //    {
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.right * 5000 * Time.deltaTime;
            //    }
            //    if (PlayerEntity.getIsLockedInSouth())
            //    {
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.right * 5000 * Time.deltaTime;
            //    }
            //    if (PlayerEntity.getIsLockedInWest())
            //    {
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.forward * 5000 * Time.deltaTime;
            //    }
            //    if (PlayerEntity.getIsLockedInEast())
            //    {
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = -transform.forward * 5000 * Time.deltaTime;
            //    }
            //}

            //if (dirf.Equals(directionForce.up))
            //{
            //    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            //    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = transform.up * 5000 * Time.deltaTime;
            //}

            if (colidiu == true)
            {
                if (Mathf.Abs(PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity.x) < 1 || Mathf.Abs(PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity.y) < 1 || Mathf.Abs(PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity.z) < 1)
                {
                    Vector3 parar = new Vector3(0, Physics.gravity.y, 0);
                    PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = parar;
                    if (Mathf.Abs(PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity.x) < 1 && Mathf.Abs(PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity.y) < 1 && Mathf.Abs(PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity.z) < 1)
                    {
                        PlayerEntity.getBoxLocked().GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
                        PlayerEntity.setWantToThrow(false);
                    }

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
