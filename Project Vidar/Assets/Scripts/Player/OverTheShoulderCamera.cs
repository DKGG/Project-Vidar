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
    Transform OldTarget;

    bool changeCam;

    public Transform cameraPivot;

    Vector3 changeTargetAxis;
    Vector3 OldchangeTargetAxis;

    // Private variables
    private float mouseX, mouseY;

    private void Start()
    {
        cameraPivot = transform.parent;
        changeTargetAxis = new Vector3(1.5f, 0.5f, -8f);
        OldchangeTargetAxis = new Vector3(1.5f, 0.5f, -2.5f);
        OldTarget = playerTransForm;
    }

    private void Update()
    {
        if (PlayerEntity.getLocked() == true)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, changeTargetAxis, Time.deltaTime * 5);
            playerTransForm = playerObject.transform.parent;
            changeCam = true;
        }
        else if (PlayerEntity.getLocked() == false && changeCam == true)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, OldchangeTargetAxis, Time.deltaTime * 100);
            playerTransForm = OldTarget;
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
