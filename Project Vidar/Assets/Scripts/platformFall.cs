using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformFall : MonoBehaviour
{
    bool isFalling;
    bool alreadyFall = false;
    bool lostCollision = false;   
    float downSpeed = 0;
    float timer = 5.0f;
    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling == true)
        {
            downSpeed += Time.deltaTime / 20;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
            alreadyFall = true;
        }

        if (alreadyFall == true && lostCollision == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + downSpeed, transform.position.z);
                isFalling = false;
                if (Mathf.Abs(Vector3.Distance(transform.position, startPosition)) < 2)
                {
                    transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
                    downSpeed = 0;
                    alreadyFall = false;
                    timer = 5.0f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFalling = true;
            lostCollision = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lostCollision = true;
        }
    }
}
