using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
      
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FadeToLevel();
        }
    }

    public void FadeToLevel ()
    {        
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);        
    }
}
