using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dashRune;
    [SerializeField]
    private GameObject strengthRune;
    [SerializeField]
    private GameObject doubleJumpRune;
    [SerializeField]
    private GameObject freezeRune;

    private CanvasGroup dashCanvas;
    private CanvasGroup strengthCanvas;
    private CanvasGroup doubleJumpCanvas;
    private CanvasGroup freezeCanvas;

    private bool dashCorroutineStarted = false;
    private bool strengthCorroutineStarted = false;
    private bool doubleJumpCorroutineStarted = false;
    private bool freezeCorroutineStarted = false;


    private void Awake()
    {
        dashCanvas = dashRune.GetComponent<CanvasGroup>();
        strengthCanvas = strengthRune.GetComponent<CanvasGroup>();
        doubleJumpCanvas = doubleJumpRune.GetComponent<CanvasGroup>();
        freezeCanvas = freezeRune.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(PlayerEntity.getDashing() && !dashCorroutineStarted)
        {
            StartCoroutine(IncreaseAlpha(dashCanvas));
        } else if (!PlayerEntity.getDashing() && dashCorroutineStarted)
        {
            StartCoroutine(DecreaseAlpha(dashCanvas));
        }

        if (!PlayerEntity.getGrounded() && PlayerEntity.getJumping() && !PlayerEntity.getIsOnDialogue() && !doubleJumpCorroutineStarted)
        {
            StartCoroutine(IncreaseAlpha(doubleJumpCanvas));
        }
        else if (PlayerEntity.getGrounded() && !PlayerEntity.getJumping())
        {
            StartCoroutine(DecreaseAlpha(doubleJumpCanvas));
        }
    }

    IEnumerator IncreaseAlpha(CanvasGroup canvas)
    {
        Debug.Log("dasshhhh");
        doubleJumpCorroutineStarted = true;
        dashCorroutineStarted = true;
        while (canvas.alpha < 1)
        {
            canvas.alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DecreaseAlpha(CanvasGroup canvas)
    {
        Debug.Log("!!!dasshhhh");
        while (canvas.alpha > 0.5f)
        {
            canvas.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        doubleJumpCorroutineStarted = false;
        dashCorroutineStarted = false;

    }
}
