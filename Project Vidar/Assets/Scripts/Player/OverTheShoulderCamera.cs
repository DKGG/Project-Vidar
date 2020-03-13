using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheShoulderCamera : MonoBehaviour
{
    [Header("Look Properties")]
    [SerializeField] Transform playerTransForm, lookTarget;
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] GameObject playerObject;

    bool changeCam;
    bool moveCam = false;

    public Transform zoomIn;
    public Transform zoomOut;

    Vector3 changeTargetAxis;
    Vector3 OldchangeTargetAxis;

    // Private variables
    private float mouseX, mouseY;
    Transform cameraPivot;
    Transform cameraFocus;

    private void Start()
    {
        cameraPivot = transform.parent;
    }

    private void Update()
    {
        if (PlayerEntity.getKeyX())
        {
            moveCam = !moveCam;
        }

        if (moveCam)
        {
            transform.position = Vector3.Lerp(transform.position, zoomIn.transform.position, Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, zoomOut.transform.position, Time.deltaTime * 5f);
            
            changeTargetAxis = new Vector3(1.5f, 0.5f, -8f);
            OldchangeTargetAxis = new Vector3(1.5f, 0.5f, -2.5f);

            cameraFocus = playerTransForm;
        }

        if (PlayerEntity.getLocked() && !changeCam)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, changeTargetAxis, Time.deltaTime * 5);
            playerTransForm = playerObject.transform.parent;
            changeCam = true;
        }

        if (PlayerEntity.getLocked() && changeCam)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, OldchangeTargetAxis, Time.deltaTime * 5);
            playerTransForm = cameraFocus;
            changeCam = false;
        }
    }

    private void LateUpdate()
    {
        mouseX += PlayerEntity.checkMouseX() * rotationSpeed;
        mouseY -= PlayerEntity.checkMouseY() * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -80, 80);

        cameraPivot.position = playerTransForm.position;

        transform.LookAt(lookTarget);
        cameraPivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
}
