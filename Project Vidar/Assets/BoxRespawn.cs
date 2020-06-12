using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro.Examples;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    public float spawnTime = 0;
    [SerializeField] GameObject box;
    Vector3 initialPos;
    bool saiu;

    // Start is called before the first frame update
    void Start()
    {        
        initialPos = box.transform.position;
    }

    private void Update()
    {
        
        if(spawnTime!= 0)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                spawnTime = 0;
                box.transform.position = initialPos;
                box.GetComponent<Rigidbody>().velocity = Vector3.zero;
                box.GetComponent<Rigidbody>().isKinematic = true;
                box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ContinuosBox"))
        {
            spawnTime = 2f;
        }
    }
}
