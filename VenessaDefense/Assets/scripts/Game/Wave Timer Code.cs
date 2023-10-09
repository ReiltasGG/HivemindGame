using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isCounting = false;

    private void Start()
    {
    //    timerText.enabled = false; // Initially hide the timer text
    }

    private void Update()
    {
        if (isCounting)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isCounting = true;
        timerText.enabled = true; // Show the timer text
    }

    public void StopTimer()
    {
        isCounting = false;
     //   timerText.enabled = false;
    }

    private void UpdateTimerText(float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = "Time: " + timerString;
    }

}
