using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UI_SkillTree;

public class UI_SkillTree : MonoBehaviour { 
    private Button button;
    private Color originalColor;

    public int currentCurrency = 0;
    public Currency GameManager; // Assuming Currency is the correct script type

    public GameObject SkillTree;
    public class characterSkills
    {
        public int skillNumber;
        public bool skillExist;
       


        // Add more enemy types as needed
    }


    public List<characterSkills> listOfSkills = new List<characterSkills>
    {
    new characterSkills { skillNumber = 0, skillExist = false},
    new characterSkills { skillNumber = 1, skillExist = false },
    new characterSkills { skillNumber = 2, skillExist = false },
    new characterSkills { skillNumber = 3, skillExist = false },
    new characterSkills { skillNumber = 4, skillExist = false },
    new characterSkills { skillNumber = 5, skillExist = false },
    new characterSkills { skillNumber = 6, skillExist = false },
    new characterSkills { skillNumber = 7, skillExist = false },
    new characterSkills { skillNumber = 8, skillExist = false },
    new characterSkills { skillNumber = 9, skillExist = false },
    new characterSkills { skillNumber = 10, skillExist = false },
    new characterSkills { skillNumber = 11, skillExist = false },
    new characterSkills { skillNumber = 12, skillExist = false },
    new characterSkills { skillNumber = 13, skillExist = false },
    };

    void Start()
    {
        SkillTree.SetActive(false);
        button = GetComponent<Button>();
        //originalColor = button.colors.normalColor;
        GameManager = GameObject.FindWithTag("GamesManager").GetComponent<Currency>();
    }

    void Update()
    {
        //currentCurrency = GameManager.getCurrency();
    }

   

    public void ChangeHoverColor()
    {

        var colors = GetComponent<Button>().colors;
        colors.normalColor = Color.red;
        GetComponent<Button>().colors = colors;
    }

    public void openSkillTree()
    {

    }

    public bool getSkilldata(int whichSkill)
    {
        bool temp = listOfSkills[whichSkill-1].skillExist;
        return temp;

    }

    public void updateSkillDataTrue(int whichSkill)
    {
        listOfSkills[whichSkill-1].skillExist = true;
    }


   
}
