using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheShoulderCamera : MonoBehaviour
{
    [Header("Look Properties")]
    [SerializeField] Transform playerTransForm, lookTarget;
    [SerializeField] float rotationSpeed = 5.0f;

    private Transform cameraPivot;

    // Private variables
    private float mouseX, mouseY;

    private void Start()
    {
        cameraPivot = transform.parent;
    }

    private void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -80, 80);

        cameraPivot.position = playerTransForm.position;

        transform.LookAt(lookTarget);
        cameraPivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
