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
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        timeTicker = 0;
        CountWave();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Wave Text") == true)
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

        if (time <= 0)
        {
            Destroy(GameObject.FindWithTag("Wave Text"));
            time = 15;
            CountWave();
        }
    }

    void CountWave()
    {
        waveNumber++;
    }
}
