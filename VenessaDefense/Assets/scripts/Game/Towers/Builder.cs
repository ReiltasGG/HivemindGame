using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    // Start is called before the first frame update
    public static Builder main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;

    private int SelectedTower;

    private void Awake()
    {
        main = this;
    }
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }
    public void SetSlectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }
}
