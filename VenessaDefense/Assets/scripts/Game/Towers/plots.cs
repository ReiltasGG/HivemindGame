using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plots : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    private GameObject tower;
    private Color startColor;

    private Color hoverColor = Color.red;

    private void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = Builder.main.GetSelectedTower();

        if(towerToBuild.cost > Currency.main.currency ) 
        {
            return; 
        }

        Currency.main.subtractCurrency(towerToBuild.cost);

        Vector3 positionZOnTopPlot = transform.position + new Vector3(0, 0, -5);
        tower = Instantiate(towerToBuild.prefab, positionZOnTopPlot, Quaternion.identity);
    }
}
