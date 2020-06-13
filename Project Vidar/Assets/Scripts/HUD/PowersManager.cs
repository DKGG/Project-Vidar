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

    public static CanvasGroup dashCanvas;
    public static CanvasGroup strengthCanvas;
    public static CanvasGroup doubleJumpCanvas;
    public static CanvasGroup freezeCanvas;

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
        //DASH
        if(PlayerEntity.getDashing() && !dashCorroutineStarted)
        {
            dashCorroutineStarted = true;
            StartCoroutine(IncreaseAlpha(dashCanvas));
        } 
        //DOUBLEJUMP    
        if (!PlayerEntity.getGrounded() && PlayerEntity.getJumping() && !PlayerEntity.getIsOnDialogue() && !doubleJumpCorroutineStarted)
        {
            doubleJumpCorroutineStarted = true;
            StartCoroutine(IncreaseAlpha(doubleJumpCanvas));
        }
        //else if (PlayerEntity.getGrounded() && !PlayerEntity.getJumping())
        //{
        //    StartCoroutine(DecreaseAlpha(doubleJumpCanvas));
        //}
        //STRENGTH
        if(PlayerEntity.getIsLockedInContinuous())
        {
            if (!strengthCorroutineStarted)
            {
                strengthCorroutineStarted = true;
                StartCoroutine(IncreaseAlpha(strengthCanvas));
            }
        }
        if (!PlayerEntity.getIsLockedInContinuous())
        {
            strengthCorroutineStarted = false;
        }
        //FREEZE
        if (PlayerEntity.getIsFreezing())
        {
            freezeCorroutineStarted = true;
            StartCoroutine(IncreaseAlpha(freezeCanvas));
        }
    }

    IEnumerator IncreaseAlpha(CanvasGroup canvas)
    {
        //doubleJumpCorroutineStarted = true;
        while (canvas.alpha < 1)
        {
            canvas.alpha += 0.1f;
            if (!freezeCorroutineStarted && !dashCorroutineStarted && !strengthCorroutineStarted && !doubleJumpCorroutineStarted)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        //se ele comecar alguma coroutine que desative rapido e nao quando o player apertar, tratar aqui pra nao flickar no update
        if (freezeCorroutineStarted)
        {
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(DecreaseAlpha(freezeCanvas));
        }
        if (dashCorroutineStarted)
        {
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(DecreaseAlpha(dashCanvas));
        }
        if (strengthCorroutineStarted)
        {
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(DecreaseAlpha(strengthCanvas));
        }
        if (doubleJumpCanvas)
        {
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(DecreaseAlpha(doubleJumpCanvas));
        }
    }

    IEnumerator DecreaseAlpha(CanvasGroup canvas)
    {
        while (canvas.alpha > 0.3f)
        {
            canvas.alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        doubleJumpCorroutineStarted = false;
        dashCorroutineStarted = false;
        //strengthCorroutineStarted = false;
        freezeCorroutineStarted = false;

    }
}
