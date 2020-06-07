﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private Image dialogueEmote;
    [SerializeField]
    private Animator animator;

    private string currentScene;
    private bool crIsRunning = false;
    private Dialogue oldDialogue;
    private Dialogue dialogue;
    private Queue<Dialogue> dialogues;
    private Sprite dialogueEmoteSprite;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = new Queue<Dialogue>();
    }
    void Update()
    {
        if (PlayerEntity.getIsOnDialogue() && PlayerEntity.getButtonJump())
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(DialogueCollection dialogueList)
    {
        // Lock player movement and by so make way to use Jump/Space bar key here
        PlayerEntity.setIsOnDialogue(true);
        PlayerEntity.setCanPlayIdleAnim(true);
        AnimatorManager.setStateIdle();
        FindObjectOfType<AudioManager>().Stop("grass");
        FindObjectOfType<AudioManager>().Stop("wood");


        dialogues.Clear();
        currentScene = dialogueList.sceneName;

        foreach (Dialogue dialogue in dialogueList.dialogues)
        {
            dialogues.Enqueue(dialogue);
        }

        animator.SetBool("IsOpen", true);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //FindObjectOfType<AudioManager>().Play("popUp");
        // End dialogue if there is no coroutines and no dialogues
        if (dialogues.Count == 0 && !crIsRunning)
        {
            EndDialogue();
            return;
        }

        // If the next sentence is being called before typing animation coroutine is over just display all text and stop coroutine
        // That way, user can cancel the animation and move to the next dialogue like in many other games
        if (crIsRunning)
        {
            dialogueText.text = oldDialogue.sentence;
            StopAllCoroutines();
            crIsRunning = false;
        }
        else
        {
            dialogue = dialogues.Dequeue();
            nameText.text = dialogue.name;
            string path = "Assets/Resources/Emotes/" + dialogue.emote + ".png";
            dialogueEmoteSprite = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
            Debug.Log(dialogueEmoteSprite);
            dialogueEmote.sprite = dialogueEmoteSprite;
            StartCoroutine(TypeSentence(dialogue.sentence));
        }
        oldDialogue = dialogue;
    }

    // TODO animate on a more efficient way?
    IEnumerator TypeSentence(string sentence)
    {
        //FindObjectOfType<AudioManager>().Play("typing");
        crIsRunning = true;
        dialogueText.text = "";
        int i = 0;
        foreach (char letter in sentence.ToCharArray())
        {
            if(i % 4 == 0)
            {
                FindObjectOfType<AudioManager>().Play("typing");
            }
            i++;
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        //FindObjectOfType<AudioManager>().Stop("typing");

        crIsRunning = false;
    }

    void EndDialogue()
    {
        PlayerEntity.setIsOnDialogue(false);

        animator.SetBool("IsOpen", false);
    }
}