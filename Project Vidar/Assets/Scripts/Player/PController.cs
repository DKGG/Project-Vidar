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
            if (PlayerEntity.getIsInside() && PlayerEntity.getKeyE() && !PlayerEntity.getLocked() && !PlayerEntity.getBoxLocked().GetComponentInChildren<FreezableBox>().isFrozen)
            {
                PlayerEntity.setWantToLock(true);
            }

            if (PlayerEntity.getIsInside() && PlayerEntity.getKeyE() && PlayerEntity.getLocked())
            {
                PlayerEntity.setWantToLock(false);
            }

            if (PlayerEntity.getLocked() && PlayerEntity.getIsLockedInContinuous() && PlayerEntity.getKeyQ())
            {
                PlayerEntity.setWantToThrow(true);
            }
        }

        PlayerStatus();
    }

    public void PlayerStatus()
    {
        if (PlayerEntity.getLocked())
        {
            DisableMovement();

            rb.isKinematic = true;

            transform.position = Vector3.Lerp(transform.position, PlayerEntity.getPositionToLock().position, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, PlayerEntity.getPositionToLock().rotation, Time.deltaTime * 5f);

            if (Mathf.Abs(Vector3.Distance(transform.position, PlayerEntity.getPositionToLock().position)) < 0.1f)
            {
                transform.position = PlayerEntity.getPositionToLock().position;
            }

            return;
        }
        else if(PlayerEntity.getIsOnDialogue())
        {
            DisableMovement();
        }

        EnableMovement();

        rb.isKinematic = false;
    }

    private void DisableMovement()
    {
        //PlayerEntity.setIdle(false);
        GetComponent<Movement>().enabled = false;
        GetComponent<playerJump>().enabled = false;
        GetComponent<Dash>().enabled = false;
        GetComponent<RaycastShoot>().enabled = false;
    }

    private void EnableMovement()
    {
        PlayerEntity.setLocked(false);
        GetComponent<Movement>().enabled = true;
        GetComponent<playerJump>().enabled = true;
        GetComponent<Dash>().enabled = true;
        GetComponent<RaycastShoot>().enabled = true;
    }
}
