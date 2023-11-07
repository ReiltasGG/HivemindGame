using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public int totalTowersPlaced = 0;
    public event Action<int> towersPlacedUpdated;

    public void AddTower()
    {
        totalTowersPlaced++;
        towersPlacedUpdated?.Invoke(totalTowersPlaced);
    }

    public int GetTotalTowersPlaced()
    {
        return totalTowersPlaced;
    }

}
