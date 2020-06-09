using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 100f;
    public float hitForce = 100f;
    public Transform gunEnd;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine; 
    private float nextFire;

    private Vector3 freezeSave;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {        
        if (PlayerEntity.getKeyQ() == true && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            /*
             * ViewportToWorldPoint converts point on viewport to game world.
             * X and Y defined the position on screen.
             * Z as 0 defined the point to be exactly where the camera/player is.
             */
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                FreezableBox box = hit.collider.GetComponent<FreezableBox>();
                AnimatorManager.setStateFreezing();

                if (box != null && hit.rigidbody != null)
                {
                    if (box.isFrozen)
                    {
                        FindObjectOfType<AudioManager>().Play("freeze");
                        AnimatorManager.setStateFreezing();
                        PlayerEntity.setIsFreezing(true);
                        //AnimatorManager.setState("isFreezing", true);
                        //Debug.Log("true");
                        box.isFrozen = false;
                        box.rb.constraints = RigidbodyConstraints.None;
                        box.rb.velocity = freezeSave;
                    }
                    else
                    {
                        PlayerEntity.setIsFreezing(true);
                        FindObjectOfType<AudioManager>().Play("freeze");
                        //AnimatorManager.setStateFreezing();
                        //Debug.Log("false");
                        box.isFrozen = true;
                        freezeSave = box.rb.velocity;
                        box.rb.constraints = RigidbodyConstraints.FreezeAll;
                    }
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }

    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        yield return 1.1;
        PlayerEntity.setIsFreezing(false);
    }
}
