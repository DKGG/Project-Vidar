using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousBox : MonoBehaviour
{
    [SerializeField] directionForceApply dirForce;

    public Transform checaChaoCenter;
    public Transform checaChao;

    LockOnBox lockOnBox;

    bool veloContinua;
    bool isLocked = false;
    bool readyToPush = false;
    bool colidiu = false;
    bool isInside = false;
    bool isGrounded = true;

    Rigidbody rb;



    Vector3 wayToGo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //checa chao
        isGrounded = Physics.Linecast(checaChaoCenter.position, checaChao.position);
        //checa chao

        if (veloContinua)//se bater no objeto
        {
            if (colidiu == false)
            {
                rb.velocity = wayToGo * 5000 * Time.deltaTime;
            }
            else
            {

                if (Mathf.Abs(rb.velocity.x) < 1 || Mathf.Abs(rb.velocity.y) < 1 || Mathf.Abs(rb.velocity.z) < 1)
                {
                    Vector3 parar = new Vector3(0, Physics.gravity.y, 0);
                    rb.velocity = parar;
                    if (Mathf.Abs(rb.velocity.x) < 1 && Mathf.Abs(rb.velocity.y) < 1 && Mathf.Abs(rb.velocity.z) < 1)
                    {
                        rb.velocity = Vector3.zero;
                    }

                }

            }
        }
    }

    IEnumerator timeToApplyForce()
    {
        yield return new WaitForSeconds(0.6f);
        switch (dirForce)
        {
            //exemplo de como fazer a velocidade continua
            case directionForceApply.foward:
                veloContinua = true;
                wayToGo = -transform.forward;
                break;
            //exemplo de como fazer a velocidade aumentar e diminuir dps de um tempo
            case directionForceApply.up:
                //rb.velocity = Vector3.up * streghtForce * Time.deltaTime;
                veloContinua = true;
                wayToGo = transform.up;
                break;

        }

        yield return new WaitForSeconds(0.6f);
        readyToPush = false;
        isLocked = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("paraBloco"))
        {
            colidiu = true;
            veloContinua = false;
            //readyToPush = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colidiu = false;
            veloContinua = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
        }
    }



    enum directionForceApply
    {

        foward,
        up,
    };
}
