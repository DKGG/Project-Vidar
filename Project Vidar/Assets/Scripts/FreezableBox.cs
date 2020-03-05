using UnityEngine;
using System.Collections;

public class FreezableBox : MonoBehaviour
{
	public bool isFrozen = false;
	public bool freeze;
	public Rigidbody rb;

	public void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
}
