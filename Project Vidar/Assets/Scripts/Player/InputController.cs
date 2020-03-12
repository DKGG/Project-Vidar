using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool isGrounded = true;
    public bool isDashing = false;

    public bool GetButtonJump()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool GetKeyLeftShift()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool GetKeyE()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool GetKeyQ()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
    public float CheckInputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");         
    }

    public float CheckInputVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }
}
