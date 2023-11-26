using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{

    public Button[] buttons;

    private void Awake()
    {
        int unlocklevel = PlayerPrefs.GetInt("Unlocked Level", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0;i < unlocklevel; i++) 
        {
            buttons[i].interactable = true;
        }
    }
    public void OpenLevel(int levelID)
    {

        string levelName = "Day " + levelID;
        SceneManager.LoadScene(levelName);
    }
}
