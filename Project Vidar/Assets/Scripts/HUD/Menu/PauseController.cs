using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject confirmExitUI;

    public RectTransform ResumeButton;
    public Texture2D cursorSprite;
    private OverTheShoulderCamera cameraScript;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    private void Awake()
    {
        Cursor.visible = false;
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OverTheShoulderCamera>();


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
        cameraScript.enabled = true;

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1.3f;
        GameIsPaused = false;
    }

    void Pause()
    {
        cameraScript.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
        SetCursorPos(Screen.width / 2, Screen.height / 2);
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

    public void LoadExitConfirmation()
    {
        confirmExitUI.SetActive(true);
    }

    public void CloseExitConfirmation()
    {
        confirmExitUI.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
