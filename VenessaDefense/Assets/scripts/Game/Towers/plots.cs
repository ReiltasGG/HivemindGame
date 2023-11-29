using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class plots : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    private GameObject tower;
    private GameObject playerHUD;
    private Transform notEnoughCoins;

    private Color startColor;

    private Color hoverColor = Color.red;

    private GameObject hoveredTower = null;


    private void Start()
    {
        startColor = sr.color;
        playerHUD = GameObject.Find("PlayerHUD with shop");
        notEnoughCoins = playerHUD.transform.Find("Shop").Find("NotEnoughCoinsText");
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;

        CreateHoveredTower();

    }

    private void OnMouseExit()
    {
        sr.color = startColor;

        DeleteHoveredTower();
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        
        if (tower != null) return;
        

        Tower towerToBuild = GetTowerToBuild();
        if(towerToBuild == null) return;

        if(towerToBuild.cost > Currency.main.currency ) {
            DisplayNotEnoughCoins();
            return; 
        }

        Currency.main.subtractCurrency(towerToBuild.cost);

        Vector3 positionZOnTopPlot = transform.position + new Vector3(0, 0, -5);
        tower = Instantiate(towerToBuild.prefab, positionZOnTopPlot, Quaternion.identity);
        GameObject gameManager = GameObject.FindWithTag("GamesManager");
        TowerManager towerManager = gameManager.GetComponent<TowerManager>();
        towerManager.AddTower();
    }

    private Tower GetTowerToBuild()
    {
        return Builder.main.GetSelectedTower();
    }

    private void DisplayNotEnoughCoins()
    {
        TextMeshProUGUI insufficientCoins = notEnoughCoins.GetComponent<TextMeshProUGUI>();
        insufficientCoins.text = "Not Enough Coins!";
        StartCoroutine(FadeText(insufficientCoins, 1.0f));
    }

    private IEnumerator FadeText(TextMeshProUGUI text, float duration){
        StopCoroutine(FadeText(text, duration));

        float startTime = Time.time;
        float startAlpha = text.color.a;
        float endAlpha = 0; // Fade out to fully transparent

        while (Time.time < startTime + duration){
            float t = (Time.time - startTime) / duration;
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(startAlpha, endAlpha, t));
            yield return null;
        }
   // Ensure the text is fully transparent
        text.color = new Color(text.color.r, text.color.g, text.color.b, endAlpha);
        text.text = "";
        text.color = new Color(text.color.r, text.color.g, text.color.b, startAlpha);
    }

    private void CreateHoveredTower()
    {
        Tower highlighedTower = GetTowerToBuild();
        hoveredTower = Instantiate(highlighedTower.prefab, transform.position + new Vector3(0, 0, -5), Quaternion.identity);
        RemoveAllComponentsExceptSpriteAndTransform(hoveredTower);

        SpriteRenderer sprite = hoveredTower.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            Color originalColor = sprite.color;
            Color highlightColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.75f);

            sprite.color = highlightColor;
        }
    }

    private void DeleteHoveredTower()
    {
        if (hoveredTower != null)
        {
            Destroy(hoveredTower);
        }
    }
    private void RemoveAllComponentsExceptSpriteAndTransform(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();

        foreach (Component component in components)
        {
            if (!(component is SpriteRenderer) && !(component is Transform))
            {
                Destroy(component);
            }
        }
    }
}
