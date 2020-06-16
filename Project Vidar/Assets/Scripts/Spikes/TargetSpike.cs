using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpike : MonoBehaviour
{
    #region Field Declarations
    enum MovePattern
    {
        noTarget,
        singleTarget,
        randomTarget
    }

    [Header("Movement Configs")]
    [SerializeField]
    private MovePattern movePattern;
    [SerializeField]
    private bool movementEnabled = true;
    [SerializeField]
    public float speed = 4f;
    [SerializeField]
    private GameObject playerTarget;
    [SerializeField] public bool deadly = false;

    [Space]
    [Header("Script Variables")]
    [SerializeField]
    private GameObject target;
    private GameObject[] killers;

    #endregion

    private void Start()
    {
        switch (movePattern)
        {
            case MovePattern.noTarget:
                target = null;
                break;
            case MovePattern.singleTarget:
                target = playerTarget;
                break;
            case MovePattern.randomTarget:
                killers = GameObject.FindGameObjectsWithTag("killer");
                int index = Random.Range(0, 5);
                target = killers[index];
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (movePattern)
        {
            case MovePattern.noTarget:
                transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
                break;
            case MovePattern.singleTarget:
            case MovePattern.randomTarget:
                transform.LookAt(target.transform);
                transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("killer"))
            Destroy(gameObject);
    }
}