using UnityEngine;
using System.Collections;

public class FreezableBox : MonoBehaviour
{
	public bool isFrozen = false;
	public bool freeze;
	public Rigidbody rb;
	//Renderer rnd;
	//public Material[] material;
	//Animator anim;
	public GameObject boxWithShader;

	Mesh initialMesh;
	Mesh swapMesh;

	public void Start()
	{		
		//rnd = gameObject.GetComponent<Renderer>();
		//rnd.sharedMaterial = material[0];	
	}
	public void Update()
	{
		changeAnimState();
	}
	public void changeAnimState()
	{
		if (!isFrozen)
		{
			//anim.SetBool("freeze", true);			
			//rnd.sharedMaterial = material[0];
			boxWithShader.SetActive(false);
		}
		else
		{
			//anim.SetBool("freeze", false);
			//rnd.sharedMaterial = material[1];
			boxWithShader.SetActive(true);
		}
	}
}
