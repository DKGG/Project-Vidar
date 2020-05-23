using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockB1 : MonoBehaviour
{
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

    bool noNorte;
    bool noSul;
    bool noOeste;
    bool noLeste;

    public LayerMask Player;

    boxMovement boxMove;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        boxMove = GetComponent<boxMovement>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        noNorte = Physics.Linecast(ponto3.position, ponto4.position, Player);
        noSul = Physics.Linecast(ponto1.position, ponto2.position, Player);
        noOeste = Physics.Linecast(ponto2.position, ponto4.position, Player);
        noLeste = Physics.Linecast(ponto1.position, ponto3.position, Player);
        Debug.Log(posicao);

        if (noNorte)
        {
            PlayerEntity.setIslockedInNorth(true);
            PlayerEntity.setIslockedInSouth(false);
            PlayerEntity.setIslockedInWest(false);
            PlayerEntity.setIslockedInEast(false);
            PlayerEntity.setPositionToLock(FaceNorte);
            Debug.Log("Norte");
        }
        if (noSul)
        {
            PlayerEntity.setIslockedInNorth(false);
            PlayerEntity.setIslockedInSouth(true);
            PlayerEntity.setIslockedInWest(false);
            PlayerEntity.setIslockedInEast(false);
            PlayerEntity.setPositionToLock(FaceSul);
            Debug.Log("Sul");
        }

        if (noOeste)
        {
            PlayerEntity.setIslockedInNorth(false);
            PlayerEntity.setIslockedInSouth(false);
            PlayerEntity.setIslockedInWest(true);
            PlayerEntity.setIslockedInEast(false);
            PlayerEntity.setPositionToLock(FaceOeste);
            Debug.Log("Oeste");
        }

        if (noLeste)
        {
            PlayerEntity.setIslockedInNorth(false);
            PlayerEntity.setIslockedInSouth(false);
            PlayerEntity.setIslockedInWest(false);
            PlayerEntity.setIslockedInEast(true);
            PlayerEntity.setPositionToLock(FaceLeste);
            Debug.Log("Leste");
        }

        if (PlayerEntity.getWantToLock() == true && PlayerEntity.getLocked() == false) 
        {
            playerTransform.parent = PlayerEntity.getBoxLocked().transform;
            PlayerEntity.setLocked(true);
            boxMove.enabled = true;
            rb.isKinematic = false;  
                  
        }
        if(PlayerEntity.getWantToLock() == false && PlayerEntity.getLocked() == true)
        {
            playerTransform.parent = null;
            PlayerEntity.setLocked(false);
            boxMove.enabled = false;
            rb.isKinematic = true;
        }      
       
    }
}
