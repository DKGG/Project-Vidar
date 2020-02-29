using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    string status = "quo";

    GameObject caixa = null;

    InputController inputController;

    bool isInside;


    void Start()
    {
        inputController = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        #region interacaoCaixas

        if (caixa != null)
        {
            if (isInside == true && inputController.CheckInputE() == true && status == "quo")
            {
                caixa.GetComponent<LockB>().locka = true;
            }
            else if (isInside == true && inputController.CheckInputE() == true && status == "locked")
            {
                caixa.GetComponent<LockB>().locka = false;
            }

            else if (isInside == false)
            {
                caixa.GetComponent<LockB>().locka = false;
            }

            if (caixa.GetComponent<LockB>().islocked == true)
            {
                status = "locked";
                transform.position = Vector3.Lerp(transform.position, caixa.GetComponent<LockB>().posicao.position, Time.deltaTime * 5);                
                caixa.GetComponent<ThrowContinuousBox>().lockSide = caixa.GetComponent<LockB>().side;
            }
            else
            {
                status = "quo";
            }

            if (status == "locked" && inputController.CheckInputQ())
            {
                caixa.GetComponent<ThrowContinuousBox>().push = true;
            }
        }
        #endregion
        PlayerAction(status);
    }

    public void PlayerAction(string status)
    {
        switch (status)
        {
            case "quo":
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
            isInside = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {
            isInside = false;
            //Debug.Log("O player saiu no trigger que deveria");
        }
    }
    #endregion
}
