using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] float timeLeft = 6f;
    float initialTimeLeft;

    Camera cam;
    Camera mainCam;
    public bool active = false;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        mainCam = Camera.main;
        initialTimeLeft = timeLeft;
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
                timeLeft = initialTimeLeft;
                active = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
            }
            else
            {
                mainCam.enabled = false;
                cam.enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
            }
        }
    }
}
