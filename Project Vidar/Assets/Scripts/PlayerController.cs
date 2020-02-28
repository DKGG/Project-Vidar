using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float forceCounter;
    float forceTime = 0.5f;
    bool isInside;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isInside);
    }

    public void CheckScripts(bool active, string name)
    {
        switch (name)
        {
            //exemplo de como fazer a velocidade continua
            case "Movement":
                if (active)
                {
                    GetComponent<Movement>().enabled = true;
                }
                else
                    GetComponent<Movement>().enabled = false;
                break;
            //exemplo de como fazer a velocidade aumentar e diminuir dps de um tempo
            case "playerJump":
                if (active)
                {
                    GetComponent<playerJump>().enabled = true;
                }
                else
                    GetComponent<playerJump>().enabled = false;
                break;

        }
    }


    public bool CheckPlayerStates(string state)
    {
        switch (state)
        {
            //exemplo de como fazer a velocidade continua
            case "isInside":
                if (isInside)
                {
                    return true;
                }
                else
                    return false;
            default:
                return false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("ContinuosBox"))
    //    {
    //        isInside = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("ContinuosBox"))
    //    {
    //        isInside = false;
    //    }
    //}
}
