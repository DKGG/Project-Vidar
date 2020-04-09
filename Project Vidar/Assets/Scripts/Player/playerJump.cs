using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;    
    public float jumpForce = 5f;
    public int jumpCounter = 1;

    private void Start()
    {        
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(PlayerEntity.getJumping());
        GroundCheck();

        if (PlayerEntity.getButtonJump())
        {
            if (PlayerEntity.getGrounded() == true && jumpCounter > 0)
            {
                Jump();
                PlayerEntity.setCanDoubleJump(false);
            }
            else if (PlayerEntity.getCanDoubleJump() && jumpCounter > 0)
            {
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
            jumpCounter = 1;

        }
        else
        {
            PlayerEntity.setGrounded(false);
        }
    }

    private void Jump()
    {
        PlayerEntity.setJumping(true);
        AnimatorManager.setStateJump();
        jumpCounter -= 1;
        //PlayerEntity.setCanPlayJumpAnim(true);
        Debug.Log("Pulei");
        rb.velocity = Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y);
        StartCoroutine(Wait());
        StartCoroutine(secondJumpTimer());
    }
    private IEnumerator Wait()
    {
        yield return 1;
        PlayerEntity.setJumping(false);
        // yield return new WaitForSeconds(1);

    }
    private IEnumerator secondJumpTimer()
    {
        yield return new WaitForSeconds(1);
        PlayerEntity.setCanDoubleJump(true);
    }
}
