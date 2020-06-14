using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaShaderAnimation : MonoBehaviour
{
    [SerializeField] float smoothTime = 1f;
    [SerializeField] float alphaSpeed = 0f;
    public bool spellDown = true;
    public bool spellUp = false;

    Renderer rend;
    float alpha;
    private float maxAlpha;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (gameObject.CompareTag("charge"))
        {
            rend.material.shader = Shader.Find("Custom/DiscardFragment");
            maxAlpha = 0.5f;
        }
        else
        {
            rend.material.shader = Shader.Find("Custom/RimMiscellaneous");
            maxAlpha = 1;
        }
    }

    void Update()
    {
        if (spellDown)
        {
            alpha = Mathf.SmoothDamp(rend.material.GetFloat("_Alpha"), 0, ref alphaSpeed, smoothTime);
            rend.material.SetFloat("_Alpha", alpha);
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        if (spellUp)
        {
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            rend.material.SetFloat("_Alpha", maxAlpha);
        }
    }
}
