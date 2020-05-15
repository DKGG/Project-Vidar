using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations;

public class AnimatorManager : MonoBehaviour
{
    private enum AnimState
    {
        idle,
        run,
        jump,
        dash,
        falling
    }
    private static AnimState states;

    public static Animator control;

    void Start()
    {
        control = GetComponent<Animator>();
    }

    private void Update()
    {
        updateAnim();
        checkStates();
    }

    public static void updateAnim()
    {
        #region Animation Walk
        if (PlayerEntity.getCanPlayWalkAnim())
        {
            AnimatorManager.setState("isWalking", true);
        }
        if (!PlayerEntity.getCanPlayWalkAnim())
        {
            AnimatorManager.setState("isWalking", false);
        }
        #endregion

        #region Animation Idle
        if (PlayerEntity.getCanPlayIdleAnim())
        {
            AnimatorManager.setState("isStoped", true);
        }
        if (!PlayerEntity.getCanPlayIdleAnim())
        {
            AnimatorManager.setState("isStoped", false);
        }
        #endregion

        #region Animation Dash
        if (PlayerEntity.getCanPlayDashAnim())
        {
            AnimatorManager.setState("isDashing", true);
        }
        if (!PlayerEntity.getCanPlayDashAnim())
        {
            AnimatorManager.setState("isDashing", false);
        }
        #endregion

        #region Animation Jump
        if (PlayerEntity.getCanPlayJumpAnim())
        {
            AnimatorManager.setState("IsJumping", true);
        }
        if (!PlayerEntity.getCanPlayJumpAnim())
        {
            AnimatorManager.setState("IsJumping", false);
        }
        #endregion

        #region Animation Falling
        if (PlayerEntity.getCanPlayFallAnim())
        {
            AnimatorManager.setState("isFalling", true);
        }
        if (!PlayerEntity.getCanPlayFallAnim())
        {
            AnimatorManager.setState("isFalling", false);
        }
        #endregion
    }

    #region Set states methods
    public static void setStateIdle()
    {
        states = AnimState.idle;
    }
    public static void setStateJump()
    {
        states = AnimState.jump;
    }
    public static void setStateRun()
    {
        states = AnimState.run;
    }
    public static void setStateDash()
    {
        states = AnimState.dash;
    }
    public static void setStateFalling()
    {
        states = AnimState.falling;
    }
    #endregion

    private void checkStates()
    {
        switch (states)
        {
            case AnimState.idle:
                if (!PlayerEntity.getDashing() && !PlayerEntity.getIsFalling())
                {
                    /* Must add Audio Manager to Game Object */
                    // FindObjectOfType<AudioManager>().Stop("grassStep");

                    /* Audio */
                    PlayerEntity.setisPlayingStoneStep(false);
                    PlayerEntity.setIsPlayingGrassStep(false);
                    PlayerEntity.setisPlayingWaterStep(false);

                    /* Animations */
                    PlayerEntity.setCanPlayDashAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayWalkAnim(false);
                    PlayerEntity.setCanPlayFallAnim(false);

                    PlayerEntity.setCanPlayIdleAnim(true);
                }
                break;
            case AnimState.run:
                if (!PlayerEntity.getDashing() && !PlayerEntity.getIsFalling())
                {
                    /* Audio */
                    /*
                    if (!PlayerEntity.getisPlayingGrassStep() && PlayerEntity.getGrounded())
                    {
                        FindObjectOfType<AudioManager>().Play("grassStep");
                        PlayerEntity.setIsPlayingGrassStep(true);
                    }
                    */

                    /* Animations */
                    PlayerEntity.setCanPlayIdleAnim(false);
                    PlayerEntity.setCanPlayDashAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayFallAnim(false);

                    PlayerEntity.setCanPlayWalkAnim(true);
                }
                break;
            case AnimState.jump:
                /* Audio */
                if (PlayerEntity.getisPlayingGrassStep())
                {
                    FindObjectOfType<AudioManager>().Stop("grassStep");
                    PlayerEntity.setIsPlayingGrassStep(false);
                }

                /* Animations */
                PlayerEntity.setCanPlayIdleAnim(false);
                PlayerEntity.setCanPlayDashAnim(false);
                PlayerEntity.setCanPlayWalkAnim(false);
                PlayerEntity.setCanPlayFallAnim(false);

                PlayerEntity.setCanPlayJumpAnim(true);
                break;
            case AnimState.dash:
                /* Audio */
                if (PlayerEntity.getisPlayingGrassStep())
                {
                    FindObjectOfType<AudioManager>().Stop("grassStep");
                    PlayerEntity.setIsPlayingGrassStep(false);
                }

                /* Animations */
                PlayerEntity.setCanPlayIdleAnim(false);
                PlayerEntity.setCanPlayWalkAnim(false);
                PlayerEntity.setCanPlayJumpAnim(false);
                PlayerEntity.setCanPlayFallAnim(false);

                PlayerEntity.setCanPlayDashAnim(true);
                break;
            case AnimState.falling:
                if (!PlayerEntity.getGrounded())
                {
                    /* Audio */
                    if (PlayerEntity.getisPlayingGrassStep())
                    {
                        FindObjectOfType<AudioManager>().Stop("grassStep");
                        PlayerEntity.setIsPlayingGrassStep(false);
                    }

                    /* Animations */
                    PlayerEntity.setCanPlayIdleAnim(false);
                    PlayerEntity.setCanPlayWalkAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayDashAnim(false);

                    /* States */
                    PlayerEntity.setJumping(false);
                    PlayerEntity.setIsFalling(true);

                    PlayerEntity.setCanPlayFallAnim(true);
                }
                break;
        }
    }

    public static void setState(String state, Boolean condition)
    {
        control.SetBool(state, condition);
    }
}
