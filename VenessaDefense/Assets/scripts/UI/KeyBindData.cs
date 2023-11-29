using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyBindData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
        void Awake()
    {
       
        
            DontDestroyOnLoad(gameObject);
        
    }
    // Update is called once per frame
    void Update()
    {
          Debug.Log(PlayerPrefs.GetString("SkillTreeOpen", "T"));
    }
     
     public void SaveNewKey(KeyCode newKey, int which)
    {
    if(which == 0)
    {
    PlayerPrefs.SetString("SkillTreeOpen", newKey.ToString());
    }
  
    else if(which == 1)
    {
    PlayerPrefs.SetString("DashKey",  newKey.ToString());
    }
    else if(which ==2 )
    {
    PlayerPrefs.SetString("DecoyKey",  newKey.ToString());
    }

    PlayerPrefs.Save(); // Ensure to save changes immediately
    }
    public void LevelSelect()
    {
           if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Unlocked Level", PlayerPrefs.GetInt("Unlocked Level", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
