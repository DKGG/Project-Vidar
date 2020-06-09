using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheShoulderCamera : MonoBehaviour
{
    [Header("Look Properties")]
    [SerializeField] Transform playerTransForm, lookTarget;
    public float rotationSpeed = 10.0f;
    [SerializeField] GameObject playerObject;
    [SerializeField] int mouseMax = 70;
    [SerializeField] int mouseMin = -70;
    [SerializeField] float zoomSpeed = 5f;

    bool changeCam;
    bool moveCam = false;

    public Transform zoomIn;
    public Transform zoomOut;

    Vector3 changeTargetAxis;
    Vector3 OldchangeTargetAxis;

    // Private variables
    private float mouseX, mouseY;
    Transform cameraPivot;
    Transform playerFocus;
    Transform obstruction;
    Transform oldObstruction;

    private void Start()
    {
        cameraPivot = transform.parent;
        changeTargetAxis = new Vector3(2.5f, 0.75f, -16f);
        OldchangeTargetAxis = new Vector3(2.5f, 0.75f, -3.75f);
        playerFocus = playerTransForm;
        obstruction = lookTarget;
        oldObstruction = obstruction;
    }

    private void Update()
    {

        if (PlayerEntity.getKeyX())
        {
            moveCam = !moveCam;
        }

        if (moveCam)
        {
            transform.position = Vector3.Lerp(transform.position, zoomIn.transform.position, Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, zoomOut.transform.position, Time.deltaTime * 5f);
        }

        if (PlayerEntity.getLocked() == true)
        {
            //playerTransForm.position = playerObject.transform.parent.position;            
            playerTransForm = PlayerEntity.getBoxLocked().transform;
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, changeTargetAxis, Time.deltaTime * 5);
            changeCam = true;
        }

        if (PlayerEntity.getLocked() == false)
        {
            //Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, OldchangeTargetAxis, Time.deltaTime * 100);
            playerTransForm = playerFocus;
        }


    }

    private void LateUpdate()
    {
        if (cameraPivot.position == null)
        {
            playerTransForm = playerFocus;
        }

        mouseX += PlayerEntity.checkMouseX() * rotationSpeed;
        mouseY -= PlayerEntity.checkMouseY() * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, mouseMin, mouseMax);

        cameraPivot.position = playerTransForm.position;

        transform.LookAt(lookTarget);
        cameraPivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        ViewObstructed();
    }

    /**
         * 1.Creates Raycast from camera to player
         * 2.Check collision with gameObject
         * 
         * On HIT:
         *  GameObject Mesh Renderer set to Shadows Only
         **/
    private void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, lookTarget.position - transform.position, out hit, 4.5f))
        {

            //Debug.DrawRay(transform.position, lookTarget.position - transform.position, Color.red, 4.5f);
            if (hit.collider.gameObject.tag != "Player" && hit.transform)
            {
                obstruction = hit.transform;
                Camera.main.useOcclusionCulling = false;
                if (obstruction && obstruction.gameObject.GetComponent<MeshRenderer>())
                {
                    obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                    if (Vector3.Distance(obstruction.position, transform.position) >= 3f &&
                        Vector3.Distance(transform.position, lookTarget.position) >= 1.5f)
                    {
                        transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    }
                }
            }
        }
        else
        {
            if (obstruction && obstruction.gameObject.GetComponent<MeshRenderer>())
            {
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            if (Vector3.Distance(transform.position, lookTarget.position) < 4.5f)
            {
                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }

        if(oldObstruction.gameObject.GetComponent<MeshRenderer>() && oldObstruction != obstruction && oldObstruction.transform)
        {
            oldObstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            Camera.main.useOcclusionCulling = true;
        }
        oldObstruction = obstruction;
    }
}
