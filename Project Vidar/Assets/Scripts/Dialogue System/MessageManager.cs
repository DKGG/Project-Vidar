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

    private bool messageDisplayed = false;

    public void DisplayMessage(string message, string helperMessage = "", float helperSeconds = 0)
    {
        // Prevent the same message being shown more than one time
        if (!messageDisplayed)
        {
            messageDisplayed = true;
            messageAnimator.SetBool("IsOpen", true);
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
