using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocity = 5f;
    public float turnSpeed = 100f;

    public Transform verificaFrente;

    public bool grassStep;
    public bool woodStep;
    public String sound;

    bool somethingInFrontOfPlayer;
    Vector2 input;
    float angle;
    Quaternion targetRotation;
    Transform cam;
    RaycastHit objectHit;

    private void Start()
    {
        //sound = "grass";
        cam = Camera.main.transform;
    }

    private void Update()
    {
        Debug.Log(sound);
        //linecastall
        //raycasthit
        //noNorte = Physics.Linecast(ponto3.position, ponto4.position, Player);
        //objectHit = Physics.Linecast(gameObject.transform.position, verificaFrente.position);

        somethingInFrontOfPlayer = Physics.Linecast(gameObject.transform.position,verificaFrente.position);

        getInput();

        if (Math.Abs(input.x) < 1 && Math.Abs(input.y) < 1)
        {
            if (!PlayerEntity.getJumping() && !PlayerEntity.getIsFalling() && !PlayerEntity.getDashing())
            {
                PlayerEntity.setIsPlayingGrassStep(false);
                PlayerEntity.setisPlayingStoneStep(false);
                PlayerEntity.setisPlayingWoodStep(false);;
                AnimatorManager.setStateIdle();
                FindObjectOfType<AudioManager>().Stop(sound);
            }
            return;
        }

        if (!PlayerEntity.getJumping() && !PlayerEntity.getIsFalling() && !PlayerEntity.getDashing())
        {
            AnimatorManager.setStateRun();
            //if (!PlayerEntity.getisPlayingGrassStep() && PlayerEntity.getGrounded())
            //{
            //    //FindObjectOfType<AudioManager>().Play(sound);
            //    //PlayerEntity.setIsPlayingGrassStep(true);
            //    //grassStep = true;
            //}
        }

        if (!PlayerEntity.getGrounded())
        {
            PlayerEntity.setIsPlayingGrassStep(false);
            PlayerEntity.setisPlayingStoneStep(false);
            PlayerEntity.setisPlayingWoodStep(false); 
            FindObjectOfType<AudioManager>().Stop(sound);
        }

        calculateDirection();
        rotate();

        if (!somethingInFrontOfPlayer)
        {
            move();
        }        
    }

    private void move(){
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    private void rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed*Time.deltaTime);
    }

    private void getInput()
    {
        input.x = PlayerEntity.checkInputHorizontal();
        input.y = PlayerEntity.checkInputVertical();
    }

    void calculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    private void OnCollisionStay(Collision collision)
    {
        sound = collision.gameObject.tag;
        if (collision.gameObject.tag.Equals("wood") && !PlayerEntity.getisPlayingWoodStep() && !PlayerEntity.getDashing() && !PlayerEntity.getIsOnDialogue())
        {
            FindObjectOfType<AudioManager>().stopAll();
            FindObjectOfType<AudioManager>().Play(sound);
            PlayerEntity.setisPlayingWoodStep(true);
        }
        else if (collision.gameObject.tag.Equals("grass") && !PlayerEntity.getisPlayingGrassStep() && !PlayerEntity.getDashing() && !PlayerEntity.getIsOnDialogue())
        {
            FindObjectOfType<AudioManager>().stopAll();
            FindObjectOfType<AudioManager>().Play(sound);
            PlayerEntity.setIsPlayingGrassStep(true);
        }
        else if (collision.gameObject.tag.Equals("stone") && !PlayerEntity.getisPlayingStoneStep() && !PlayerEntity.getDashing() && !PlayerEntity.getIsOnDialogue())
        {
            FindObjectOfType<AudioManager>().stopAll();
            FindObjectOfType<AudioManager>().Play(sound);
            PlayerEntity.setisPlayingStoneStep(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("SoundTriggerTemple") && !FindObjectOfType<AudioManager>().getAmbientThatIsPlaying().Equals("ambientTemple"))
        {
            FindObjectOfType<AudioManager>().stopAllAmbients();
            FindObjectOfType<AudioManager>().Play("ambientTemple");
        } else if (other.gameObject.name.Equals("SoundTriggerTutorial") && !FindObjectOfType<AudioManager>().getAmbientThatIsPlaying().Equals("ambient"))
        {
            FindObjectOfType<AudioManager>().stopAllAmbients();
            FindObjectOfType<AudioManager>().Play("ambient");
        }
    }
}
