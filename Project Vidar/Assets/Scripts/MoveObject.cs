using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] GameObject toMove;
    [SerializeField] GameObject moveTo;
    [SerializeField] float speed = 7f;

    bool triggered = false;

    void Update()
    {
        if (triggered)
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, moveTo.transform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }
}
