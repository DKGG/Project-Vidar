using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("Move Object From -> To")]
    [Space]
    [SerializeField] GameObject toMove;
    [SerializeField] GameObject moveTo;
    [SerializeField] float speed = 7f;
    [Space]
    [Header("Will Change Cam")]
    // [SerializeField] bool changeCam;
    [SerializeField] CameraChanger scriptCam;

    bool triggered = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Vector3.Distance(moveTo.transform.position, toMove.transform.position) < 3f)
        {
            triggered = false;
        }
        else if (triggered)
        {
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, moveTo.transform.position, Time.deltaTime * speed);
            if (scriptCam != null && !scriptCam.active)
                scriptCam.active = true;
            if (anim != null)
                anim.SetBool("turn", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            triggered = !triggered;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
            triggered = !triggered;
    }
}
