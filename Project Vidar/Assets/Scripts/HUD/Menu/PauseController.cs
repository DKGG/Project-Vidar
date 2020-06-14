using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PauseController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject confirmExitUI;
    [SerializeField]
    private GameObject graphicsMenuUI;

    public RectTransform ResumeButton;
    public Texture2D cursorSprite;
    private OverTheShoulderCamera cameraScript;
    private float speedSliderValue = 5f;
    private float volumeSliderValue = 0.5f;
    private float cameraRotationDefault;
    private PostProcessVolume postProcessing;
    private Vignette vignette;
    private MotionBlur motionBlur;
    private DepthOfField depthOfField;
    private AudioManager audioManager;
    private PauseController pauseController;


    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    private void Awake()
    {
        Cursor.visible = false;
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OverTheShoulderCamera>();
        cameraRotationDefault = cameraScript.rotationSpeed;

        postProcessing = GameObject.Find("Main PostProcessingVolume").GetComponent<PostProcessVolume>();
        postProcessing.profile.TryGetSettings(out vignette);
        postProcessing.profile.TryGetSettings(out motionBlur);
        postProcessing.profile.TryGetSettings(out depthOfField);

        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Start()
    {
        // TODO
        // Load values from player's computer
        setVolume(volumeSliderValue);
        setCameraSpeed(speedSliderValue);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        confirmExitUI.SetActive(false);
        graphicsMenuUI.SetActive(false);

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

    public void CloseOptions()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void LoadExitConfirmation()
    {
        pauseMenuUI.SetActive(false);
        confirmExitUI.SetActive(true);
    }

    public void CloseExitConfirmation()
    {
        pauseMenuUI.SetActive(true);
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

    public void setCameraSpeed(float value)
    {
        // TODO
        // Save values on player's computer
        speedSliderValue = value;
        cameraScript.rotationSpeed = cameraRotationDefault * (speedSliderValue / 10);
    }

    public void setVolume(float value)
    {
        // TODO
        // Save values on player's computer
        volumeSliderValue = value;
        audioManager.setGeneralVolume(volumeSliderValue);
    }

    public void LoadGraphics()
    {
        optionsMenuUI.SetActive(false);
        graphicsMenuUI.SetActive(true);
    }

    public void CloseGraphics()
    {
        optionsMenuUI.SetActive(true);
        graphicsMenuUI.SetActive(false);
    }
    public void ToggleVignette(bool value)
    {
        vignette.enabled.value = value;
    }

    public void ToggleMotionBlur(bool value)
    {
        motionBlur.enabled.value = value;
    }

    public void ToggleDepthField(bool value)
    {
        depthOfField.enabled.value = value;
    }
}
