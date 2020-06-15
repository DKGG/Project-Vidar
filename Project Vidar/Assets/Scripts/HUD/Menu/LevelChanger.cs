using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    //public Animator animator;
    [SerializeField]
    private int levelIndex;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEntity.setDashing(false);
            PlayerEntity.setJumping(false);
            PlayerEntity.setWalking(false);
            AnimatorManager.setStateIdle();
            FadeToLevel();
            OnFadeComplete();
        }
    }

    public void FadeToLevel ()
    {        
        //animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete(){
        
        SceneManager.LoadScene(levelIndex);        
    }
}
