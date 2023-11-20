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
    private string NextDaySceneName;
    private bool isSceneLoaded = false;

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
            UpdateEnemiesKilledOnGameOverScreen();
        };
    }

    private void UpdateEnemiesKilledOnGameOverScreen()
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

    public void StartDayClearedScene(int dayCleared_)
    {
        dayCleared = dayCleared_;
        NextDaySceneName = "Day " + (dayCleared + 1).ToString();
        Debug.Log(NextDaySceneName);
        changeSceneToDayCleared();
    }

    void changeSceneToDayCleared()
    {
        SceneManager.LoadSceneAsync(DayClearedSceneName).completed += (AsyncOperation asyncOp) =>
        {
            UpdateDayClearedOnDayClearedScene();
            isSceneLoaded = true;
        };
    }

    private void UpdateDayClearedOnDayClearedScene()
    {
        GameObject uiCanvas = GameObject.Find("UI");

        if (uiCanvas == null)
            throw new ArgumentNullException("No UI canvas on Game Cleared Scene");

        Transform dayClearedUIElement = uiCanvas.transform.Find("Day Cleared");

        if (dayClearedUIElement == null)
            throw new ArgumentNullException("Couldn't find day cleared UI Element.");

        TextMeshProUGUI dayClearedText = dayClearedUIElement.GetComponent<TextMeshProUGUI>();

        dayClearedText.text = $"Day {dayCleared} Cleared";

    }
    
    /*
    private void UpdateNextDayScene()
    {
        NextDaySceneName = "Day " + (dayCleared + 1).ToString();
    }
    

    public string GetNextDaySceneName()
    {
        return NextDaySceneName;
    }

    */
    
    public void SceneToNextDay()
    {
        SceneManager.LoadScene("Day 2");
        /*
        string temp = GetNextDaySceneName();
        Debug.Log(temp);
        */
    }

}
