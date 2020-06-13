using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private Vector3 spawnPoint;
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (PlayerEntity.getSpawnPoint() != spawnPoint)
            {
                animator.SetBool("IsActive", true);
                StartCoroutine(TurnOffAnimation());
            }
            PlayerEntity.setSpawnPoint(spawnPoint);

            // col.transform.position = spawnPoint;
        }
    }

    IEnumerator TurnOffAnimation()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("IsActive", false);
    }
}
