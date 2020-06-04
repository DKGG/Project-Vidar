using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PController : MonoBehaviour
{  
    GameObject caixa = null;    
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {


        if (PlayerEntity.getBoxLocked() != null)
        {
            if (PlayerEntity.getIsInside() == true && PlayerEntity.getKeyE() == true && PlayerEntity.getLocked() == false)
            {
                PlayerEntity.setWantToLock(true);
            }
            if (PlayerEntity.getIsInside() == true && PlayerEntity.getKeyE() == true && PlayerEntity.getLocked() == true)
            {
                PlayerEntity.setWantToLock(false);
            }

            if (PlayerEntity.getLocked() == true && PlayerEntity.getIsLockedInContinuous() == true && PlayerEntity.getKeyQ() == true)
            {
                PlayerEntity.setWantToThrow(true);
            }

        }

        PlayerStatus();
    }

    public void PlayerStatus()
    {
        if (PlayerEntity.getLocked() == true)
        {
            //PlayerEntity.setIdle(false);
            GetComponent<Movement>().enabled = false;
            GetComponent<playerJump>().enabled = false;
            GetComponent<Dash>().enabled = false;
            GetComponent<RaycastShoot>().enabled = false;
            rb.isKinematic = true;

            transform.position = Vector3.Lerp(transform.position, PlayerEntity.getPositionToLock().position, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, PlayerEntity.getPositionToLock().rotation, Time.deltaTime * 5f);
            if (Mathf.Abs(Vector3.Distance(transform.position, PlayerEntity.getPositionToLock().position)) < 0.1f)
            {
                transform.position = PlayerEntity.getPositionToLock().position;
            }
        }
        else if (PlayerEntity.getIsOnDialogue())
        {
            GetComponent<Movement>().enabled = false;
            GetComponent<playerJump>().enabled = false;
            GetComponent<Dash>().enabled = false;
            GetComponent<RaycastShoot>().enabled = false;
            rb.isKinematic = false;
        }
        else
        {
            PlayerEntity.setLocked(false);
            GetComponent<Movement>().enabled = true;
            GetComponent<playerJump>().enabled = true;
            GetComponent<Dash>().enabled = true;
            GetComponent<RaycastShoot>().enabled = true;
            rb.isKinematic = false;
        }

    }
}
