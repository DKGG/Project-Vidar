using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheShoulderCamera : MonoBehaviour
{
    [Header("Look Properties")]
    [SerializeField] Transform playerTransForm, lookTarget;
    [SerializeField] float rotationSpeed = 5.0f;

    public Transform cameraPivot;

    private float startTime;
    private float journeyLength;

    // Private variables
    private float mouseX, mouseY;

    private void Start()
    {
        cameraPivot = transform.parent;
        

        startTime = Time.deltaTime;

        journeyLength = Vector3.Distance(zoomOut.transform.position, zoomIn.transform.position);
    }

    private void Update()
    {    
                        
        if (inputController.GetKeyX())
        {
          
            moveCam = !moveCam;
        }

        if(moveCam == true)
        {
            transform.position = Vector3.Lerp(transform.position, zoomIn.transform.position, Time.deltaTime * 5f);
            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, zoomOut.transform.position, Time.deltaTime * 5f);
        }

    }

    private void LateUpdate()
    {
        mouseX += inputController.CheckInputMouseX() * rotationSpeed;
        mouseY -= inputController.CheckInputMouseY() * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -80, 80);

        cameraPivot.position = playerTransForm.position;

        // transform.LookAt(lookTarget);
        cameraPivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);       
              
    }
}
