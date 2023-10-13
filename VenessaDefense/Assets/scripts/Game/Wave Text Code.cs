using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTextCode : MonoBehaviour
{
    public Text timeText;
    public float timeTicker = 0;
    public float time = 15;
    public double value = 1;
    private int waveNumber = 1;

    EnemyIntroManager enemyIntroManager = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyIntroManager = GetComponent<EnemyIntroManager>();

        if (enemyIntroManager == null)
            throw new ArgumentNullException("Enemy Intro Manager not found");

        StartCoroutine(enemyIntroManager.DisplayNewIntros(waveNumber));

        timeTicker = 0;
        CountWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            Destroy(GameObject.FindWithTag("Wave Text"));
            time = 15;
            CountWave();
        }

        if (GameObject.FindWithTag("Wave Text") == false)
            return;

        if (enemyIntroManager != null)
             StartCoroutine(enemyIntroManager.DisplayNewIntros(waveNumber));


        displayWaveText();

    }

    private void displayWaveText()
    {
        timeText = GameObject.FindWithTag("Wave Text")?.GetComponent<Text>();

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
