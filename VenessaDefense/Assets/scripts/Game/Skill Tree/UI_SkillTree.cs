using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UI_SkillTree;


public static class SkillData
{
  public static List<int> skillNumber = new List<int>
  {
      0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13
  };

  public static List<bool> skillUnlocked = new List<bool>
  {
      false, false, false, false, false, false, false, false, false, false, false, false, false, false
  };
}

public class UI_SkillTree : MonoBehaviour { 
    public int currentCurrency = 0;

    public GameObject SkillTree;
   
    void Start()
    {
        SkillTree.SetActive(false);
    }

    public void ChangeHoverColor()
    {
        var colors = GetComponent<Button>().colors;
        colors.normalColor = Color.red;

        GetComponent<Button>().colors = colors;
    }

    public bool getSkilldata(int whichSkill)
    {
        return SkillData.skillUnlocked[whichSkill-1];
    }

    public void UnlockSkill(int whichSkill)
    {
        SkillData.skillUnlocked[whichSkill-1] = true;
    }

    private void ResetSkills()
    {
        int previousDay = PlayerPrefs.GetInt("CurrentDay", 1) - 1;
        if (previousDay >= 1){
            for (int i = 1; i <= 13; i++){
                bool previousDayStatus = PlayerPrefs.GetInt("hasBeenBought" + i, 0) == 1;
                PlayerPrefs.SetInt("hasBeenBought" + i, previousDayStatus ? 1 : 0);
                PlayerPrefs.DeleteKey("SkillLogicExecuted" + i);
            }
            PlayerPrefs.SetInt("CurrentDay", previousDay);
            PlayerPrefs.Save();
        }
    }

    public void ResetSkillsOnGameOver()
    {
        ResetSkills();
    }
    
   
}
