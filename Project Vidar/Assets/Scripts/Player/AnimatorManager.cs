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
        dash
    }

    private static AnimState states;
    public static Animator control;
    // Start is called before the first frame update
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
    #endregion

    private void checkStates()
    {
        switch (states)
        {
            case AnimState.idle:
                if (!PlayerEntity.getDashing())
                {
                    PlayerEntity.setCanPlayDashAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayWalkAnim(false);
                    PlayerEntity.setCanPlayIdleAnim(true);
                }
                break;
            case AnimState.run:
                if (!PlayerEntity.getDashing())
                {
                    PlayerEntity.setCanPlayIdleAnim(false);
                    PlayerEntity.setCanPlayDashAnim(false);
                    PlayerEntity.setCanPlayJumpAnim(false);
                    PlayerEntity.setCanPlayWalkAnim(true);
                }
                break;
            case AnimState.jump:
                PlayerEntity.setCanPlayIdleAnim(false);
                PlayerEntity.setCanPlayDashAnim(false);
                PlayerEntity.setCanPlayWalkAnim(false);
                PlayerEntity.setCanPlayJumpAnim(true);
                break;
            case AnimState.dash:
                PlayerEntity.setCanPlayIdleAnim(false);
                PlayerEntity.setCanPlayWalkAnim(false);
                PlayerEntity.setCanPlayJumpAnim(false);
                PlayerEntity.setCanPlayDashAnim(true);
                break;
        }
    }

    public static void setState(String state, Boolean condition)
    {
        control.SetBool(state, condition);
    }
}
