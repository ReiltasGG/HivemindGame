using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public void ShowInfo(string type)
    {
        ShowTowerStats towerManager = GameObject.FindWithTag("GamesManager").GetComponent<ShowTowerStats>();
        string towerType = string.Empty;
        switch (type)
        {
            case "Honey Bee":
            towerType = "Honey Bee\n\n'Slowing Tower'\n\nShoots honey to slow nearby enemies\n\nDuration: 3 seconds\n\nRange: 3 blocks\n\nCooldown: 3 seconds\n\nTower HP: 400\n\nCost: $15";
            break;

            case "Battle Bee":
            towerType = "Battle Bee\n\n'Gunning Tower'\n\nShoots stingers at nearby enemies\n\nDamage: 10\n\nRange: 5 blocks\n\nTower HP: 300\n\nCost: $20";
            break;

            case "Shaman Bee":
            towerType = "Shaman Bee\n\n'Cursing Tower'\n\nCurses nearby enemies to take extra damage\n\nDuration: 3 seconds\n\nRange: 3 blocks\n\nCooldown: 3 seconds\n\nTower HP: 400\n\nCost: $25";
            break;

            case "Healer Bee":
            towerType = "Healer Bee\n\n'Medic Tower'\n\nHeals the player in range by 20 HP\n\nRange: 4 blocks\n\nCooldown: 10 seconds\n\nTower HP: 500\n\nCost: $30";
            break;
        }

        towerManager.SetTowerText(towerType);
        towerManager.ToggleTowerStats();
    }
    
}
