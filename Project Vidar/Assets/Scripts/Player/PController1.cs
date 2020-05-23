using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController1 : MonoBehaviour
{
    float strength;

    GameObject caixa = null;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerEntity.setIdle(true);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {        

        if (caixa != null)
        {
            if(PlayerEntity.getIsInside() == true && PlayerEntity.getKeyE() == true && PlayerEntity.getLocked() == false)
            {                
                PlayerEntity.setWantToLock(true);               
            }
            if(PlayerEntity.getIsInside() == true && PlayerEntity.getKeyE() == true && PlayerEntity.getLocked() == true)
            {               
                PlayerEntity.setWantToLock(false);
            }
            
        }

        PlayerStatus();
    }

    public void PlayerStatus()
    {
        //if (PlayerEntity.getIdle() == true)
        //{
        //    PlayerEntity.setLocked(false);
        //    GetComponent<Movement>().enabled = true;
        //    GetComponent<playerJump>().enabled = true;
        //    GetComponent<Dash>().enabled = true;
        //    GetComponent<RaycastShoot>().enabled = true;
        //    rb.isKinematic = false;            
        //}

        if (PlayerEntity.getLocked() == true)
        {
            PlayerEntity.setIdle(false);
            GetComponent<Movement>().enabled = false;
            GetComponent<playerJump>().enabled = false;
            GetComponent<Dash>().enabled = false;
            GetComponent<RaycastShoot>().enabled = false;
            rb.isKinematic = true;

            transform.position = Vector3.Lerp(transform.position, PlayerEntity.getPositionToLock().position, Time.deltaTime * 5f);
            if (Mathf.Abs(Vector3.Distance(transform.position, PlayerEntity.getPositionToLock().position)) < 1f)
            {
                Debug.Log("TRUE");
                transform.position = PlayerEntity.getPositionToLock().position;
                //bool aqui
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {
            caixa = other.gameObject;
            PlayerEntity.setBoxLocked(caixa);
            PlayerEntity.setIsInside(true);
            PlayerEntity.setIsInsideOfContinuous(true);
        }

        if (other.gameObject.CompareTag("SimpleBox"))
        {
            caixa = other.gameObject;
            PlayerEntity.setBoxLocked(caixa);
            PlayerEntity.setIsInside(true);
            PlayerEntity.setIsInsideOfSimple(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {            
            PlayerEntity.setIsInsideOfContinuous(false);
        }

        if (other.gameObject.CompareTag("SimpleBox"))
        {            
            PlayerEntity.setIsInsideOfSimple(false);
        }
    }
}
