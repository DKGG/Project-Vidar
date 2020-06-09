using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenuUI;
    [SerializeField]
    private GameObject pauseMenuUI;

    private float speedSliderValue = 5f;
    private float volumeSliderValue = 0.5f;
    private float cameraRotationDefault;
    private OverTheShoulderCamera cameraScript;
    private GameObject postProcessing;
    private AudioManager audioManager;
    private PauseController pauseController;

    public void Awake()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OverTheShoulderCamera>();
        cameraRotationDefault = cameraScript.rotationSpeed;

        postProcessing = GameObject.Find("Main PostProcessingVolume");
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

    public void TogglePostProcessing(bool value)
    {
        postProcessing.SetActive(value);
    }
}
