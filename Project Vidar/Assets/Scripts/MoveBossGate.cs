using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBossGate : MonoBehaviour
{
    [Header("Move Object From -> To")]
    [Space]
    [SerializeField] GameObject toMove;
    [SerializeField] GameObject moveTo;
    [SerializeField] float speed = 2f;

    bool triggered = false;

    void Update()
    {
        if (PlayerEntity.getBossSpawner() == 6)
        {
            triggered = true;
        }

        if (Vector3.Distance(moveTo.transform.position, toMove.transform.position) < 3f)
        {
            triggered = false;
        }
        else if (triggered)
        {
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, moveTo.transform.position, Time.deltaTime * speed);
        }
    }
}
