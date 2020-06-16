using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleManager : MonoBehaviour
{
    List<string> scenes;
    [SerializeField] GameObject portaloff;
    [SerializeField] GameObject portalon;
    [SerializeField] GameObject fog;
    [SerializeField] GameObject cena4Trigger;
    [SerializeField] GameObject messageCena4;

    // Start is called before the first frame update
    void Awake()
    {
        scenes = GameObject.FindObjectOfType<DialogueManager>().endedScenes;
        portalon.SetActive(false);
        portaloff.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (scenes.Contains("Cena3"))
        {
            portaloff.SetActive(false);
            portalon.SetActive(true);
            fog.SetActive(false);
            cena4Trigger.SetActive(true);
        }

        if (scenes.Contains("Cena4"))
        {
            messageCena4.SetActive(true);
        }

    }
}
