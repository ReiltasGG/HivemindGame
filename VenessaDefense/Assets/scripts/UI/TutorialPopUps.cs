using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUps : MonoBehaviour
{
    public static bool paused = false;
    public GameObject welcomeUI;

    void Start()
    {
        pause();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    void pause()
    {
        welcomeUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
    public void close()
    {
        welcomeUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

}
