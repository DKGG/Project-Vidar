using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    [SerializeField]public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FadeObj();
        }
    }

    public void FadeObj()
    {
        animator.SetTrigger("getSpell");
    }
}
