using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{   
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
    public bool GetKeyX()
    {
        return Input.GetKeyDown(KeyCode.X);
    }
    public float CheckInputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");         
    }

    public float CheckInputVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public float CheckInputMouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    public float CheckInputMouseY()
    {
         return Input.GetAxis("Mouse Y");
    }

    
}
