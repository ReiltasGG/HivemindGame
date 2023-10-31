using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerCurrentStats : MonoBehaviour
{
    public void ShowCurrentStats(string type)
    {
        ShowTowerStats towerManager = GameObject.FindWithTag("GamesManager").GetComponent<ShowTowerStats>();
        string towerType = string.Empty;
        AttributesManager towerStats = GetComponent<AttributesManager>();
        int towerDamage = 0;
        int towerRange = 0;
        int towerCoolDown = 0;
        int towerHP = towerStats.health;

        switch (type)
        {
            case "Honey Bee":
            towerType = $"Honey Bee\n\n\nDuration: 3 seconds\n\nRange: 3 blocks\n\nCooldown: 3 seconds\n\nTower HP: {towerHP}\n\nTier: 1";
            break;

            case "Battle Bee":
            towerType = $"Battle Bee\n\n\nDamage: 10\n\nRange: 3 Blocks\n\nTower HP: {towerHP}\n\nTier: 1";
            break;

            case "Shaman Bee":
            towerType = $"Shaman Bee\n\n\nDuration: 3 seconds\n\nRange: 3 Blocks\n\nCooldown: 3 seconds\n\nTower HP: {towerHP}\n\nTier 1";
            break;

            case "Healer Bee":
            towerType = $"Healer Bee\n\n\nRange: 4 Blocks\n\nCooldown: 10 seconds\n\nTower HP: {towerHP}\n\nTier 1";
            break;

            case "Hive":
            towerType = $"Hive\n\nHive HP: {towerHP}\n\nDefend with your life!";
            break;


        }

        towerManager.SetTowerText(towerType);
        towerManager.ToggleTowerStats();
    }
    
}
