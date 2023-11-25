using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class UI_Skill :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int SkillID;
    public int cost;

    public Image connectorOne;
    public Image connectorTwo;
    public Image connectorThree;
    public Image connectorFour;
    public Image connectorFive;
    public Image connectorSix;
    public Image connectorSeven;
    public Image connectorEight;
    public Image connectorNine;
    public Image connectorTen;
    public Image connectorEleven;

    public GameObject Lock1;
    public GameObject Lock2;
    public GameObject Lock3;
    public GameObject Lock4;
    public GameObject Lock5;
    public GameObject Lock6;
    public GameObject Lock7;
    public GameObject Lock8;
    public GameObject Lock9;
    public GameObject Lock10;
    public GameObject background;

    public GameObject getSkillTree;

    public bool hasBeenBought = false;

    public bool skill12HappenOnce = false;
    public bool skill13HappenOnce = false;


    private GameObject Player;
    private GameObject GamesManager;

    public GameObject toolsTipHealth1, toolsTipHealth2, toolsTipHealth3, toolsTipHealth4, toolsTipHealth5, toolsTipHealth6, toolsTipHealth7,
        toolsTipHealth8, toolsTipHealth9, toolsTipHealth10, toolsTipHealth11, toolsTipHealth12, toolsTipHealth13;
    
    public bool isHovered = false;
    public bool isAvailable = false;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GamesManager = GameObject.FindWithTag("GamesManager");

        hasBeenBought = PlayerPrefs.GetInt("hasBeenBought" + SkillID, 0) == 1;
    }

    void Update()
    {
        var atm = getSkillTree.GetComponent<UI_SkillTree>();
        //This will be for HP Upgrades 1-4

        if (SkillID == 1 && hasBeenBought == true)
        {
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.UnlockSkill(1);
            connectorOne.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            connectorFour.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //Add Health
            if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){

                int hpAdded = 10;
                var attributeManager = Player.GetComponent<AttributesManager>();
                attributeManager.addHealth(hpAdded);
                
                PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                PlayerPrefs.Save();
            }
        }

        else if(SkillID == 2 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(1);
            if(temp == true){
            //Adds Health
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
                atm.UnlockSkill(2);
                connectorTwo.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //Add Health
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    int hpAdded = 10;
                    var attributeManager = Player.GetComponent<AttributesManager>();
                    attributeManager.addHealth(hpAdded);
                    
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        else if(SkillID == 3 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(2);
            if(temp == true){
            //Adds Health
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
                atm.UnlockSkill(3);
                connectorThree.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //Add Health
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    int hpAdded = 10;
                    var attributeManager = Player.GetComponent<AttributesManager>();
                    attributeManager.addHealth(hpAdded);
                    
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
         
        }

        else if(SkillID == 4 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(3);
            if(temp == true){
            //Adds Health
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
                atm.UnlockSkill(4);
                connectorFour.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //Add Health
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    int hpAdded = 10;
                    var attributeManager = Player.GetComponent<AttributesManager>();
                    attributeManager.addHealth(hpAdded);
                    
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        //Skills for Attack Speed Increase (1-3)
        else if (SkillID == 5 && hasBeenBought == true)
        {
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.UnlockSkill(5);
            connectorFive.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            connectorSix.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //increase bullet speed
            if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                var temper = Player.GetComponent<PlayerShoot>();
                temper.fireRateChange(.05f);
                
                PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                PlayerPrefs.Save();
            }
        }

         else if (SkillID == 6 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(5);
            if(temp == true){ 
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.UnlockSkill(6);
                connectorSeven.GetComponent<Image>().color = new Color32(253, 255, 0, 255);

                //bullet speed
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    var temper = Player.GetComponent<PlayerShoot>();
                    temper.fireRateChange(.05f);
                
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        else if (SkillID == 7 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(6);
            if(temp == true){ 
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.UnlockSkill(7);
            
                //Add Bullet speed
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    var temper = Player.GetComponent<PlayerShoot>();
                    temper.fireRateChange(.05f);
                    
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        //Skills for Damage (1-4)
        else if (SkillID == 8 && hasBeenBought == true)
        {
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.UnlockSkill(8);
            connectorEight.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            //Add Damage
            if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                var temper = Player.GetComponent<AttributesManager>();
                temper.addDamage(5);

                PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                PlayerPrefs.Save();
            } 
        }

        else if (SkillID == 9 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(8);
            if(temp == true){
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.UnlockSkill(9);
                connectorNine.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
                //Add Damage
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    var temper = Player.GetComponent<AttributesManager>();
                    temper.addDamage(5);

                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
        }

        else if (SkillID == 10 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(9);
            if(temp == true){  
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.UnlockSkill(10);
                connectorTen.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
                //Add Damage
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    var temper = Player.GetComponent<AttributesManager>();
                    temper.addDamage(5);
                    
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }

            }
        }

        else if (SkillID == 11 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(10);
            if(temp == true){
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.UnlockSkill(11);
            
                //Add Damage
                if(!PlayerPrefs.HasKey("SkillLogicExecuted" + SkillID)){
                    var temper = Player.GetComponent<AttributesManager>();
                    temper.addDamage(5);
                    
                    PlayerPrefs.SetInt("SkillLogicExecuted" + SkillID, 1);
                    PlayerPrefs.Save();
                }
            }
        }
        //Dash && Decoy
        else if(SkillID == 12 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(1);
            bool temp2 = atm.getSkilldata(5);

            if (temp == true && temp2 == true){
                //Player gets dodge move
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
               
                atm.UnlockSkill(12);
                GameObject Player = GameObject.FindWithTag("Player");
                var playerScript = Player.GetComponent<AbilityHolder>();
                playerScript.allowSkill1();
            }
           
            }
             //Decoy
            else if(SkillID == 13 && hasBeenBought == true)
            {
                Debug.Log("Runs");
                bool temp1 = atm.getSkilldata(12);
                if(temp1 == true){
                    background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                    connectorEleven.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
                    atm.UnlockSkill(13);
                    GameObject Player = GameObject.FindWithTag("Player");
                    var playerScript = Player.GetComponent<AbilityHolder>();
                    playerScript.allowSkill2();
                }

                else {hasBeenBought = false;}
        }
        
    hasBeenBought = PlayerPrefs.GetInt("hasBeenBought" + SkillID, 0) == 1;
    checkAvailabiltiy();   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if(SkillID == 1)
        {
            toolsTipHealth1.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }

    public void Buy()
    {
        if (GetSkillPoints() < cost) return;

        SpendSkillPoints(cost);
        UpdateSkillPointText();

        hasBeenBought = true;
        PlayerPrefs.SetInt("hasBeenBought" + SkillID, hasBeenBought ? 1 : 0);
        PlayerPrefs.Save();
    }


    

    private void UpdateSkillPointText()
    {
        GameObject SkillTreeOpener = GameObject.FindWithTag("SkillTreeOpener");
        UI_SkillTreeOpener skillTreeOpenerScript = SkillTreeOpener.GetComponent<UI_SkillTreeOpener>();

        skillTreeOpenerScript.UpdateSkillPointText();
    }

    private int GetSkillPoints()
    {
        SkillPoints skillPointsManager = GamesManager.GetComponent<SkillPoints>();
        return skillPointsManager.GetSkillPoints();
    }

    private void SpendSkillPoints(int skillPoints)
    {
        SkillPoints skillPointsManager = GamesManager.GetComponent<SkillPoints>();
        skillPointsManager.SpendSkillPoints(skillPoints);
    }

    public void skillHover()
    {
        //Activates once player goes over skill with mouse
       // Debug.Log("U hovered me bruh");
        if(SkillID == 1)
            toolsTipHealth1.SetActive(true);
        else if(SkillID == 2)
            toolsTipHealth2.SetActive(true);
        else if(SkillID == 3)
            toolsTipHealth3.SetActive(true);
        else if(SkillID == 4)
            toolsTipHealth4.SetActive(true);
        else if(SkillID == 5)
            toolsTipHealth5.SetActive(true);
        else if(SkillID == 6)
            toolsTipHealth6.SetActive(true);
        else if(SkillID == 7)
            toolsTipHealth7.SetActive(true);
        else if(SkillID == 8)
            toolsTipHealth8.SetActive(true);
        else if(SkillID == 9)
            toolsTipHealth9.SetActive(true);
        else if(SkillID == 10)
            toolsTipHealth10.SetActive(true);
        else if(SkillID == 11)
            toolsTipHealth11.SetActive(true);
        else if(SkillID == 12)
            toolsTipHealth12.SetActive(true);
        else if(SkillID == 13)
            toolsTipHealth13.SetActive(true);
            
    }

    public void skillHoverOff()
    {
        if(SkillID == 1)
            toolsTipHealth1.SetActive(false);
        if(SkillID == 2)
            toolsTipHealth2.SetActive(false);
        if(SkillID == 3)
            toolsTipHealth3.SetActive(false);
        if(SkillID == 4)
            toolsTipHealth4.SetActive(false);
        if(SkillID == 5)
            toolsTipHealth5.SetActive(false);
        if(SkillID == 6)
            toolsTipHealth6.SetActive(false);
        if(SkillID == 7)
            toolsTipHealth7.SetActive(false);
        if(SkillID == 8)
            toolsTipHealth8.SetActive(false);
        if(SkillID == 9)
            toolsTipHealth9.SetActive(false);
        if(SkillID == 10)
            toolsTipHealth10.SetActive(false);
        if(SkillID == 11)
            toolsTipHealth11.SetActive(false);
        if(SkillID == 12)
            toolsTipHealth12.SetActive(false);
        if(SkillID == 13)
            toolsTipHealth13.SetActive(false);
    }
    
    public void checkAvailabiltiy()
    {  
        var atm = getSkillTree.GetComponent<UI_SkillTree>();
        if(atm.getSkilldata(1) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 1, 0) == 1)
        {
            //For HP2
            Lock1.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(2) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 2, 0) == 1)
        {
           //For HP3
             Lock2.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(3) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 3, 0) == 1)
        {
            //For HP4
             Lock3.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(1) == true && atm.getSkilldata(5) == true && GetSkillPoints() >= cost && (PlayerPrefs.GetInt("hasBeenBought" + 1, 0) == 1 && PlayerPrefs.GetInt("hasBeenBought" + 5, 0) == 1))
        {
            Lock4.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(5) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 5, 0) == 1)
        {
            Lock5.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(6) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 6, 0) == 1) 
        {
            Lock6.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(8) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 8, 0) == 1)
        {
            Lock7.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(9) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 9, 0) == 1)
        {
            Lock8.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(10) == true && GetSkillPoints() >= cost && PlayerPrefs.GetInt("hasBeenBought" + 10, 0) == 1)
        {
            Lock9.GetComponent<Image>().enabled = false;
        }
        if(atm.getSkilldata(12) == true && PlayerPrefs.GetInt("hasBeenBought" + 12, 0) == 1)
        {
            Lock10.GetComponent<Image>().enabled = false;
        }
    }
}   
