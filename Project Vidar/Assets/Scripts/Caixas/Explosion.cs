using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float blastRadius;
    public float explodionPower;
    public LayerMask explosionLayers;

    bool stop = false;
    GameObject box;
    Collider[] hitColliders;
    AlphaShaderAnimation shader;

    private void OnCollisionEnter(Collision col)
    {
        box = col.gameObject;

        Debug.Log(box);

        if (box.layer == 9) // && PlayerEntity.getFirstExplosionWall()
        {
            //PlayerEntity.setFirstExplosionWall(false);
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

            shader = hitCol.gameObject.GetComponent<AlphaShaderAnimation>();
            shader.spellDown = true;

            //hitCol.gameObject.GetComponent<Animator>().SetBool("block",false);
            //hitCol.GetComponent<MeshRenderer>().enabled = false;

            hitCol.GetComponent<Rigidbody>().AddExplosionForce(explodionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
            Destroy(hitCol.gameObject,2);
        }
    }
}
