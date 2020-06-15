using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class fadeInOut : MonoBehaviour
{
    private PostProcessVolume postProcessing;
    private AutoExposure autoExposure;
    public static bool fadeIn;
    public static bool fadeOut;

    // Start is called before the first frame update
    private void Awake()
    {
        postProcessing = GameObject.Find("Main PostProcessingVolume").GetComponent<PostProcessVolume>();
        postProcessing.profile.TryGetSettings(out autoExposure);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            autoExposure.minLuminance.value += 4f * Time.deltaTime;
        }

        if (fadeOut)
        {
            if(autoExposure.minLuminance.value > 0)
            {
                autoExposure.minLuminance.value -= 4f * Time.deltaTime;
            }           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fadeIn = true;
        }
    }
}
