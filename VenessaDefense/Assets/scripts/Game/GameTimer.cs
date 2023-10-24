using System;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timeTicker = 0;

    private List<Action> functionsToRunEveryFrame = null;

    private void Start()
    {
        functionsToRunEveryFrame = new List<Action>();
    }

    void Update()
    {
        timeTicker += Time.deltaTime;
        RunFunctionsEveryFrame();
    }

    public float GetTimePassed()
    {
        return timeTicker;
    }

    public void RunFunctionsEveryFrame()
    {
        foreach (var func in functionsToRunEveryFrame)
        {
            func();
        }
    }

    public void AddFunctionToRunEveryFrame(Action func)
    {
        functionsToRunEveryFrame.Add(func);
    }
}
