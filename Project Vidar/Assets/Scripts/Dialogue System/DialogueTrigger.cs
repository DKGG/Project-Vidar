using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueCollection dialogueList;
    private bool dialogueExecuted = false;
    private PostProcessVolume postProcessing;
    private AutoExposure autoExposure;

    [SerializeField]
    private string fileName;
    [SerializeField]
    private bool fadeInOut = false;

    void Start()
    {
        // Open a json.file on the provided path and put it on a DialogueCollection object
        if (fileName != null)
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>("Dialogues/" + fileName);
            string jsonString = jsonTextFile.ToString();
            dialogueList = JsonUtility.FromJson<DialogueCollection>(jsonString);
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
        FindObjectOfType<DialogueManager>().StartDialogue(dialogueList, fadeInOut);
    }
}