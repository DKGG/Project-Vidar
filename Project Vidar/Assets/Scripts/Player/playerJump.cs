using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;    
    public float jumpForce = 5f;

    private void Start()
    {        
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GroundCheck();

        if (PlayerEntity.getButtonJump())
        {
            if (PlayerEntity.getGrounded() == true)
            {
                Jump();
            }
            else if (PlayerEntity.getCanDoubleJump())
            {
                PlayerEntity.setCanDoubleJump(false);
                Jump();
                PlayerEntity.setCanDoubleJump(false);                ;
            }
        }
    }

    private void GroundCheck()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, 2f))
        {
            PlayerEntity.setGrounded(true);
            PlayerEntity.setCanDoubleJump(true);
           // PlayerEntity.setCanPlayJumpAnim(false);

        }
        else
        {
            PlayerEntity.setCanPlayWalkAnim(false);
            PlayerEntity.setGrounded(false);
        }
    }

    private void Jump()
    {
        PlayerEntity.setCanPlayJumpAnim(true);
        Debug.Log("Pulei");
        rb.velocity = Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y);
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        PlayerEntity.setCanPlayJumpAnim(true);
        yield return 2;
        PlayerEntity.setCanPlayJumpAnim(false);
        yield return new WaitForSeconds(1);
    }
}
