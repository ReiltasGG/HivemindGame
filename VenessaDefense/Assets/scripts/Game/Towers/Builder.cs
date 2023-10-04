using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    // Start is called before the first frame update
    public static Builder main;

    [Header("References")]
    [SerializeField] private GameObject[] towerPrefabs;

    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public GameObject GetSelectedTower()
    {
        return towerPrefabs[SelectedTower];
    }
}