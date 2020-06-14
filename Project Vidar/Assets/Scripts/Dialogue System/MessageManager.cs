using System.Collections;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI messageText;
    [SerializeField]
    private TextMeshProUGUI helperText;
    [SerializeField]
    private Animator messageAnimator;
    [SerializeField]
    private Animator helperAnimator;

    [HideInInspector]
    public string message = "";
    private bool messageDisplayed = false;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void DisplayMessage(string message, string helperMessage = "", float helperSeconds = 0)
    {
        // Prevent the same message being shown more than one time
        if (!messageDisplayed)
        {
            messageDisplayed = true;
            this.message = message;

            messageAnimator.SetBool("IsOpen", true);
            audioManager.Play("popUp");
           
            StartCoroutine(TypeMessage(message, messageText));
            
            if (!helperMessage.Equals("") && helperSeconds > 0)
            {
                StartCoroutine(HandleHelperMessage(helperMessage, helperSeconds));
            }
        }
    }

    // TODO animate on a more efficient way?
    IEnumerator TypeMessage(string message, TextMeshProUGUI textElement)
    {
        textElement.text = "";

        foreach (char letter in message.ToCharArray())
        {
            textElement.text += letter;
            yield return null;
        }
    }

    IEnumerator HandleHelperMessage(string helperMessage, float helperSeconds)
    {
        yield return new WaitForSeconds(helperSeconds);
        
        helperAnimator.SetBool("IsOpen", true);
        audioManager.Play("popUp");
        
        StartCoroutine(TypeMessage(helperMessage, helperText));
    }

    public void DismissMessage()
    {
        messageDisplayed = false;
        
        messageAnimator.SetBool("IsOpen", false);
        helperAnimator.SetBool("IsOpen", false);
        
        StopAllCoroutines();
    }
}
