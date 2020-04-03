using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    float strength;

    GameObject caixa = null;
    Rigidbody rb;

    void Start()
    {
        PlayerEntity.setIdle(true);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {       
        #region interacaoCaixas
        if (caixa != null)
        {
            if (PlayerEntity.getIsInside() == true && PlayerEntity.getKeyE() == true && PlayerEntity.getIdle() == true)
            {
                caixa.GetComponent<LockB>().locka = true;
            }
            else if (PlayerEntity.getIsInside() == true && PlayerEntity.getKeyE() == true && PlayerEntity.getLocked() == true)
            {
                caixa.GetComponent<LockB>().locka = false;
            }

            else if (PlayerEntity.getIsInside() == false)
            {
                caixa.GetComponent<LockB>().locka = false;
                strength = 0;
            }

            if (caixa.GetComponent<LockB>().islocked == true)
            {
                PlayerEntity.setLocked(true);
                PlayerEntity.setIdle(false);
                transform.position = Vector3.Lerp(transform.position, caixa.GetComponent<LockB>().posicao.position, Time.deltaTime * 5f);                
                if (Mathf.Abs(Vector3.Distance(transform.position, caixa.GetComponent<LockB>().posicao.position)) < 0.1f)
                {
                    Debug.Log("TRUE KRL");
                   
                }
                //PlayerEntity.setCanThrow(true);
            }
            else if(caixa.GetComponent<LockB>().islocked == false)
            {
                PlayerEntity.setLocked(false);
                PlayerEntity.setIdle(true);               
                //if (Mathf.Abs(Vector3.Distance(transform.position, caixa.GetComponent<LockB>().posicao.position)) > 1)
                //{
                //    PlayerEntity.setCanThrow(false);
                //}


            }
            //if (PlayerEntity.getCanThrow() == true)
            //{
                if (PlayerEntity.getLocked() == true && PlayerEntity.getKeyQ() == true)
                {                                  
                    caixa.GetComponent<LockB>().locka = false;
                    if (PlayerEntity.getIsLockedInContinuous() == true)
                    {
                        caixa.GetComponent<ThrowContinuousBox>().push = true;
                    }

                }
                if (PlayerEntity.getKeyQHeld() == true)
                {
                    strength += 1f;
                    if (strength > 15f)
                    {
                        strength = 15f;
                    }
                    PlayerEntity.setWantToThrow(true);
                }
                if (PlayerEntity.getKeyQHeld() == false && PlayerEntity.getWantToThrow() == true)
                {
                    caixa.GetComponent<ThrowBox>().strength = strength;
                    caixa.GetComponent<ThrowBox>().push = true;
                    PlayerEntity.setWantToThrow(false);
                }
            //}
        }
        #endregion

        PlayerAction();
    }

    public void PlayerAction()
    {

        if (PlayerEntity.getIdle() == true)
        {
            GetComponent<Movement>().enabled = true;
            GetComponent<playerJump>().enabled = true;
            GetComponent<Dash>().enabled = true;
            GetComponent<RaycastShoot>().enabled = true;
            rb.isKinematic = false;
        }

        if (PlayerEntity.getLocked() == true)
        {
            GetComponent<Movement>().enabled = false;
            GetComponent<playerJump>().enabled = false;
            GetComponent<Dash>().enabled = false;
            GetComponent<RaycastShoot>().enabled = false;
            rb.isKinematic = true;
        }
    }

    #region Collissions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ContinuosBox"))
        {           
            PlayerEntity.setIsLockedInContinuous(true);
        }

        if (collision.gameObject.CompareTag("SimpleBox"))
        {
            PlayerEntity.setIsLockedInSimple(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ContinuosBox"))
        {           
            PlayerEntity.setIsLockedInContinuous(false);
        }

        if (collision.gameObject.CompareTag("SimpleBox"))
        {
            PlayerEntity.setIsLockedInSimple(false);
        }
    }
    #endregion

    #region Triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {
            // o other é o objeto o qual está sendo comparada a tag no caso a "ContinuousBox"
            //se usar só "transform.position" vai mudar a posição de quem está com esse código no caso o player;
            //Debug.Log("O player entrou no trigger que deveria");            
            caixa = other.gameObject;
            PlayerEntity.setBoxLocked(caixa);
            PlayerEntity.setIsLockedInContinuous(true);
            PlayerEntity.setIsInside(true);
        }

        if (other.gameObject.CompareTag("SimpleBox"))
        {
            caixa = other.gameObject;
            PlayerEntity.setBoxLocked(caixa);
            PlayerEntity.setIsLockedInSimple(true);
            PlayerEntity.setIsInside(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {
            PlayerEntity.setIsLockedInContinuous(false);
            PlayerEntity.setIsInside(false);
            //caixa = null;
            //Debug.Log("O player saiu no trigger que deveria");
        }
        if (other.gameObject.CompareTag("SimpleBox"))
        {
            PlayerEntity.setIsLockedInSimple(false);
            PlayerEntity.setIsInside(false);
            //caixa = null;
        }
    }
    #endregion
}
