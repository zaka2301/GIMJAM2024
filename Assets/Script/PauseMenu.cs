using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public static bool IsSettingsOpened = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        if (IsSettingsOpened)
        {
            CloseSettings();
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void OpenSettings()
    {
        settingsMenuUI.SetActive(true);
        settingsMenuUI.GetComponent<Animator>().SetBool("IsOpened", true);
        IsSettingsOpened = true;
    }

    public void CloseSettings()
    {
        settingsMenuUI.GetComponent<Animator>().SetBool("IsOpened", false);
        StartCoroutine(InactivateSettings());
        IsSettingsOpened = false;
    }

    IEnumerator InactivateSettings()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("settings closed");
        settingsMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Bye bye");
        Application.Quit();
    }
}
