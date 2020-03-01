using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;
    bool inputJump;
    bool verificaChao;
    float jumpForce = 5f;
    float contaPulo = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //verificaChao = 
        //Debug.Log(contaPulo);
        inputJump = Input.GetKeyDown(KeyCode.Space);
        if (inputJump/* && contaPulo < 2*/ ){

            rb.velocity += Vector3.up * Mathf.Sqrt(jumpForce * 1f * -Physics.gravity.y);
            //rb.velocity += Physics.gravity.y;
            contaPulo++; 
        }       
        
    }
}
