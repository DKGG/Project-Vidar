using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject graphicsMenuUI;

    private float speedSliderValue = 5f;
    private float volumeSliderValue = 0.5f;
    private float cameraRotationDefault;
    private OverTheShoulderCamera cameraScript;
    private PostProcessVolume postProcessing;
    private Vignette vignette;
    private MotionBlur motionBlur;
    private DepthOfField depthOfField;
    private AudioManager audioManager;
    private PauseController pauseController;

    public void Awake()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OverTheShoulderCamera>();
        cameraRotationDefault = cameraScript.rotationSpeed;

        postProcessing = GameObject.Find("Main PostProcessingVolume").GetComponent<PostProcessVolume>();
        postProcessing.profile.TryGetSettings(out vignette);
        postProcessing.profile.TryGetSettings(out motionBlur);
        postProcessing.profile.TryGetSettings(out depthOfField);

        audioManager = FindObjectOfType<AudioManager>();
        pauseController = pauseMenuUI.transform.parent.GetComponent<PauseController>();
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
        if (Input.GetKeyDown(KeyCode.Escape) && optionsMenuUI.activeSelf)
        {
            pauseController.HandleInput();
        }
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

    public void LoadPause()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void ShowGraphics()
    {
        graphicsMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void HideGraphics()
    {
        graphicsMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
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
