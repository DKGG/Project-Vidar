using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrbit : MonoBehaviour
{

    protected Transform _XTForm_camera;
    protected Transform _XTForm_parent;

    protected Vector3 localRotation;
    protected float _cameraDistance = 5f;

    public float mouseSensitivity = 4f;
    public float scrollSensitivity = 2f;
    public float orbitSpeed = 10f;
    public float scrollSpeed = 6f;

    public bool cameraDisable = false;


    // Start is called before the first frame update
    void Start()
    {
        this._XTForm_camera = this.transform;
        this._XTForm_parent = this.transform.parent;
    }

    
    //é late update pq a camera tem que renderizar depois que tudo já tenha sido renderizado na cena
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            cameraDisable = !cameraDisable;
        }
        if (!cameraDisable)
        {
            //Rotaciona a camera baseada nas coordenadas do mouse
            if(Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                //travar a rotação em Y
                localRotation.y = Mathf.Clamp(localRotation.y, 0f, 90f);
            }
            //Zoom da camera com o scroll do mouse
            if(Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

                //scrolla mais se tiver longe e scrolla mto pouco se tiver perto
                ScrollAmount *= (this._cameraDistance * 0.3f);

                this._cameraDistance += ScrollAmount * -1f;

                //não se aproxima mais que 1.5 metros do objeto e não se distância mais que 10 metro dele
                this._cameraDistance = Mathf.Clamp(this._cameraDistance, 3f,10f);                
            }
        }

        //orientações da camera
        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        this._XTForm_parent.rotation = Quaternion.Lerp(this._XTForm_parent.rotation, QT, 0.1f* orbitSpeed);
        if (this._XTForm_camera.localPosition.z != this._cameraDistance * -1f)
        {
            this._XTForm_camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XTForm_camera.localPosition.z, this._cameraDistance * -1f,0.1f*scrollSpeed));
        }

    }
}
