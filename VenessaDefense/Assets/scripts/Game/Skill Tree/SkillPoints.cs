using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoints : MonoBehaviour
{
    //public static int NumberOfSkillPoints = 0;
    private string skillPointsKey = "SkillPoints";
    private int NumberOfSkillPoints = 0;

    void Start()
    {
        NumberOfSkillPoints = PlayerPrefs.GetInt(skillPointsKey, 0);
    }


    public int GetSkillPoints() { return NumberOfSkillPoints; }
    
    public void SpendSkillPoints(int skillPointsSpent) 
    {  
        NumberOfSkillPoints -= skillPointsSpent; 
        SaveSkillPoints();
    }
    public void GainSkillPoints(int skillPointsGained) 
    { 
        NumberOfSkillPoints += skillPointsGained; 
        SaveSkillPoints();
    }
    
    private void SaveSkillPoints()
    {
        PlayerPrefs.SetInt(skillPointsKey, NumberOfSkillPoints);
        PlayerPrefs.Save();
    }

}
