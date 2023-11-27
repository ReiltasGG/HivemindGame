using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class KeyBind : MonoBehaviour
{
    public Button rebindButton;
    public GameObject keyData;
    public TextMeshProUGUI keyDisplayText;
    //if 1 toggle tree, 2 dash, 3 decoy
    public int whichBind;
    private string abilityKeyString;
    

    private bool isRebinding = false;

   void Start()
{
    if(rebindButton!=null)
    rebindButton.onClick.AddListener(StartRebind);
    
   
   InitializeKey();
    UpdateKeyDisplay();
}


    // Ensure the KeyBind instance persists between scenes
    void Awake()
    {
       
    }

    void Update()
    {
         rebindButton.onClick.AddListener(StartRebind);
          UpdateKeyDisplay();
        //  Debug.Log("functions");
        Debug.Log(PlayerPrefs.GetString("SkillTreeOpen", "T"));
        if (isRebinding)
        {
            ListenForKeyPress();
        }
    }

    void InitializeKey()
    {
        if (!PlayerPrefs.HasKey("SkillTreeOpen"))
        {
            PlayerPrefs.SetString("SkillTreeOpen", "T");
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("DashKey"))
        {
            PlayerPrefs.SetString("DashKey", "Q");
            PlayerPrefs.Save();
        }
         if (!PlayerPrefs.HasKey("DecoyKey"))
        {
            PlayerPrefs.SetString("DecoyKey", "C");
            PlayerPrefs.Save();
        }
    }

    void StartRebind()
    {
        isRebinding = true;
        keyDisplayText.text = "Press a key...";
    }

    void ListenForKeyPress()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    SaveNewKey(keyCode, whichBind);
                    isRebinding = false;
                    UpdateKeyDisplay();
                    break;
                }
            }
        }
    }

    void SaveNewKey(KeyCode newKey, int which)
    {
        keyData.GetComponent<KeyBindData>().SaveNewKey(newKey, which);
        if(whichBind == 0)
        { 
          
           PlayerPrefs.SetString("SkillTreeOpen", newKey.ToString());
        }
        
        else if(whichBind == 1)
        {
          PlayerPrefs.SetString("DashKey", newKey.ToString());
        }
        else if(whichBind == 2)
        {
            PlayerPrefs.SetString("DecoyKey", newKey.ToString());
        }

    }

    void UpdateKeyDisplay()
    {
          //Debug.Log("I run");
        if(whichBind == 0)
        { 
          
            abilityKeyString = PlayerPrefs.GetString("SkillTreeOpen", "T");
        }
        
        else if(whichBind == 1)
        {
          abilityKeyString = PlayerPrefs.GetString("DashKey", "Q"); 
        }
        else if(whichBind == 2)
        {
             abilityKeyString = PlayerPrefs.GetString("DecoyKey", "C");
        }

        if(keyDisplayText!=null)
        keyDisplayText.text = abilityKeyString;
    }
}
