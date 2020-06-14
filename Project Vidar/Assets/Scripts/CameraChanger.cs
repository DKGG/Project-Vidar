using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] float timeLeft = 3f;

    Camera cam;
    Camera mainCam;
    public bool active = false;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (active)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                mainCam.enabled = true;
                cam.enabled = false;
                timeLeft = 3f;
                active = false;
            }
            else
            {
                mainCam.enabled = false;
                cam.enabled = true;
            }
        }
    }
}
