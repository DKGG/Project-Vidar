using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameObject firstMessage;
    void Start()
    {
        firstMessage = GameObject.Find("MoveMessage");
        firstMessage.SetActive(false);
    }

    void Update()
    {
        if (!PlayerEntity.getIsOnDialogue())
        {
            firstMessage.SetActive(true);
        }
    }
}
