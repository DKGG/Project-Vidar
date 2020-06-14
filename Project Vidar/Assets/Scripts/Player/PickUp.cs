using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform grabPos;
    [SerializeField] Transform VerificaFrente;
    [SerializeField] Transform VerificaTras;
    [SerializeField] Transform VerificaDireita;
    [SerializeField] Transform VerificaEsquerda;
    Rigidbody rb;
    Vector3 staticPos;
    bool grab = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (grab)
        {
            rb.velocity = Vector3.zero;            
        }    
    }
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
        VerificaFrente.localPosition = new Vector3(0,1,3);        
        //staticPos = gameObject.transform.localPosition;
        //VerificaEsquerda.localPosition = new Vector3(-2,1, 2);
        //VerificaDireita.localPosition = new Vector3(2,1, 2);
        PlayerEntity.setIsLockedInSimple(true);        
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;        
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
    private void OnCollisionEnter(Collision collision)
    {
        //if (grab)
        //{
        //    if (Mathf.Abs(Vector3.Distance(transform.position, grabPos.position)) > 0.1f)
        //    {
        //        transform.position = /*grabPos.position;*/ Vector3.Lerp(transform.position, grabPos.position, 1);
        //    }
        //}
    }
}
