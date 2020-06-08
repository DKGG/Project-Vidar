using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject pauseMenuUI;

    public RectTransform ResumeButton;
    public Texture2D cursorSprite;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenuUI.activeSelf)
        {
            HandleInput();
        }
    }

    public void HandleInput()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1.3f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.ForceSoftware);

        // Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void QuitMenu()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
