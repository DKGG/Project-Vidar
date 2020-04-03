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

    //public string side = "";

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
                PlayerEntity.setIslockedInNorth(true);
                PlayerEntity.setIslockedInSouth(false);
                PlayerEntity.setIslockedInWest(false);
                PlayerEntity.setIslockedInEast(false);
                //side = "norte";                
            }

            if (noSul)
            {
                posicao = FaceSul;
                PlayerEntity.setIslockedInNorth(false);
                PlayerEntity.setIslockedInSouth(true);
                PlayerEntity.setIslockedInWest(false);
                PlayerEntity.setIslockedInEast(false);
                //side = "sul";                
            }
            if (noOeste)
            {
                posicao = FaceOeste;
                PlayerEntity.setIslockedInNorth(false);
                PlayerEntity.setIslockedInSouth(false);
                PlayerEntity.setIslockedInWest(true);
                PlayerEntity.setIslockedInEast(false);
                //side = "oeste";                
            }
            if (noLeste)
            {
                posicao = FaceLeste;
                PlayerEntity.setIslockedInNorth(false);
                PlayerEntity.setIslockedInSouth(false);
                PlayerEntity.setIslockedInWest(false);
                PlayerEntity.setIslockedInEast(true);
                //side = "leste";                
            }

            //playerTransform.transform.parent = transform;
            playerTransform.transform.parent = PlayerEntity.getBoxLocked().transform;
            boxMovement.enabled = true;
            islocked = true;
            //PlayerEntity.setLocked(true);
        }

        if (!locka && PlayerEntity.getLocked())
        {
            //Debug.Log("NãoPodeLockar");
            playerTransform.parent = null;
            boxMovement.enabled = false;
            islocked = false;
            //PlayerEntity.setLocked(false);
        }
    }

}
