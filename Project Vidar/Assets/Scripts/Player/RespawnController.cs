using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public Vector3 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerEntity.setSpawnPoint(spawnPoint);
            Debug.Log(PlayerEntity.getSpawnPoint());
            // col.transform.position = spawnPoint;
        }
    }
}
