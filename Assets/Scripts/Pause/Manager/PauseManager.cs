using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public KeyCode pauseKey;
    public GameObject pausePanelGO;
    public Slider sensitivitySlider;

    private bool isPaused = false;

    public event Action sensitivityChanged;

    private void Start()
    {
        isPaused = false;
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 100);
    }

    private void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            SetPause();
        }
    }

    public void SetPause()
    {
        if(!isPaused && !PlayerManager.instance.isPlayerAllowedToMove)
            return;

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pausePanelGO.SetActive(isPaused);
        PlayerManager.instance.SetPlayerState(!isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
        sensitivityChanged?.Invoke();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
