using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLever : MonoBehaviour
{
    [SerializeField] MovingPlatform platformTarget;

    private void OnTriggerStay(Collider other)
    {
        // Can be through trigger
        if (Input.GetKeyDown(KeyCode.E) && !platformTarget.movementEnabled)
            ActivatePlatform();

        // if (Input.GetKeyDown(KeyCode.E) && platformTarget.movementEnabled)
        //    DeactivatePlatform();
    }

    private void ActivatePlatform()
    {
        platformTarget.movementEnabled = true;
    }

    private void DeactivatePlatform()
    {
        platformTarget.movementEnabled = false;
    }
}
