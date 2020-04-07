using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Animations;

public class AnimatorManager : MonoBehaviour
{
    public static Animator control;
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<Animator>();
    }
    private void Update()
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
    }

    public static void setState(String state, Boolean condition)
    {
        control.SetBool(state, condition);
    }
}
