using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Explosão : MonoBehaviour
{
    //[SerializeField] Transform explosion;
    private Collider[] hitColliders;
    public float blastRadius;
    public float explodionPower;
    public LayerMask explosionLayers;
    float timer = 5f;
    bool startTimer = false;  
        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (startTimer == true)
        //{
        //    timer -= Time.deltaTime;
        //}
        //if (timer <= 0)
        //{
        //    Destroy(gameObject);
        //}


    }

    private void OnCollisionEnter(Collision col)
    {
        ExplosionWorks(col.contacts[0].point);
        startTimer = true;

    }

    void ExplosionWorks(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);

        foreach (Collider hitCol in hitColliders)
        {
            hitCol.GetComponent<Rigidbody>().isKinematic = false;
            hitCol.GetComponent<Rigidbody>().AddExplosionForce(explodionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
            //Destroy(hitCol.gameObject);
        }         
    }
}
