using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] float DashDistance = 10f;
    float angle;
    [SerializeField] int dragIntensity = 8;
    [SerializeField] GameObject cam;
    Transform cam2;
    Quaternion targetRotation;

    Vector3 dashDirection;
    Vector3 dashVelocity;
   
    Rigidbody rb;
    WaitForSeconds dashDuration = new WaitForSeconds(1.0f);

    void Start()
    {
        cam2 = Camera.main.transform;
        rb = GetComponent<Rigidbody>();        
    }

    void Update()
    {
        if (PlayerEntity.getKeyLeftShift() && !PlayerEntity.getDashing())
        {
            dashVariables();

            StartCoroutine(DashReset());
        }
    }

    private IEnumerator DashReset()
    {
        PlayerEntity.setDashing(true);
        PlayerEntity.setCanPlayDashAnim(true);
        rb.velocity = dashVelocity;         // Adiciona o novo vetor na velocidade do Rigidbody
        yield return dashDuration;
        PlayerEntity.setDashing(false);
        PlayerEntity.setCanPlayDashAnim(false);
        rb.drag = 0;
    }

    private void dashVariables()
    {
        calculateDirection();
      //  rotate();
        rb.drag = dragIntensity;

        dashDirection = new Vector3(
            Mathf.Clamp(Mathf.Log(1f / (Time.deltaTime * rb.drag)) / Time.deltaTime, 0, 100),   // X
            0,                                                                                  // Y
            Mathf.Clamp(Mathf.Log(1f / (Time.deltaTime * rb.drag)) / Time.deltaTime, 0, 100)    // Z
        );

        dashVelocity = Vector3.Scale(
            cam.transform.forward,          // Coloca o dash para a frente do player
            DashDistance * dashDirection    // Força do dash vezes a direção
        );

    }

    void calculateDirection()
    {
        angle = Mathf.Atan2(PlayerEntity.checkInputHorizontal(), PlayerEntity.checkInputVertical());
        angle = Mathf.Rad2Deg * angle;
        angle += cam2.eulerAngles.y;
    }
    //private void rotate()
    //{
    //    targetRotation = Quaternion.Euler(0, angle, 0);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Movement.turnSpeed * Time.deltaTime);
    //}
}
