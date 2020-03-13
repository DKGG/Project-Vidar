using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxMovement : MonoBehaviour
{
    public float velocity = 5f;
    public float turnSpeed = 100f;

    Vector2 input;
    Vector3 boxPush;
    float angle;
    Quaternion targetRotation;
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        getInput();

        if (Math.Abs(input.x) < 1 && Math.Abs(input.y) < 1)
        {
            return;
        }

        calculateDirection();
        // rotate();
        move();
    }

    private void move()
    {
        Debug.Log("Y: " + input.y);
//         Debug.Log("X: " + input.x);

        // Debug.Log("Forward: " + transform.forward);

        if (input.y > 0)
        {
            transform.position += transform.forward * velocity * Time.deltaTime;
            // boxPush = transform.forward;
            Debug.Log(boxPush);
        }
        
        if (input.y < 0)
        {
            transform.position -= transform.forward * velocity * Time.deltaTime;
        }

        if (input.x > 0)
        {
            Debug.Log("X plus");
            transform.position -= transform.right * velocity * Time.deltaTime;
        }

        if (input.x < 0)
        {
            Debug.Log("X minus");
            transform.position += transform.right * velocity * Time.deltaTime;
        }
    }

    private void rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void getInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void calculateDirection()
    {
        angle = Mathf.Atan2(-input.x, -input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }
}
