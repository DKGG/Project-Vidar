using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{    
    public float CheckInputMouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    public float CheckInputMouseY()
    {
         return Input.GetAxis("Mouse Y");
    }

    
}
