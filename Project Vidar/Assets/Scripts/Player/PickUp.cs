using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform grabPos;
    [SerializeField] Transform VerificaFrente;
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
        VerificaFrente.localPosition = new Vector3(0,1,3.3f);
        PlayerEntity.setIsLockedInSimple(true);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        grab = true;
        yield return null;
    }

    IEnumerator DropAction()
    {
        yield return new WaitForEndOfFrame();
        transform.parent = null;
        VerificaFrente.localPosition = new Vector3(0, 1, 1);
        PlayerEntity.setIsLockedInSimple(false);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        grab = false;
        yield return null;
    }
}
