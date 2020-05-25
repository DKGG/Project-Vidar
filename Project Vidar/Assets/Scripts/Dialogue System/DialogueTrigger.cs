using UnityEngine;
using System.IO;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueCollection dialogueList;
    private bool dialogueExecuted = false;

    [SerializeField]
    private string fileName;
    void Start()
    {
        // Open a json.file on the provided path and put it on a DialogueCollection object
        // TODO use Application.dataPath?

        string dialoguePath = Directory.GetCurrentDirectory() + "/Assets/" + fileName;
        using (StreamReader stream = new StreamReader(dialoguePath))
        {
            string json = stream.ReadToEnd();
            stream.Close();
            dialogueList = JsonUtility.FromJson<DialogueCollection>(json);
        }
    }

    private void Update()
    {
        // Key pressed ou sei lá
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogueExecuted)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        dialogueExecuted = true;
        // TODO use singleton?
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueList);
    }
}