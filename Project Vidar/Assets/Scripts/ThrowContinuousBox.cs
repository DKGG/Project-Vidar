using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowContinuousBox : MonoBehaviour
{    

    public enum DirectionForce
    {
        normal,
        up
    };


    bool colidiu;

    [SerializeField] DirectionForce dirf;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerEntity.getWantToThrow());
        //if (PlayerEntity.getWantToThrow() == true && colidiu == false)
        //{
        //    //switch (dirf)
        //    //{
        //    //    case DirectionForce.normal:
        //    //        Debug.Log("entrei no normal", transform);
        //    //        break;
        //    //    case DirectionForce.up:
        //    //        Debug.Log("entrei no up", transform);
        //    //        break;
        //    //    default:
        //    //        break;
        //    //}

        //    if (dirf.Equals(DirectionForce.normal))
        //    {
        //        Debug.Log("normal");
        //    }
        //    else
        //    {
        //        Debug.Log("up");
        //    }

        //    PlayerEntity.setWantToThrow(false);
        //    PlayerEntity.setThrewTheBox(true);
        //}

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (/*collision.gameObject.CompareTag("Player") ||*/ collision.gameObject.CompareTag("paraBloco"))
        {
            colidiu = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    colidiu = false;
        //}
    }
}

    
