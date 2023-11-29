using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public static Builder main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int SelectedTower = 0;

    private void Update()
    {
        float scrollWheel = Input.mouseScrollDelta.y;

        if (scrollWheel > 0f)
        {
            Builder.main.SetSelectedTower(Builder.main.SelectedTower + 1);
        }
        else if (scrollWheel < 0f)
        {
            Builder.main.SetSelectedTower(Builder.main.SelectedTower - 1);
        }
    }

    private void Awake()
    {
        main = this;
    }
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower % towers.Length];
    }
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }

}
