using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plots : MonoBehaviour
{

    [Header("References")]
        [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    // Start is called before the first frame update
    private GameObject tower;
    private Color startColor;

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

        GameObject towerToBuild = Builder.main.GetSelectedTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
    }
}
