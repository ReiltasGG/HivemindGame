using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{

    public Button[] buttons;


    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("Unlocked Level", 1));
    }


    private void Awake()
   {
      int unlocklevel = PlayerPrefs.GetInt("Unlocked Level", 1);
      for (int i = 0; i < buttons.Length; i++)
      {
          buttons[i].interactable = false;
      }
      for (int i = 0; i < unlocklevel; i++) 
      {
         buttons[i].interactable = true;
      }
        DontDestroyOnLoad(gameObject);
    }
    public void OpenLevel(int levelID)
    {
        string levelName = "Day " + levelID;
        SceneManager.LoadScene(levelName);
    }
}
