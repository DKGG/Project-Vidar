using UnityEngine;
using System.Collections;

public class AddConstantVelocity : MonoBehaviour
{
    [SerializeField]
    Vector3 v3Force;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += v3Force;
    }
}
