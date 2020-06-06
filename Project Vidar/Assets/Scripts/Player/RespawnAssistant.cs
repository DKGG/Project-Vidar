using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAssistant : MonoBehaviour
{
    Camera m_MainCamera;

    void Start()
    {
        //This gets the Main Camera from the Scene
        m_MainCamera = Camera.main;
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            // Fade Out when falling

            // ==

            // Move Player to Respawn Point
            // Debug.Log(PlayerEntity.getSpawnPoint());
            transform.position = PlayerEntity.getSpawnPoint();
            // ==
        }
    }
}
