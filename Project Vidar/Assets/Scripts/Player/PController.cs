using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    string status = "idle";

    bool lockedInContinuous;
    bool lockedInSimple;
    bool isInside;
    bool wantToThrow = false;

    float strength;

    GameObject caixa = null;
    Rigidbody rb;

    InputController inputController;




    void Start()
    {
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("strength no pcontroller" + strength);

        #region interacaoCaixas

        if (caixa != null)
        {
            if (isInside == true && inputController.GetKeyE() == true && status == "idle")
            {
                caixa.GetComponent<LockB>().locka = true;
            }
            else if (isInside == true && inputController.GetKeyE() == true && status == "locked")
            {
                caixa.GetComponent<LockB>().locka = false;
            }

            else if (isInside == false)
            {
                caixa.GetComponent<LockB>().locka = false;
                strength = 0;                
            }

            if (caixa.GetComponent<LockB>().islocked == true)
            {
                status = "locked";
                transform.position = Vector3.Lerp(transform.position, caixa.GetComponent<LockB>().posicao.position, Time.deltaTime * 5f);
                if (lockedInContinuous)
                {
                    caixa.GetComponent<ThrowContinuousBox>().lockSide = caixa.GetComponent<LockB>().side;
                }
                if (lockedInSimple)
                {
                    caixa.GetComponent<ThrowBox>().lockSide = caixa.GetComponent<LockB>().side;
                }
            }
            else
            {
                status = "idle";
            }

            if (status == "locked" && inputController.GetKeyQ())
            {
                //Debug.Log("Entrei no  antes do if do que eu achei que n ia funcionar");                
                //rb.velocity = Vector3.zero;               
                caixa.GetComponent<LockB>().locka = false;
                if (lockedInContinuous)
                {
                    caixa.GetComponent<ThrowContinuousBox>().push = true;
                }
                
            }

            if (inputController.GetKeyQHeld() == true)
            {
                strength += 1f;
                if (strength > 15f)
                {
                    strength = 15f;
                }
                wantToThrow = true;
            }
            if(inputController.GetKeyQHeld() == false && wantToThrow == true)
            {
                caixa.GetComponent<ThrowBox>().strength = strength;
                caixa.GetComponent<ThrowBox>().push = true;
                wantToThrow = false;
            }

            //if (!inputController.GetKeyQ())
            //{
            //    Debug.Log("aquele if do get keyQ" + inputController.GetKeyQ());
            //    throwing = true;
            //}
            //if (status == "locked" && throwing == true)
            //{
            //    Debug.Log("Entrei no if que eu achei que n ia funcionar");
            //    caixa.GetComponent<ThrowBox>().strength = strength * 10f;
            //    caixa.GetComponent<ThrowBox>().push = true;
            //    throwing = false;

            //}
        }
        #endregion
        PlayerAction(status);
    }

    public void PlayerAction(string status)
    {
        switch (status)
        {
            case "idle":
                GetComponent<Movement>().enabled = true;
                GetComponent<playerJump>().enabled = true;
                break;
            case "locked":
                GetComponent<Movement>().enabled = false;
                GetComponent<playerJump>().enabled = false;
                break;
            default:
                break;
        }
    }

    #region Collissions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ContinuosBox"))
        {
            Debug.Log("O player entrou na colisão que deveria");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ContinuosBox"))
        {
            Debug.Log("O player saiu da colisão que deveria");
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
            lockedInContinuous = true;
            isInside = true;
        }

        if (other.gameObject.CompareTag("SimpleBox"))
        {
            caixa = other.gameObject;
            lockedInSimple = true;
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {
            lockedInContinuous = false;
            isInside = false;
            //Debug.Log("O player saiu no trigger que deveria");
        }
        if (other.gameObject.CompareTag("SimpleBox"))
        {
            lockedInSimple = false;
            isInside = false;
        }
    }
    #endregion
}
