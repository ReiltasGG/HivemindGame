using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextCode : MonoBehaviour
{
    private const float TEXT_TIME = 15f;
    public Text timeText;
    public float timeTicker = 0;
    public float time = TEXT_TIME;
    public double value = 1;
    private int waveNumber = 1;

    void Update()
    {
        if (time <= 0)
        {
            Destroy(GameObject.FindWithTag("Wave Text"));
            time = TEXT_TIME;
            CountWave();
        }

        if (GameObject.FindWithTag("Wave Text") == false)
            return;

        displayWaveText();

    }

    private void displayWaveText()
    {
        timeText = GameObject.FindWithTag("Wave Text")?.GetComponent<Text>();

        if (timeText == null) return ;

        // Subtracts time for the timer
        timeTicker = timeTicker + Time.deltaTime;
        if (value < timeTicker && time > 0)
        {
            time = time - 1;
            timeText.text = "Wave " + waveNumber + " incoming in " + time + " seconds";
            timeTicker = 0;
        }
    }
    void CountWave()
    {
        waveNumber++;
    }
}
