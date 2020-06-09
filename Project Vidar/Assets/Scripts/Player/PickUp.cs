using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform grabPos;

    bool grab = false;

    private void OnTriggerStay(Collider other)
    {
        // Can be through trigger
        if (Input.GetKeyDown(KeyCode.E) && !grab)
            GrabAction(); // PickUpObject();
        else if (Input.GetKeyDown(KeyCode.E) && grab)
            DropAction(); // DropObject();
    }

    private void GrabAction()
    {
        
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = grabPos.position;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        grab = true;
        
    }

    private void DropAction()
    {
        
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        grab = false;

    }
}
