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

    // Start is called before the first frame update
    void Start()
    {
        timeTicker = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(time);
        if (GameObject.FindWithTag("Wave Text") == true)
        {
            timeText = GameObject.FindWithTag("Wave Text")?.GetComponent<Text>();

            Debug.Log(value);
            // Subtracts time for the timer
            timeTicker = timeTicker + Time.deltaTime;
            if (value < timeTicker && time > 0)
            {
                time = time - 1;
                timeText.text = "Wave incoming in " + time + " seconds";
                timeTicker = 0;
            }
        }

        if (time <= 0)
        {
            Destroy(GameObject.FindWithTag("Wave Text"));
            time = 15;
        }
    }
}