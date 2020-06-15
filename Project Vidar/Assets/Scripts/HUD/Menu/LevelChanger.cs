using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    //public Animator animator;
    [SerializeField]
    private int sceneIndex;

    [SerializeField]
    GameObject loadingScreen;
    [SerializeField]
    Slider slider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEntity.setDashing(false);
            PlayerEntity.setJumping(false);
            PlayerEntity.setWalking(false);
            AnimatorManager.setStateIdle();
            // FadeToLevel();
            StartCoroutine(LoadAsynchronously(sceneIndex));
        }
    }

    public void FadeToLevel()
    {
        //animator.SetTrigger("FadeOut");
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        // SceneManager.LoadScene(levelIndex);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}
