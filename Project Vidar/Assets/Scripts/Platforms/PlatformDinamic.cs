using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlatformDinamic : MonoBehaviour
{
    enum PlatformMovement
    {
        forward,
        up,
        down
    };

    [SerializeField] PlatformMovement platformMovement;    
    [SerializeField] AudioSource audio;

    //public GameObject Player;
    public GameObject respawn;
    Vector3 scaleVector = new Vector3(1, 1, 1);
    public float speed = 15;
    private int direction = 1;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        switch(platformMovement)
        {
            case PlatformMovement.up:
                transform.Translate(Vector3.up * speed * direction * Time.deltaTime);
                break;
            case PlatformMovement.forward:
                transform.Translate(Vector3.forward * speed * direction * Time.deltaTime);
                break;
            case PlatformMovement.down:
                transform.Translate(Vector3.down * speed * direction * Time.deltaTime);
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

        if (other.gameObject.CompareTag("PlatformT"))
        {
            direction *= -1;
            //troca de direção aqui
            audio.Play();

        }
        if (other.gameObject.CompareTag("Respawna"))
        {
           transform.position = respawn.transform.position;
           rb.velocity = Vector3.zero;           
            
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
}
