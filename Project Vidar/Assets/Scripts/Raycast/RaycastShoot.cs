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
    private Transform cameraPivot;

    FreezableBox freezableComponent;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        cameraPivot = GameObject.FindGameObjectWithTag("MainCamera").transform.parent;
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

                if(hit.collider.GetComponent<FreezableBox>())
                {
                    freezableComponent = hit.collider.GetComponent<FreezableBox>();
                }
                else
                {
                    freezableComponent = hit.collider.gameObject.GetComponentInChildren<FreezableBox>();
                }

                // AnimatorManager.setStateFreezing();

                if (freezableComponent != null && freezableComponent.rb != null)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, cameraPivot.rotation.eulerAngles.y, 0), 0.9f);
                    if (freezableComponent.isFrozen)
                    {
                        FindObjectOfType<AudioManager>().Play("freeze");
                        // AnimatorManager.setStateFreezing();
                        PlayerEntity.setIsFreezing(true);

                        freezableComponent.isFrozen = false;
                        freezableComponent.rb.constraints = RigidbodyConstraints.FreezeRotation;
                        freezableComponent.rb.velocity = freezeSave;
                    }
                    else
                    {
                        PlayerEntity.setIsFreezing(true);
                        FindObjectOfType<AudioManager>().Play("freeze");

                        freezableComponent.isFrozen = true;
                        freezeSave = freezableComponent.rb.velocity;
                        freezableComponent.rb.constraints = RigidbodyConstraints.FreezeAll;
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

        //laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
        yield return 1.1;
        PlayerEntity.setIsFreezing(false);
    }
}
