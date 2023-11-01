using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UI_SkillTree;

public class UI_SkillTree : MonoBehaviour { 
    public int currentCurrency = 0;

    public GameObject SkillTree;
    public class characterSkills
    {
        public int skillNumber;
        public bool skillUnlocked;
    }

    public List<characterSkills> listOfSkills = new List<characterSkills>
    {
        new characterSkills { skillNumber = 0, skillUnlocked = false},
        new characterSkills { skillNumber = 1, skillUnlocked = false },
        new characterSkills { skillNumber = 2, skillUnlocked = false },
        new characterSkills { skillNumber = 3, skillUnlocked = false },
        new characterSkills { skillNumber = 4, skillUnlocked = false },
        new characterSkills { skillNumber = 5, skillUnlocked = false },
        new characterSkills { skillNumber = 6, skillUnlocked = false },
        new characterSkills { skillNumber = 7, skillUnlocked = false },
        new characterSkills { skillNumber = 8, skillUnlocked = false },
        new characterSkills { skillNumber = 9, skillUnlocked = false },
        new characterSkills { skillNumber = 10, skillUnlocked = false },
        new characterSkills { skillNumber = 11, skillUnlocked = false },
        new characterSkills { skillNumber = 12, skillUnlocked = false },
        new characterSkills { skillNumber = 13, skillUnlocked = false },
    };

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
        return listOfSkills[whichSkill-1].skillUnlocked;
    }

    public void UnlockSkill(int whichSkill)
    {
        listOfSkills[whichSkill-1].skillUnlocked = true;
    }
   
}
