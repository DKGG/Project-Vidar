using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEntity
{
    //VARIABLES
    #region Player States Variables

    private static bool isAlive;
    private static bool isIdle;
    private static bool isInside;
    private static bool isGrounded;
    private static bool isFalling;
    private static bool wantToThrow;
    private static bool islockedInContinuous;
    private static bool islockedInSimple;
    private static bool islockedInNorth;
    private static bool islockedInSouth;
    private static bool islockedInWest;
    private static bool islockedInEast;
    private static bool isOnDialogue;

    private static GameObject BoxLocked;

    #endregion

    #region Player Special Abilities Variables

    private static bool canForce;
    private static bool canDash = true;
    private static bool canDoubleJump = true;
    private static bool canFreeze;
    private static bool canThrow;

    #endregion

    #region Movement States Variables
    
    private static bool isDashing;
    private static bool isLocked;
    private static bool isWalking;
    private static bool isJumping;

    #endregion

    #region Animator States Variables

    private static bool canPlayDashAnim;
    private static bool canPlayWalkAnim;
    private static bool canPlayJumpAnim;
    private static bool canPlayIdleAnim;
    private static bool canPlayFallAnim;

    #endregion

    #region sounds States Variables

    private static bool isPlayingGrassStep;
    private static bool isPlayingStoneStep;
    private static bool isPlayingWaterStep;

    #endregion

    //METHODS

    #region Player States Methods  
    public static bool getIsAlive()
    {
        return isAlive;
    }

    public static void setIsAlive(bool value)
    {
        isAlive = value;
    }

    public static bool getIsOnDialogue()
    {
        return isOnDialogue;
    }

    public static void setIsOnDialogue(bool value)
    {
        isOnDialogue = value;
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

    public static bool getCanThrow()
    {
        return canThrow;
    }

    public static void setCanThrow(bool value)
    {
        canThrow = value;
    }

    /*
     * Player will start the game with these abilities
     */

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

    /********/

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

    public static bool getIsLockedInNorth()
    {
        return islockedInNorth;
    }
    public static void setIslockedInNorth(bool value)
    {
        islockedInNorth = value;
    }

    public static bool getIsLockedInSouth()
    {
        return islockedInSouth;
    }
    public static void setIslockedInSouth(bool value)
    {
        islockedInSouth = value;
    }

    public static bool getIsLockedInWest()
    {
        return islockedInWest;
    }
    public static void setIslockedInWest(bool value)
    {
        islockedInWest = value;
    }

    public static bool getIsLockedInEast()
    {
        return islockedInEast;
    }
    public static void setIslockedInEast(bool value)
    {
        islockedInEast = value;
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

    public static bool getIsFalling()
    {
        return isFalling;
    }

    public static void setIsFalling(bool value)
    {
        isFalling = value;
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

    #region Sounds States Methods
    public static bool getisPlayingGrassStep()
    {
        return isPlayingGrassStep;
    }

    public static void setIsPlayingGrassStep(bool value)
    {
        isPlayingGrassStep = value;
    }

    public static bool getisPlayingStoneStep()
    {
        return isPlayingStoneStep;
    }

    public static void setisPlayingStoneStep(bool value)
    {
        isPlayingStoneStep = value;
    }
    public static bool getisPlayingWaterStep()
    {
        return isPlayingWaterStep;
    }

    public static void setisPlayingWaterStep(bool value)
    {
        isPlayingWaterStep = value;
    }
    #region Animator States Methods

    #endregion
    public static bool getCanPlayDashAnim()
    {
        return canPlayDashAnim;
    }

    public static void setCanPlayDashAnim(bool value)
    {
        canPlayDashAnim = value;
    }

    public static bool getCanPlayWalkAnim()
    {
        return canPlayWalkAnim;
    }

    public static void setCanPlayWalkAnim(bool value)
    {
        canPlayWalkAnim = value;
    }

    public static bool getCanPlayJumpAnim()
    {
        return canPlayJumpAnim;
    }

    public static void setCanPlayJumpAnim(bool value)
    {
        canPlayJumpAnim = value;
    }
    public static bool getCanPlayIdleAnim()
    {
        return canPlayIdleAnim;
    }

    public static void setCanPlayIdleAnim(bool value)
    {
        canPlayIdleAnim = value;
    }

    public static bool getCanPlayFallAnim()
    {
        return canPlayFallAnim;
    }

    public static void setCanPlayFallAnim(bool value)
    {
        canPlayFallAnim = value;
    }
    #endregion

    //OBJECTS

    #region GameObjects
    public static GameObject getBoxLocked()
    {
        return BoxLocked;
    }

    public static void setBoxLocked(GameObject obj)
    {
        BoxLocked = obj;
    }
    #endregion

    //INPUTS

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

    public static float checkMouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    public static float checkMouseY()
    {
        return Input.GetAxis("Mouse Y");
    }

    public static bool getKeyX()
    {
        return Input.GetKeyDown(KeyCode.X);
    }
    #endregion
}
