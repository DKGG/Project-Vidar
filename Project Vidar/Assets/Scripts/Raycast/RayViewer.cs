using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{
    public float weaponRange = 50f;
    public Camera fpsCam;
    public Transform lookAt;
    
    void Update()
    {
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
        //Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);
        //Debug.DrawRay(lineOrigin, lookAt.forward * weaponRange, Color.red);
    }
}
