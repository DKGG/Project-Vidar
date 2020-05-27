using UnityEngine;
using System.Collections;

public class FreezableBox : MonoBehaviour
{
	public bool isFrozen = false;
	public bool freeze;
	public Rigidbody rb;
	Animator anim;

	public void Start()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		anim.SetBool("freeze", true);
	}
	public void Update()
	{
		changeAnimState();
	}
	public void changeAnimState()
	{
		if (!isFrozen)
		{
			anim.SetBool("freeze", true);
		} else
		{
			anim.SetBool("freeze", false);
		}
	}
}
