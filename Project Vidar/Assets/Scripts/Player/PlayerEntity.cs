using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEntity
{
    #region Player States Variables
    private static bool isAlive;
    private static bool isIdle;
    private static bool isInside;
    private static bool isGrounded;
    private static bool wantToThrow;
    private static bool islockedInContinuous;
    private static bool islockedInSimple;

    #endregion

    #region Player Special Abilities Variables
    private static bool canForce;
    private static bool canDash;
    private static bool canDoubleJump;
    private static bool canFreeze;
    #endregion

    #region Movement States Variables
    
    private static bool isDashing;
    private static bool isLocked;
    private static bool isWalking;    
    private static bool isJumping;
    
    #endregion

    #region Player States Methods
    public static bool getIsAlive()
    {
        return isAlive;
    }

    public static void setIsAlive(bool value)
    {
        isAlive = value;
    }
    #endregion

    #region Player Special Abilities Methods
    public static bool getCanForce()
    {
        return canForce;
    }

    public static void setCanForce(bool value)
    {
        canForce = value;
    }

    public static bool getCanDash()
    {
        return canDash;
    }

    public static void setCanDash(bool value)
    {
        canDash = value;
    }

    public static bool getCanDoubleJump()
    {
        return canDoubleJump;
    }

    public static void setCanDoubleJump(bool value)
    {
        canDoubleJump = value;
    }

    public static bool getCanFreeze()
    {
        return canFreeze;
    }

    public static void setCanFreeze(bool value)
    {
        canFreeze = value;
    }

    public static bool getIsInside()
    {
        return isInside;
    }

    public static void setIsInside(bool value)
    {
        isInside = value;
    }

    public static bool getWantToThrow()
    {
        return wantToThrow;
    }
    public static void setWantToThrow(bool value)
    {
        wantToThrow = value;
    }

    public static bool getIsLockedInContinuous()
    {
        return islockedInContinuous;
    }
    public static void setIsLockedInContinuous(bool value)
    {
        islockedInContinuous = value;
    }

    public static bool getIsLockedInSimple()
    {
        return islockedInSimple;
    }
    public static void setIsLockedInSimple(bool value)
    {
        islockedInSimple = value;
    }
    #endregion

    #region Movement States Methods
    public static bool getGrounded()
    {
        return isGrounded;
    }

    public static void setGrounded(bool value)
    {
        isGrounded = value;
    }

    public static bool getDashing()
    {
        return isDashing;
    }

    public static void setDashing(bool value)
    {
        isDashing = value;
    }

    public static bool getLocked()
    {
        return isLocked;
    }

    public static void setLocked(bool value)
    {
        isLocked = value;
    }

    public static bool getIswalking()
    {
        return isWalking;
    }

    public static void setWalking(bool value)
    {
        isWalking = value;
    }

    public static bool getIdle()
    {
        return isIdle;
    }

    public static void setIdle(bool value)
    {
        isIdle = value;
    }

    public static bool getJumping()
    {
        return isJumping;
    }

    public static void setJumping(bool value)
    {
        isJumping = value; 
    }
    #endregion


    #region Inputs
    public static bool getButtonJump()
    {
        return Input.GetButtonDown("Jump");
    }

    public static bool getKeyLeftShift()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public static bool getKeyE()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    public static bool getKeyQ()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public static bool getKeyQHeld()
    {
        return Input.GetKey(KeyCode.Q);
    }
    public static float checkInputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public static float checkInputVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }
    #endregion
}
