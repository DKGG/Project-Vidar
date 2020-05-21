using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private Animator animator;

    private string currentScene;
    private bool crIsRunning = false;
    private Dialogue oldDialogue;
    private Dialogue dialogue;
    private Queue<Dialogue> dialogues;

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
        // PlayerEntity.setLocked(true);

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
            StartCoroutine(TypeSentence(dialogue.sentence));
        }
        oldDialogue = dialogue;
    }

    // TODO animate on a more efficient way?
    IEnumerator TypeSentence(string sentence)
    {
        crIsRunning = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        
        crIsRunning = false;
    }

    void EndDialogue()
    {
        PlayerEntity.setIsOnDialogue(false);
        // PlayerEntity.setLocked(false);

        animator.SetBool("IsOpen", false);
    }
}