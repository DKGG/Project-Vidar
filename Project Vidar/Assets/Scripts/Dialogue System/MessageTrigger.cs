using UnityEngine;
using UnityEditor;
using System.Collections;

public class MessageTrigger : MonoBehaviour
{
    public bool dismissCurrentMessage;
    public bool displayMessage;

    // Conditional fields handled on MessageTriggerEditor class
    public string message;
    public bool displayHelperMessage;
    public string helperMessage;
    public float helperSeconds;
    public bool dismissAfterSeconds;
    public float dismissSeconds;

    private MessageManager messageManager;

    void Start()
    {
        // TODO use singleton?
        messageManager = FindObjectOfType<MessageManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dismissCurrentMessage)
            {
                if (messageManager.message == message)
                {
                    dismissCurrentMessage = false;
                }
            }

            if (displayMessage && !dismissCurrentMessage)
            {
                TriggerMessage();
            }
            else if (displayMessage && dismissCurrentMessage)
            {
                StartCoroutine("DismissAndDisplayMessage");
            }
            else
            {
                StopAllCoroutines();
                DismissMessage();
            }

            if (dismissAfterSeconds)
            {
                StartCoroutine("DismissMessageAfterSeconds");
            }
        }
    }

    IEnumerator DismissAndDisplayMessage()
    {
        DismissMessage();
        // Give time to MessageManager execute animation
        yield return new WaitForSeconds(0.3f);
        TriggerMessage();
    }

    IEnumerator DismissMessageAfterSeconds()
    {
        yield return new WaitForSeconds(dismissSeconds);
        DismissMessage();
    }

    public void TriggerMessage()
    {
        if (displayHelperMessage)
        {
            messageManager.DisplayMessage(message, helperMessage, helperSeconds);
        }
        else
        {
            messageManager.DisplayMessage(message);
        }
    }

    public void DismissMessage()
    {
        messageManager.DismissMessage();
    }

    [CustomEditor(typeof(MessageTrigger))]
    public class MessageTriggerEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            var messageTrigger = target as MessageTrigger;

            messageTrigger.dismissCurrentMessage = EditorGUILayout.Toggle("Dismiss Curr. Message: ", messageTrigger.dismissCurrentMessage);
            messageTrigger.displayMessage = EditorGUILayout.Toggle("Display Message: ", messageTrigger.displayMessage);

            // If displayMessage is marked show message fields
            if (messageTrigger.displayMessage)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("Message: ");
                messageTrigger.message = EditorGUILayout.TextArea(messageTrigger.message, GUILayout.Height(80));
                messageTrigger.displayHelperMessage = EditorGUILayout.Toggle("Display Helper Msg.", messageTrigger.displayHelperMessage);
                // If diplayHelperMessage is marked show helper message fields
                if (messageTrigger.displayHelperMessage)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PrefixLabel("Helper Message: ");
                    messageTrigger.helperMessage = EditorGUILayout.TextArea(messageTrigger.helperMessage, GUILayout.Height(80));
                    EditorGUILayout.PrefixLabel("After Seconds: ");
                    messageTrigger.helperSeconds = EditorGUILayout.FloatField(messageTrigger.helperSeconds);
                    EditorGUI.indentLevel--;
                }
                messageTrigger.dismissAfterSeconds = EditorGUILayout.Toggle("Dismiss After Sec.", messageTrigger.dismissAfterSeconds);
                if (messageTrigger.dismissAfterSeconds)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PrefixLabel("After Seconds: ");
                    messageTrigger.dismissSeconds = EditorGUILayout.FloatField(messageTrigger.dismissSeconds);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }

            // Prevent Unity to restore fields values to prefab values and enable "CTRL-Z"
            Undo.RecordObject(messageTrigger, "Set Value");
            EditorUtility.SetDirty(messageTrigger);
        }
    }
}
