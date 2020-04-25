using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFalling : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject parede1;
    [SerializeField] GameObject parede2;
    [SerializeField] Transform explosion;
    float timer = 5f;
    bool startTimer = false;    
    public float radius = 5.0F;
    public float power = 10.0F;

    void Start()
    {
        //Vector3 explosionPos = explosion.position;
        //Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        //foreach (Collider hit in colliders)
        //{
        //    Rigidbody rb = hit.GetComponent<Rigidbody>();

        //    if (rb != null)
        //        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        //}        
        
    }

    void Update()
    {
        if (startTimer == true)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("ContinuosBox"))
        {
            Vector3 explosionPos = explosion.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            parede1.SetActive(false);
            parede2.SetActive(true);
            //parede2.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
            //parede2.GetComponentInChildren<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
            }
            //parede2.GetComponent<Rigidbody>().isKinematic = false;
            //parede2.GetComponentInChildren<Rigidbody>().isKinematic = false;
            startTimer = true;
        }

        if (collision.gameObject.CompareTag("SimpleBox"))
        {
            Vector3 explosionPos = transform.position;
            parede1.SetActive(false);
            parede2.SetActive(true);
            //parede2.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
            parede2.GetComponentInChildren<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0f);
            //parede2.GetComponent<Rigidbody>().isKinematic = false;
            //parede2.GetComponentInChildren<Rigidbody>().isKinematic = false;
            startTimer = true;
        }

    }
}
