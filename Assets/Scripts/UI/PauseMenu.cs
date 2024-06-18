using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public static bool isGamePaused = false;

    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
