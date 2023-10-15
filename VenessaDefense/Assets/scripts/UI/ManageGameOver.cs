using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGameOver : MonoBehaviour
{
    private int enemiesKilled = 0;

    public void StartGameOverScreen(int enemiesKilled_)
    {
        enemiesKilled = enemiesKilled_;
        changeScene();
    }

    void changeScene()
    {
        SceneManager.LoadSceneAsync("GameOver").completed += (AsyncOperation asyncOp) =>
        {
            UpdateEnemiesKilledOnGameOverScreen();
        };
    }

    private void UpdateEnemiesKilledOnGameOverScreen()
    {
        GameObject uiCanvas = GameObject.Find("UI");

        if (uiCanvas == null )
            throw new ArgumentNullException("No UI canvas on Game Over Screen");

        Transform enemiesKilledUIElement = uiCanvas.transform.Find("Enemies Killed");

        if (enemiesKilledUIElement == null )
            throw new ArgumentNullException("Couldn't find enemies killed on game over screen");

        TextMeshProUGUI numberEnemiesKilledText = enemiesKilledUIElement.Find("NumberEnemiesKilled").GetComponent<TextMeshProUGUI>();

        numberEnemiesKilledText.text = $"{enemiesKilled}";
    }
}
