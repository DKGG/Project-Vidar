using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaShaderAnimation : MonoBehaviour
{
    [SerializeField] float smoothTime = 1f;
    [SerializeField] float alphaSpeed = 0f;
    public bool spellDown = false;

    Renderer rend;
    float alpha;

    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.shader = Shader.Find("Custom/RimMiscellaneous");
    }

    void Update()
    {
        if (spellDown)
        {
            alpha = Mathf.SmoothDamp(rend.material.GetFloat("_Alpha"), 0, ref alphaSpeed, smoothTime);
            rend.material.SetFloat("_Alpha", alpha);
        }
    }
}
