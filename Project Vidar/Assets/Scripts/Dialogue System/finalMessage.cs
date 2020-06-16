using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalMessage : MonoBehaviour
{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public GameObject textOne;
    [SerializeField]
    public GameObject textTwo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            textOne.SetActive(false);
            textTwo.SetActive(false);
            anim.SetBool("logoOn", true);

            StartCoroutine(WaitForSecondsCall());
        }
    }

    IEnumerator WaitForSecondsCall ()
    {
        yield return new WaitForSeconds(2f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
