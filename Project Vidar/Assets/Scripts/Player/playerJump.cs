using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;
    bool isGrounded = true;
    bool doubleJump = true;
    public float jumpForce = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
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
            isGrounded = true;
            doubleJump = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        rb.velocity = Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y);
    }
}
