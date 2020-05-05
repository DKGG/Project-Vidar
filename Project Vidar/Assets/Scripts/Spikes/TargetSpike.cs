using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpike : MonoBehaviour
{
    #region Field Declarations

    [Header("Movement Configs")]
    [SerializeField] public bool movementEnabled = true;
    [SerializeField] float speed = 15f;
    [SerializeField] Transform target;

    #endregion

    private void FixedUpdate()
    {
        transform.Translate(target.position * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }
}