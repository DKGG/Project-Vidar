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
        falling,
        freezing,
        channeling
    }
    private static AnimState states;

    public static Animator control;

    void Start()
    {
        control = GetComponent<Animator>();
        control.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        checkStates();
        updateAnim();
        if (PlayerEntity.getLocked() || PlayerEntity.getIsInsideOfContinuous() || PlayerEntity.getIsLockedInSimple())
        {
            setStateChanneling();
        }
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
        #region Animation Freezing
        if (PlayerEntity.getCanPlayFreezeAnim())
        {
            AnimatorManager.setState("isFreezing", true);
        }
        if (!PlayerEntity.getCanPlayFreezeAnim())
        {
            AnimatorManager.setState("isFreezing", false);
        }
        #endregion
        #region Animation Channeling
        if (PlayerEntity.getCanPlayChannelingAnim())
        {
            AnimatorManager.setState("isChanneling", true);
        }
        if (!PlayerEntity.getCanPlayChannelingAnim())
        {
            AnimatorManager.setState("isChanneling", false);
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
    public static void setStateFreezing()
    {
        states = AnimState.freezing;
    }
    public static void setStateChanneling()
    {
        states = AnimState.channeling;
    }
    #endregion

    private void checkStates()
    {
        switch (states)
        {
            case AnimState.idle:
                if (!PlayerEntity.getDashing() && !PlayerEntity.getIsFalling() && !PlayerEntity.getIsFreezing() && !PlayerEntity.getLocked() && !PlayerEntity.getIsLockedInSimple() && !PlayerEntity.getIsLockedInSimple())
                {
                    /* Must add Audio Manager to Game Object */
                    // FindObjectOfType<AudioManager>().Stop("grassStep");

                    /* Audio */
                    PlayerEntity.setisPlayingStoneStep(false);
                    PlayerEntity.setIsPlayingGrassStep(false);
                    PlayerEntity.setisPlayingWoodStep(false);

                    /* Animations */
                    PlayerEntity.setCanPlayDashAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayWalkAnim(false);
                    PlayerEntity.setCanPlayFallAnim(false);
                    PlayerEntity.setCanPlayFreezeAnim(false);
                    PlayerEntity.setCanPlayChannelingAnim(false);

                    PlayerEntity.setCanPlayIdleAnim(true);
                }
                break;
            case AnimState.run:
                if (!PlayerEntity.getDashing() && !PlayerEntity.getIsFalling() && !PlayerEntity.getIsFreezing() && !PlayerEntity.getLocked() && !PlayerEntity.getIsLockedInSimple() && !PlayerEntity.getIsLockedInSimple())
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
                    PlayerEntity.setCanPlayFreezeAnim(false);
                    PlayerEntity.setCanPlayChannelingAnim(false);

                    PlayerEntity.setCanPlayWalkAnim(true);
                }
                break;
            case AnimState.jump:
                /* Audio */
                if(!PlayerEntity.getLocked() && !PlayerEntity.getIsLockedInSimple() && !PlayerEntity.getIsLockedInSimple())
                {
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
                    PlayerEntity.setCanPlayFreezeAnim(false);
                    PlayerEntity.setCanPlayChannelingAnim(false);

                    PlayerEntity.setCanPlayJumpAnim(true);
                }
                break;
            case AnimState.dash:
                if (!PlayerEntity.getLocked() && !PlayerEntity.getIsLockedInSimple() && !PlayerEntity.getIsLockedInSimple())
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
                    PlayerEntity.setCanPlayFallAnim(false);
                    PlayerEntity.setCanPlayFreezeAnim(false);
                    PlayerEntity.setCanPlayChannelingAnim(false);

                    PlayerEntity.setCanPlayDashAnim(true);
                }
                break;
            case AnimState.falling:
                if (!PlayerEntity.getGrounded() && !PlayerEntity.getLocked() && !PlayerEntity.getIsLockedInSimple() && !PlayerEntity.getIsLockedInSimple())
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
                    PlayerEntity.setCanPlayFreezeAnim(false);
                    PlayerEntity.setCanPlayChannelingAnim(false);

                    /* States */
                    PlayerEntity.setJumping(false);
                    PlayerEntity.setIsFalling(true);

                    PlayerEntity.setCanPlayFallAnim(true);
                }
                break;
            case AnimState.freezing:
                if (!PlayerEntity.getLocked() && !PlayerEntity.getIsLockedInSimple() && !PlayerEntity.getIsLockedInSimple())
                {
                    PlayerEntity.setCanPlayIdleAnim(false);
                    PlayerEntity.setCanPlayWalkAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayFallAnim(false);
                    PlayerEntity.setCanPlayDashAnim(false);
                    PlayerEntity.setCanPlayChannelingAnim(false);

                    PlayerEntity.setCanPlayFreezeAnim(true);
                }
                break;
            case AnimState.channeling:
                PlayerEntity.setCanPlayIdleAnim(false);
                PlayerEntity.setCanPlayWalkAnim(false);
                PlayerEntity.setCanPlayJumpAnim(false);
                PlayerEntity.setCanPlayFallAnim(false);
                PlayerEntity.setCanPlayDashAnim(false);
                PlayerEntity.setCanPlayFreezeAnim(false);

                PlayerEntity.setCanPlayChannelingAnim(true);
                break;
        }
    }

    public static void setState(String state, Boolean condition)
    {
        control.SetBool(state, condition);
    }
}
