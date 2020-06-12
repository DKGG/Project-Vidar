using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;

    public float jumpForce = 5f;
    public int jumpCounter = 2;
    [SerializeField] private float dragDownForce = 5f;
    [SerializeField] private float extraGravity = 1.6f;

    [SerializeField] CapsuleCollider vidarSphere;
    private LayerMask groundLayers;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundLayers = 1;
    }

    private void Update()
    {
        GroundCheck();
        JumpBalance();

        if (PlayerEntity.getButtonJump())
        {
            if (jumpCounter > 0)
            {
                if (PlayerEntity.getDashing())
                {
                    rb.drag = 0;
                }

                Jump();
            }

        }
    }

    private void GroundCheck()
    {
        PlayerEntity.setGrounded(
            Physics.CheckCapsule(
                vidarSphere.bounds.center,
                new Vector3(vidarSphere.bounds.center.x, vidarSphere.bounds.min.y, vidarSphere.bounds.center.z),
                vidarSphere.radius * 0.9f,
                groundLayers)
            );
    }

    private void JumpBalance()
    {
        if (Mathf.Abs(rb.velocity.y) <= 1f && !PlayerEntity.getGrounded() && !PlayerEntity.getDashing())
        {
            AnimatorManager.setStateFalling();
            PlayerEntity.setIsFalling(true);
            return;
        }

        if (Mathf.Abs(rb.velocity.y) < 1)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (dragDownForce - 1) * Time.deltaTime;
        }
        else if (Mathf.Abs(rb.velocity.y) > 0 && !PlayerEntity.getButtonJump())
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (extraGravity - 1) * Time.deltaTime;
        }

        if (PlayerEntity.getGrounded())
        {
            //jumpCounter = 2;
            PlayerEntity.setIsFalling(false);
        }
        if (!PlayerEntity.getIsFalling() && !PlayerEntity.getJumping() && PlayerEntity.getGrounded())
        {
            resetJump();
        }
    }

    private void Jump()
    {
        FindObjectOfType<AudioManager>().Play("jump");
        PlayerEntity.setJumping(true);
        rb.velocity = Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y);
        AnimatorManager.setStateJump();
        jumpCounter -= 1;
        StartCoroutine(JumpTime());
    }

    private IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(1);
        PlayerEntity.setJumping(false);
    }

    private void resetJump()
    {
        jumpCounter = 2;
    }
}
