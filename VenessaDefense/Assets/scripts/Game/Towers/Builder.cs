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

    private int SelectedTower = -1;

    private void Awake()
    {
        main = this;
    }
    public Tower GetSelectedTower()
    {
        if (SelectedTower != -1 && SelectedTower < towers.Length){
            return towers[SelectedTower];
        }
        //return towers[SelectedTower];
        return null;
    }
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }

}
