using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerJump : MonoBehaviour
{
    Rigidbody rb;

    public float jumpForce = 5f;
    public int jumpCounter = 1;
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
            if (PlayerEntity.getGrounded() || jumpCounter > 0)
            {
                jumpCounter -= 1;
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
            jumpCounter = 1;
            PlayerEntity.setIsFalling(false);
        }
    }

    private void Jump()
    {
        PlayerEntity.setJumping(true);
        rb.velocity = Vector3.up * Mathf.Sqrt(jumpForce * -1f * Physics.gravity.y);
        AnimatorManager.setStateJump();

        StartCoroutine(JumpTime());
    }

    private IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(1);
        PlayerEntity.setJumping(false);
    }
}
