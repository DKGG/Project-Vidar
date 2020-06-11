using UnityEngine;
using System.Collections;

public class FreezableBox : MonoBehaviour
{
	public bool isFrozen = false;
	public Rigidbody rb;

	AlphaShaderAnimation shader;

	/* Deprecated */
	GameObject boxWithShader;
	Mesh initialMesh;
	Mesh swapMesh;

	public void Start()
	{
		if(gameObject.GetComponentInParent<Rigidbody>())
        {
			rb = gameObject.GetComponentInParent<Rigidbody>();
		} else
        {
			rb = gameObject.GetComponent<Rigidbody>();
		}

		shader = gameObject.GetComponent<AlphaShaderAnimation>();
	}

	public void Update()
	{
		if (!isFrozen)
        {
			shader.spellDown = true;
			shader.spellUp = false;
		}
        else
        {
			shader.spellDown = false;
			shader.spellUp = true;
		}

		// changeAnimState();
	}

	/* Deprecated */
	#region Animation Freeze

	public void changeAnimState()
	{
		if (!isFrozen)
		{
			freezeEffect();
			boxWithShader.GetComponent<Animator>().SetBool("freeze", true);
		}
		else
		{
			//anim.SetBool("freeze", false);
			//rnd.sharedMaterial = material[1];
			freezeEffect();
			//boxWithShader.SetActive(true);
			boxWithShader.GetComponent<Animator>().SetBool("freeze", false);
		}
	}

	private IEnumerator freezeEffect()
	{
		//gunAudio.Play();

		yield return 1.0;
	}

	#endregion
}
