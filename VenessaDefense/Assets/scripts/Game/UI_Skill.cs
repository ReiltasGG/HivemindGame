using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour
{
    public int SkillID;
    public int cost;
    public int currency;
    public Image connectorOne;
    public Image connectorTwo;
    public GameObject background;

    public GameObject getSkillTree;

    public bool hasBeenBought = false;
    // Start is called before the first frame update
    void Start()
    {
     //   getSkillTree = GameObject.FindWithTag("Skill Tree");

    }

    // Update is called once per frame
    void Update()
    {
        var atm = getSkillTree.GetComponent<UI_SkillTree>();
        if (SkillID == 1 && hasBeenBought == true)
        {
            //Player HP Goes up
            background.GetComponent<Image>().color = new Color32(18, 255, 59, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(1);
            connectorOne.GetComponent<Image>().color = new Color32(253, 255, 0, 255);


        }
        else if(SkillID == 2 && hasBeenBought == true)
        {
            //Player Attack Goes Faster
            background.GetComponent<Image>().color = new Color32(18, 255, 59, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(2);
            connectorTwo.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
        }
        else if(SkillID == 3 && hasBeenBought == true)
        {
             Debug.Log("Skill3 found me");
            bool temp = atm.getSkilldata(1);
            bool temp2 = atm.getSkilldata(2);

            if (temp == true && temp2 == true)
            {
                //Player gets dodge move
                background.GetComponent<Image>().color = new Color32(18, 255, 59, 255);
                //Set SkillID to true
               
                atm.updateSkillDataTrue(3);
                GameObject Player = GameObject.FindWithTag("Player");
                var playerScript = Player.GetComponent<AbilityHolder>();
                playerScript.allowSkill1();
                
            }
            else 
                hasBeenBought = false;

        }
    }

    public void iGotClicked()
    {
        Debug.Log(5);
    }

    public void Buy()
    {
      //  Debug.Log("Runs");
        if (currency >= cost)
        {
          
            hasBeenBought = true;
          
        }
    }
}
