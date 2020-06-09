using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform grabPos;
    bool grab = false;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !grab)
            StartCoroutine(GrabAction()); // PickUpObject();
        else if (Input.GetKeyDown(KeyCode.E) && grab)
            StartCoroutine(DropAction()); // DropObject();
    }

    IEnumerator GrabAction()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = grabPos.position;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        grab = true;
        yield return null;
    }

    IEnumerator DropAction()
    {
        yield return new WaitForEndOfFrame();
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        grab = false;
        yield return null;
    }
}
