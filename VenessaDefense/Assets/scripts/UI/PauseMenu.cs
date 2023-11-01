using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    void Update()
    {
        //Debug.Log(Time.timeScale);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused == false) 
            {
                Pause(); 
            }

            else
            { 
                Resume();
            }

            Debug.Log(Time.timeScale);
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log($"Time scale: {Time.timeScale}");
    }


    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
