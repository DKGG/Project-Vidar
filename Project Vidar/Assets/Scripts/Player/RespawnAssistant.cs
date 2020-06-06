using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAssistant : MonoBehaviour
{
    Camera m_MainCamera;
    OverTheShoulderCamera cameraScript;

    void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
        cameraScript = m_MainCamera.GetComponent<OverTheShoulderCamera>();
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            // Fade Out when falling
            cameraScript.enabled = false;
            // ==

            // Move Player to Respawn Point
            // Debug.Log(PlayerEntity.getSpawnPoint());
            transform.position = PlayerEntity.getSpawnPoint();
            // ==
        }
        else if (!cameraScript.enabled)
        {
            cameraScript.enabled = true;
        }
    }
}
