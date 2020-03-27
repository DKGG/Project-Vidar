using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMove : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    [SerializeField] Vector3 moveTo;
    [SerializeField] float speed = 7f;

    bool triggered = false;


    void Update()
    {
        if (triggered)
            bridge.transform.position = Vector3.Lerp(bridge.transform.position, moveTo, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        // rb.MovePosition(transform.position + forward * Time.fixedDeltaTime);
    }
}
