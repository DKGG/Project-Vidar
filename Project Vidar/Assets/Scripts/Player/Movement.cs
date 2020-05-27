using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocity = 5f;
    public float turnSpeed = 100f;

    Vector2 input;
    float angle;
    Quaternion targetRotation;
    Transform cam;
    public bool grassStep;
    public bool woodStep;
    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        getInput();

        if (Math.Abs(input.x) < 1 && Math.Abs(input.y) < 1)
        {
            if (!PlayerEntity.getJumping() && !PlayerEntity.getIsFalling() && !PlayerEntity.getDashing())
            {
                grassStep = false;
                AnimatorManager.setStateIdle();
                FindObjectOfType<AudioManager>().Stop("grassStep");
            }
            return;
        }

        if (!PlayerEntity.getJumping() && !PlayerEntity.getIsFalling() && !PlayerEntity.getDashing())
        {
            // PlayerEntity.setWalking(true);
            AnimatorManager.setStateRun();
            if (!grassStep && PlayerEntity.getGrounded())
            {
                FindObjectOfType<AudioManager>().Play("grassStep");
                grassStep = true;
            }
        }
        if (!PlayerEntity.getGrounded())
        {
            grassStep = false;
            FindObjectOfType<AudioManager>().Stop("grassStep");
        }

        calculateDirection();
        rotate();
        move();
    }

    private void move(){
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    private void rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed*Time.deltaTime);
    }

    private void getInput()
    {
        input.x = PlayerEntity.checkInputHorizontal();
        input.y = PlayerEntity.checkInputVertical();
    }

    void calculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }
}
