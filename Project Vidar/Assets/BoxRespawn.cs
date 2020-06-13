using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro.Examples;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    public float spawnTime = 0;
    public static bool Respawning;
    [SerializeField] GameObject box;
    //public Transform playerGameObject;
    Vector3 initialPos;
    bool saiu;

    // Start is called before the first frame update
    void Start()
    {        
        initialPos = box.transform.position;
        //playerGameObject = GameObject.FindWithTag("Player").transform.parent;
    }

    private void Update()
    {
        
        if(spawnTime!= 0)
        {
            spawnTime -= Time.deltaTime;
            Respawning = true;
            if (spawnTime <= 0)
            {
                spawnTime = 0;
                box.transform.position = initialPos;                
                box.GetComponent<Rigidbody>().velocity = Vector3.zero;
                box.GetComponent<Rigidbody>().useGravity = true;
                LockB.noChao = true;                             
                box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;                
                StartCoroutine(ligaKinematic());
                box.GetComponent<Rigidbody>().useGravity = true;

            }
        }
        else
        {
            Respawning = false;
        }

    }

    IEnumerator ligaKinematic()
    {
        yield return new WaitForSeconds(0.1f);
        box.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContinuosBox"))
        {
            spawnTime = 2f;
           
        }
        if (other.gameObject.CompareTag("Player"))
        {
            //GameObject.FindWithTag("Player").transform.parent.SetParent(null);
            //PlayerEntity.setLocked(true);
            PlayerEntity.setWantToLock(false);

            //playerGameObject.SetParent(PlayerEntity.getBoxLocked().transform);
        }
    }
}
