using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] float DashDistance = 10f;
    [SerializeField] int dragIntensity = 8;
    [SerializeField] GameObject cam;

    Vector3 dashDirection;
    Vector3 dashVelocity;
   
    Rigidbody rb;
    WaitForSeconds dashDuration = new WaitForSeconds(1.0f);

    void Start()
    {
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
        rb.velocity = dashVelocity;         // Adiciona o novo vetor na velocidade do Rigidbody
        yield return dashDuration;
        PlayerEntity.setDashing(false);
        rb.drag = 0;
    }

    private void dashVariables()
    {
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
}
