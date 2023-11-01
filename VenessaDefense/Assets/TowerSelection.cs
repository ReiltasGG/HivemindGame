using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour
{
    private Button button;
    private TowerCurrentStats towerCurrentStats; 
    private ShowTowerStats showTowerStats;
    private int clickCount = 0;

    [SerializeField]
    private string towerName;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        if (button == null){Debug.Log("error with get component button");}
        button.onClick.AddListener(OnClick);

        
        towerCurrentStats = GetComponentInParent<TowerCurrentStats>();

        GameObject gameManagerObject = GameObject.FindWithTag("GamesManager");

        if (gameManagerObject != null)
        {
            showTowerStats = gameManagerObject.GetComponent<ShowTowerStats>();
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    public void OnClick()
    {
        if (clickCount % 2 == 0)
        {
            if (towerCurrentStats != null)
            {
                towerCurrentStats.ShowCurrentStats(towerName);
            }

            else
            {
                Debug.Log("towercurrent stats is null.");
            }
        }
       
        clickCount++;
    }
}
