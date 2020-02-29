using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{    
    public bool CheckInputE()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public bool CheckInputQ()
    {
        return Input.GetKey(KeyCode.Q);
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
