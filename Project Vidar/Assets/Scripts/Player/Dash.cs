using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] float DashDistance = 10f;
    [SerializeField] int dragIntensity = 8;

    Vector3 dashDirection;
    Vector3 dashVelocity;

    InputController inputController;
    Rigidbody rb;
    WaitForSeconds dashDuration = new WaitForSeconds(.5f);

    bool isDashing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputController = GetComponent<InputController>();
    }

    void Update()
    {
        if (inputController.GetKeyLeftShift() && !isDashing)
        {
            dashVariables();

            StartCoroutine(DashReset());
        }
    }

    private IEnumerator DashReset()
    {
        rb.velocity = dashVelocity;         // Adiciona o novo vetor na velocidade do Rigidbody
        yield return dashDuration;
        isDashing = false;
        rb.drag = 0;
    }

    private void dashVariables()
    {
        isDashing = true;
        rb.drag = dragIntensity;

        dashDirection = new Vector3(
            Mathf.Clamp(Mathf.Log(1f / (Time.deltaTime * rb.drag)) / Time.deltaTime, 0, 100),     // X
            0,                                                               // Y
            Mathf.Clamp(Mathf.Log(1f / (Time.deltaTime * rb.drag)) / Time.deltaTime, 0, 100)      // Z
        );

        Debug.Log(dashDirection);

        dashVelocity = Vector3.Scale(
            transform.forward,              // Coloca o dash para a frente do player
            DashDistance * dashDirection    // Força do dash vezes a direção
        );

        Debug.Log(dashVelocity);
    }
}
