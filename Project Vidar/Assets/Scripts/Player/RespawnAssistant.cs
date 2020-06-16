﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAssistant : MonoBehaviour
{
    [SerializeField] float spawnHeight = 0;
    [SerializeField] float cameraHeight = 0;

    Camera m_MainCamera;
    OverTheShoulderCamera cameraScript;
    bool deadly;

    void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        cameraScript = m_MainCamera.GetComponent<OverTheShoulderCamera>();
    }

    void Update()
    {
        if (spawnHeight > 0)
            spawnHeight = 0;

        if (!PauseController.GameIsPaused)
        {
            if (transform.position.y < cameraHeight)
            {
                // Fade Out when falling
                cameraScript.enabled = false;
                // ==
            }
            else if (!cameraScript.enabled)
            {
                cameraScript.enabled = true;
            }
        }

        if (transform.position.y < spawnHeight)
        {
            // Move Player to Respawn Point
            // Debug.Log(PlayerEntity.getSpawnPoint());
            transform.position = PlayerEntity.getSpawnPoint();
            // ==
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MovingPlatform>() && other.GetComponent<MovingPlatform>().deadly)
        {
            transform.position = PlayerEntity.getSpawnPoint();
        }
        if(other.GetComponent<TargetSpike>() && other.GetComponent<TargetSpike>().deadly)
        {
            transform.position = PlayerEntity.getSpawnPoint();
            FindObjectOfType<AudioManager>().stopAllAmbients();
            FindObjectOfType<AudioManager>().Play("ambient2");
        }
    }
}
