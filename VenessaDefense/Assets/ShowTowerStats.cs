using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTowerStats : MonoBehaviour
{
    [SerializeField]
    private GameObject towerInfo;

    [SerializeField]
    private Text towerStatsText;

    public void ToggleTowerStats()
    {
        towerInfo.SetActive(!towerInfo.activeSelf);
    }

    public void SetTowerText(string text)
    {
        towerStatsText.text = text;
    }
}
