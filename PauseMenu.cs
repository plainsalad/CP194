using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public AudioListener mainAudioListener;

    // Add a public reference to the FirstPersonLook script
    public FirstPersonLook firstPersonLookScript;

    public Zoom zoomScript;
    
    public GameObject lighter;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.Instance.IsGameOver)
            {
                return;
            }
            if (UIManager.Instance.IsUIActive())
            {
                UIManager.Instance.RequestCloseActiveUI();
            }
            else if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        mainAudioListener.enabled = true;
        UIManager.SetCursorState(false, CursorLockMode.Locked);

        // Enable the FirstPersonLook script
        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = true;
            zoomScript.enabled = true;
            lighter.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        mainAudioListener.enabled = false;

        // Disable the FirstPersonLook script
        if (firstPersonLookScript != null)
        {
            firstPersonLookScript.enabled = false;
            zoomScript.enabled = false;
            lighter.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
