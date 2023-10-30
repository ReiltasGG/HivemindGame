using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateTowerStat : MonoBehaviour
{
    [SerializeField]
    private Text towerStatsText;

    public void SetTowerText(string text)
    {
        towerStatsText.text = text;
    }
}
