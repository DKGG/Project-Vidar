using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] GameObject toMove;
    [SerializeField] GameObject moveTo;
    [SerializeField] float speed = 7f;

    Vector3 initialtoMove;
    Vector3 initialMoveTo;

    bool triggered = false;

    private void Start()
    {
        initialtoMove = toMove.transform.position;
        initialMoveTo = moveTo.transform.position;
    }

    void Update()
    {
        if (triggered)
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, moveTo.transform.position, Time.deltaTime * speed);
        else
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, initialtoMove, Time.deltaTime * speed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            triggered = !triggered;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ContinuosBox"))
        {
            triggered = !triggered;
        }
    }
}
