﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private float nextSpawnTime;

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float spawnCD = 5;
    [SerializeField]
    private bool spawnEnabled = true;

    private void Update()
    {
        if (spawnEnabled && SpawnTime())
            Spawn();
    }

    private void Spawn()
    {
        nextSpawnTime = Time.time + spawnCD;
        Instantiate(prefab, transform.position, transform.rotation);
    }

    private bool SpawnTime()
    {
        return Time.time >= nextSpawnTime;
    }
}
