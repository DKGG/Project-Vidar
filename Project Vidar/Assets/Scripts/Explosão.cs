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
    public bool stop = false;
  

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 9)
        {
            Debug.Log("Entrei");
            ExplosionWorks(col.contacts[0].point);
        }
    }

    void ExplosionWorks(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);
        if (!stop)
        {
            FindObjectOfType<AudioManager>().Play("explosion");
            stop = true;
        }
        foreach (Collider hitCol in hitColliders)
        {
            hitCol.GetComponent<Rigidbody>().isKinematic = false;
            hitCol.gameObject.GetComponent<Animator>().SetBool("block",false);
            //hitCol.GetComponent<MeshRenderer>().enabled = false;
            hitCol.GetComponent<Rigidbody>().AddExplosionForce(explodionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
            Destroy(hitCol.gameObject,5);
        }
    }
}
