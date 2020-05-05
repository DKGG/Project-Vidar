using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpike : MonoBehaviour
{
    enum Physics
    {
        Rigidbody,
        Transform
    }

    enum PlatformMovement
    {
        forward,
        up,
        right
    };

    #region Field Declarations

    [Header("Movement Configs")]
    [SerializeField] public bool movementEnabled = true;
    [SerializeField] bool positiveDirection = true;[Space]

    [SerializeField] float speed = 15f;[Space]

    [Header("How will i move")]
    [SerializeField] PlatformMovement platformMovement;
    [SerializeField] Physics platformPhysics;

    int direction = 1;
    bool isColliding = false;
    bool isPlaying = false;
    Rigidbody rb;

    Vector3 scaleVector = new Vector3(1, 1, 1);
    Vector3 forward, back, left, right, up, down;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (platformPhysics)
        {
            case Physics.Rigidbody:
                if (movementEnabled)
                    RigidbodyMovement();
                break;
            case Physics.Transform:
                if (movementEnabled)
                    TransformMovement();
                break;
            default:
                break;
        }
    }

    private void RigidbodyMovement()
    {
        switch (platformMovement)
        {
            case PlatformMovement.forward:
                forward = new Vector3(speed, 0f, 0f);
                back = new Vector3(-speed, 0f, 0f);

                if (positiveDirection)
                {
                    rb.MovePosition(transform.position + forward * Time.fixedDeltaTime);
                    if (!isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("movePlataform");
                        isPlaying = true;
                    }
                }
                else
                {
                    rb.MovePosition(transform.position + back * Time.fixedDeltaTime);
                    if (!isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("movePlataform");
                        isPlaying = true;
                    }
                }
                
                break;
            case PlatformMovement.up:
                up = new Vector3(0f, speed, 0f);
                down = new Vector3(0f, -speed, 0f);

                if (positiveDirection)
                {
                    rb.MovePosition(transform.position + up * Time.fixedDeltaTime);
                    if (!isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("movePlataform");
                        isPlaying = true;
                    }
                }
                else
                {
                    rb.MovePosition(transform.position + down * Time.fixedDeltaTime);
                    if (!isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("movePlataform");
                        isPlaying = true;
                    }
                }
                break;
            case PlatformMovement.right:
                right = new Vector3(0f, 0f, speed);
                left = new Vector3(0f, 0f, -speed);

                if (positiveDirection)
                {
                    rb.MovePosition(transform.position + right * Time.fixedDeltaTime);
                    if (!isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("movePlataform");
                        isPlaying = true;
                    }
                }
                else
                {
                    rb.MovePosition(transform.position + left * Time.fixedDeltaTime);
                    if (!isPlaying)
                    {
                        FindObjectOfType<AudioManager>().Play("movePlataform");
                        isPlaying = true;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void TransformMovement()
    {
        switch (platformMovement)
        {
            case PlatformMovement.up:
                transform.Translate(Vector3.up * speed * direction * Time.fixedDeltaTime);
                break;
            case PlatformMovement.forward:
                transform.Translate(Vector3.forward * speed * direction * Time.fixedDeltaTime);
                break;
            case PlatformMovement.right:
                transform.Translate(Vector3.right * speed * direction * Time.fixedDeltaTime);
                break;
            default:
                break;
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }

        if (other.gameObject.CompareTag("PlatformCollider"))
        {
            isPlaying = false;
            if (isColliding) return;
            isColliding = true;

            direction *= -1;
            positiveDirection = !positiveDirection;

            // troca de direção aqui
            // audio.Play();

            StartCoroutine(Reset());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.transform.localScale = scaleVector;
        }
    }

    /* 
     * A colisão é detectada diversas vezes ao usar Rigidbody, utilizando uma corotina
     * para esperar o final do frame, a colisão ainda é detectada, mas a booleana "isColliding"
     * para a repetição.
     */
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
}