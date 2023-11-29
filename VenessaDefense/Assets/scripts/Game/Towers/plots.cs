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
    

    private void Start()
    {
        startColor = sr.color;
        playerHUD = GameObject.Find("PlayerHUD with shop");
        notEnoughCoins = playerHUD.transform.Find("Shop").Find("NotEnoughCoinsText");
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        
        if (tower != null) return;
        

        Tower towerToBuild = Builder.main.GetSelectedTower();
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

}
