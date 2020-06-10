using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private string checkpoint;
    [SerializeField]
    private TutorialManager tutorialManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialManager.setCheckpoint(checkpoint);
        }
    }
}
