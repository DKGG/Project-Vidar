using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleManager : MonoBehaviour
{
    List<string> scenes;
    [SerializeField] GameObject portaloff;
    [SerializeField] GameObject portalon;

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
        if (scenes.Contains("teste"))
        {
            portaloff.SetActive(false);
            portalon.SetActive(true);
        }
    }
}
