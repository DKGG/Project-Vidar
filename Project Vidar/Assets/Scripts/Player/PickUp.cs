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
            StartCoroutine(GrabAction()); // PickUpObject();
        else if (Input.GetKeyDown(KeyCode.E) && grab)
            StartCoroutine(DropAction()); // DropObject();
    }

    IEnumerator GrabAction()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.transform.position = grabPos.position;
        this.transform.parent = GameObject.FindGameObjectWithTag("grabPos").transform;
        grab = true;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator DropAction()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        grab = false;
        yield return new WaitForEndOfFrame();
    }
}
