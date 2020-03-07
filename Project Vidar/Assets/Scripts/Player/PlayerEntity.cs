using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEntity
{
    #region Player States Variables
    private static bool isAlive;
     
    #endregion

    #region Player Special Abilities Variables
    private static bool hasForce;
    private static bool hasDash;
    private static bool hasDoubleJump;
    private static bool hasFreeze;
    #endregion

    #region Movement States Variables
    private static bool isGrounded;
    private static bool isDashing;
    private static bool isLocked;
    private static bool isWalking;
    private static bool isIdle;
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
    public static bool getHasForce()
    {
        return hasForce;
    }

    public static void setHasForce(bool value)
    {
        hasForce = value;
    }

    public static bool getHasDash()
    {
        return hasDash;
    }

    public static void setHasDash(bool value)
    {
        hasDash = value;
    }

    public static bool getHasDoubleJump()
    {
        return hasDoubleJump;
    }

    public static void setHasDoubleJump(bool value)
    {
        hasDoubleJump = value;
    }

    public static bool getHasFreeze()
    {
        return hasFreeze;
    }

    public static void setHasFreeze(bool value)
    {
        hasFreeze = value;
    }

    #endregion

    #region Movement States Methods
    public static bool GetGrounded()
    {
        return isGrounded;
    }

    public static void SetGrounded(bool value)
    {
        isGrounded = value;
    }

    public static bool GetDashing()
    {
        return isDashing;
    }

    public static void SetDashing(bool value)
    {
        isDashing = value;
    }

    public static bool GetLocked()
    {
        return isLocked;
    }

    public static void SetLocked(bool value)
    {
        isLocked = value;
    }

    public static bool GetIswalking()
    {
        return isWalking;
    }

    public static void SetWalking(bool value)
    {
        isWalking = value;
    }

    public static bool GetIdle()
    {
        return isIdle;
    }

    public static void SetIdle(bool value)
    {
        isIdle = value;
    }

    public static bool GetJumping()
    {
        return isJumping;
    }

    public static void SetJumping(bool value)
    {
        isJumping = value; 
    }
    #endregion 

}
