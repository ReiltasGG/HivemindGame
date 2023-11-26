using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
