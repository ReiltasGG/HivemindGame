using System;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timeTicker = 0;

    void Update() 
    { 
        timeTicker += Time.deltaTime; 
    }
    public float GetTimePassed() { return timeTicker; }
    public void SetTimeToZero() {  timeTicker = 0; }
}
