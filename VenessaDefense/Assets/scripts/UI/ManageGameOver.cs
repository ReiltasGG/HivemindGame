using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    private int enemiesKilled = 0;
    private int dayCleared = 0;

    private const string GameOverSceneName = "GameOver";
    private const string DayClearedSceneName = "DayCleared";
    private const string MainMenuSceneName = "MainMenu";

    public void ChangeSceneToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void StartGameOverScene(int enemiesKilled_)
    {
        enemiesKilled = enemiesKilled_;
        changeSceneToGameOver();
    }

    void changeSceneToGameOver()
    {
        SceneManager.LoadSceneAsync(GameOverSceneName).completed += (AsyncOperation asyncOp) =>
        {
            UpdateEnemiesKilledOnGameOverScrene();
        };
    }

    private void UpdateEnemiesKilledOnGameOverScrene()
    {
        GameObject uiCanvas = GameObject.Find("UI");

        if (uiCanvas == null)
            throw new ArgumentNullException("No UI canvas on Game Over Screen");

        Transform enemiesKilledUIElement = uiCanvas.transform.Find("Enemies Killed");

        if (enemiesKilledUIElement == null)
            throw new ArgumentNullException("Couldn't find enemies killed on game over scene");

        TextMeshProUGUI numberEnemiesKilledText = enemiesKilledUIElement.Find("NumberEnemiesKilled").GetComponent<TextMeshProUGUI>();

        numberEnemiesKilledText.text = $"{enemiesKilled}";
    }

    public void StartDayClearedScene(int enemiesKilled_)
    {
        enemiesKilled = enemiesKilled_;
        changeSceneToDayCleared();
    }

    void changeSceneToDayCleared()
    {
        SceneManager.LoadSceneAsync(DayClearedSceneName).completed += (AsyncOperation asyncOp) =>
        {
            UpdateDayClearedOnDayClearedScene();
        };
    }

    private void UpdateDayClearedOnDayClearedScene()
    {
        GameObject uiCanvas = GameObject.Find("UI");

        if (uiCanvas == null)
            throw new ArgumentNullException("No UI canvas on Game Cleared Scene");

        Transform dayClaredUIElement = uiCanvas.transform.Find("Day Cleared Text");

        if (dayClaredUIElement == null)
            throw new ArgumentNullException("Couldn't find day cleared UI Element.");

        TextMeshProUGUI dayClearedText = dayClaredUIElement.GetComponent<TextMeshProUGUI>();

        dayClearedText.text = $"Day {dayCleared} Cleared";
    }

}
