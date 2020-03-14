using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMove : MonoBehaviour
{
    [SerializeField] GameObject bridge;
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        bridge.transform.position = new Vector3(-0.75f, -3.5f, -0.75f);
        // rb.MovePosition(transform.position + forward * Time.fixedDeltaTime);
    }
}
