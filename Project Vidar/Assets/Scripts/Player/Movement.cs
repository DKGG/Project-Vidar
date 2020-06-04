using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocity = 5f;
    public float turnSpeed = 100f;

    public Transform verificaFrente;

    Vector2 input;
    float angle;
    Quaternion targetRotation;
    Transform cam;
    RaycastHit objectHit;
    public bool grassStep;
    public bool woodStep;
    bool somethingInFrontOfPlayer;
    public String sound;
    private void Start()
    {
        sound = "grass";
        cam = Camera.main.transform;
    }

    private void Update()
    {
        //linecastall
        //raycasthit
        //noNorte = Physics.Linecast(ponto3.position, ponto4.position, Player);
        //objectHit = Physics.Linecast(gameObject.transform.position, verificaFrente.position);
        somethingInFrontOfPlayer = Physics.Linecast(gameObject.transform.position,verificaFrente.position);

        //if(somethingInFrontOfPlayer == true)
        //{
        //    Debug.Log("Tem algo na frente");
        //    input.x = PlayerEntity.checkInputHorizontal() * 0;
        //    input.y = PlayerEntity.checkInputVertical() * 0;

        //}      

        getInput();

        if (Math.Abs(input.x) < 1 && Math.Abs(input.y) < 1)
        {
            if (!PlayerEntity.getJumping() && !PlayerEntity.getIsFalling() && !PlayerEntity.getDashing())
            {
                PlayerEntity.setIsPlayingGrassStep(false);
                //grassStep = false;
                AnimatorManager.setStateIdle();
                FindObjectOfType<AudioManager>().Stop(sound);
            }
            return;
        }

        if (!PlayerEntity.getJumping() && !PlayerEntity.getIsFalling() && !PlayerEntity.getDashing())
        {
            // PlayerEntity.setWalking(true);
            AnimatorManager.setStateRun();
            if (!PlayerEntity.getisPlayingGrassStep() && PlayerEntity.getGrounded())
            {
                FindObjectOfType<AudioManager>().Play(sound);
                PlayerEntity.setIsPlayingGrassStep(true);
                //grassStep = true;
            }
        }
        if (!PlayerEntity.getGrounded())
        {
            PlayerEntity.setIsPlayingGrassStep(false);
            //grassStep = false;d   
            FindObjectOfType<AudioManager>().Stop(sound);
        }

        calculateDirection();
        rotate();

        if (somethingInFrontOfPlayer == false)
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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    sound = collision.gameObject.tag;
    //}
}
