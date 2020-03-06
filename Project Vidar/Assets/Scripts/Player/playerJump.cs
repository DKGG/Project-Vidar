using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;
    bool doubleJump = true;
    public float jumpForce = 5f;
    InputController inputController;

    private void Start()
    {
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();

        if (inputController.GetButtonJump())
        {
            if (inputController.isGrounded)
            {
                Jump();
            }
            else if (doubleJump)
            {
                doubleJump = false;
                Jump();
            }
        }
    }

    private void GroundCheck()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, 2f))
        {
            inputController.isGrounded = true;
            doubleJump = true;
        }
        else
        {
            inputController.isGrounded = false;
        }
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y);
    }
}
