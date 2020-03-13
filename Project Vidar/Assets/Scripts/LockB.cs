using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockB : MonoBehaviour
{
    public Transform checaChao;
    public Transform checaChaoCenter;
    public Transform playerTransform;
    public Transform ponto1;
    public Transform ponto2;
    public Transform ponto3;
    public Transform ponto4;
    public Transform FaceNorte;
    public Transform FaceSul;
    public Transform FaceOeste;
    public Transform FaceLeste;
    public Transform posicao;

    // public bool isInside;
    public bool locka = false;       
    bool noNorte;
    bool noSul;
    bool noOeste;
    bool noLeste;
    public bool islocked;

    public string side = "";

    public LayerMask Player;

    Rigidbody rb;
    boxMovement boxMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxMovement = GetComponent<boxMovement>();
        posicao = FaceNorte;
    }

    void Update()
    {
        if (locka && !PlayerEntity.getLocked())
        {
            noNorte = Physics.Linecast(ponto1.position, ponto4.position, Player);
            noSul = Physics.Linecast(ponto2.position, ponto3.position, Player);
            noOeste = Physics.Linecast(ponto1.position, ponto2.position, Player);
            noLeste = Physics.Linecast(ponto3.position, ponto4.position, Player);

            if (noNorte)
            {
                posicao = FaceNorte;
                side = "norte";
                Debug.Log("" + side);
            }

            if (noSul)
            {
                posicao = FaceSul;
                side = "sul";
                Debug.Log("" + side);
            }
            if (noOeste)
            {
                posicao = FaceOeste;
                side = "oeste";
                Debug.Log("" + side);
            }
            if (noLeste)
            {
                posicao = FaceLeste;
                side = "leste";
                Debug.Log("" + side);
            }

            playerTransform.transform.parent = transform;
            boxMovement.enabled = true;            
            islocked = true;
        }

        if (!locka && PlayerEntity.getLocked())
        {
            Debug.Log("NãoPodeLockar");
            playerTransform.parent = null;
            boxMovement.enabled = false;            
            islocked = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        isInside = true;
    //        Debug.Log("O player entrou");
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        isInside = false;
    //        Debug.Log("O player saiu");
    //    }
    //}
}
