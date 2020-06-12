using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerEntity.setSpawnPoint(spawnPoint);
            // col.transform.position = spawnPoint;
        }
    }
}
