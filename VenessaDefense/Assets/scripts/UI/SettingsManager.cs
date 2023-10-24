using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openSettings()
    {
        settingsUI.SetActive(true);
    }
    public void closeSettings()
    {
        settingsUI.SetActive(false);
    }
}
