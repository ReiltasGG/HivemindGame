using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTowerStats : MonoBehaviour
{
    private GameObject towerInfo;

    private Text towerText;

    private void Start()
    {
        towerInfo = GameObject.Find("TowerInfo");
        if (towerInfo == null)
        {
            Debug.LogError("TowerInfo not found in the scene.");
        }
        else
        {
            towerInfo.SetActive(false);
        }
    }

    public void ToggleTowerStats()
    {
        if (towerInfo != null)
        {
            towerInfo.SetActive(!towerInfo.activeSelf);
        }

        else
        {
            Debug.LogError("TowerInfo is null. Make sure it's in the scene with the correct name.");
        }
    }

    public void SetTowerText(string textFill)
    {
        Transform textStatsTransform = towerInfo.transform.Find("TowerTextStats");
        if(textStatsTransform != null)
        {
            towerText = textStatsTransform.GetComponent<Text>();
            towerText.text = textFill;
        }
    }
}
