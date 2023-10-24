using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Skill :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int SkillID;
    public int cost;
    public int currency;
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

    public GameObject Lock1;
    public GameObject Lock2;
    public GameObject Lock3;
    public GameObject Lock4;
    public GameObject Lock5;
    public GameObject Lock6;
    public GameObject Lock7;
    public GameObject Lock8;
    public GameObject Lock9;
    public GameObject background;

    public GameObject getSkillTree;

    public bool hasBeenBought = false;

    public bool skill1HappenOnce = true;
    public bool skill2HappenOnce = true;
    public bool skill3HappenOnce = true;
    public bool skill4HappenOnce = true;
    public bool skill5HappenOnce = true;
    public bool skill6HappenOnce = true;
    public bool skill7HappenOnce = true;
    public bool skill8HappenOnce = true;
    public bool skill9HappenOnce = true;
    public bool skill10HappenOnce = true;
    public bool skill11HappenOnce = true;
    public bool skill12HappenOnce = true;

    public GameObject findPlayer;
    public GameObject FindGamesManager;




    public bool isHovered = false;
    public bool isAvailable = false;
    // Start is called before the first frame update
    void Start()
    {
     //   getSkillTree = GameObject.FindWithTag("Skill Tree");
      findPlayer = GameObject.FindWithTag("Player");
      FindGamesManager = GameObject.FindWithTag("GamesManager");
    }

    // Update is called once per frame
    void Update()
    {
        var currencyScript = FindGamesManager.GetComponent<Currency>();;
        currency = currencyScript.getCurrency();
       // currency = 
        var atm = getSkillTree.GetComponent<UI_SkillTree>();
        //This will be for HP Upgrades 1-4
        if (SkillID == 1 && hasBeenBought == true)
        {
            //Adds Damage   
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(1);
            connectorOne.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            connectorFour.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //Add Damage
            if(skill1HappenOnce)
            {
            int hpAdded = 10;
            var attributeManager = findPlayer.GetComponent<AttributesManager>();
            attributeManager.addHealth(hpAdded);
            skill1HappenOnce = false;
            }


        }
        else if(SkillID == 2 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(1);
            if(temp == true)
            {
            //Adds Health
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(2);
            connectorTwo.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
         
            
            //Add Health
               if(skill2HappenOnce)
              {
                int hpAdded = 10;
              var attributeManager = findPlayer.GetComponent<AttributesManager>();
              attributeManager.addHealth(hpAdded);
              skill2HappenOnce = false;
                }
            }
         
        }
          else if(SkillID == 3 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(2);
            if(temp == true)
            {
            //Adds Health
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(3);
            connectorThree.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
         
            
            //Add Health
               if(skill3HappenOnce)
              {
                int hpAdded = 10;
              var attributeManager = findPlayer.GetComponent<AttributesManager>();
              attributeManager.addHealth(hpAdded);
              skill3HappenOnce = false;
                }
            }
         
        }
            else if(SkillID == 4 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(3);
            if(temp == true)
            {
            //Adds Health
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(4);
            connectorFour.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
         
            
            //Add Health
               if(skill4HappenOnce)
              {
                int hpAdded = 10;
              var attributeManager = findPlayer.GetComponent<AttributesManager>();
              attributeManager.addHealth(hpAdded);
              skill4HappenOnce = false;
                }
            }
         
        }
        //Skills for Attack Speed Increase (1-3)
        else if (SkillID == 5 && hasBeenBought == true)
        {
            //Adds Damage   
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(5);
            connectorFive.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            connectorSix.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
            //Add Damage
            if(skill5HappenOnce)
            {
            var temper = findPlayer.GetComponent<PlayerShoot>();
            temper.fireRateChange(.05f);
            skill1HappenOnce = false;
            }


        }
         else if (SkillID == 6 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(5);
            if(temp == true)
            {
                //Adds Damage   
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.updateSkillDataTrue(6);
                connectorSeven.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
                //Add Damage
                if(skill6HappenOnce)
                {
                var temper = findPlayer.GetComponent<PlayerShoot>();
                temper.fireRateChange(.05f);
                skill6HappenOnce = false;
                }

            }
        }
         else if (SkillID == 7 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(6);
            if(temp == true)
            {
                //Adds Damage   
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.updateSkillDataTrue(7);
            
                //Add Damage
                if(skill7HappenOnce)
                {
                var temper = findPlayer.GetComponent<PlayerShoot>();
                temper.fireRateChange(.05f);
                skill7HappenOnce = false;
                }

            }
        }
        //Skills for Damage (1-4)
        else if (SkillID == 8 && hasBeenBought == true)
        {
            //Adds Damage   
            background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //Set SkillID to true
            atm.updateSkillDataTrue(8);
            connectorEight.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            //Add Damage
            if(skill8HappenOnce)
            {
            var temper = findPlayer.GetComponent<AttributesManager>();
            temper.addDamage(5);
            skill8HappenOnce = false;
            }

            
        }
        else if (SkillID == 9 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(8);
            if(temp == true)
            {
                //Adds Damage   
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.updateSkillDataTrue(9);
                connectorNine.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
                //Add Damage
                if(skill9HappenOnce)
                {
                var temper = findPlayer.GetComponent<AttributesManager>();
                temper.addDamage(5);
                skill9HappenOnce = false;
                }

            }
        }
         else if (SkillID == 10 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(9);
            if(temp == true)
            {
                //Adds Damage   
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.updateSkillDataTrue(10);
                connectorTen.GetComponent<Image>().color = new Color32(253, 255, 0, 255);
            
                //Add Damage
                if(skill10HappenOnce)
                {
                var temper = findPlayer.GetComponent<AttributesManager>();
                temper.addDamage(5);
                skill10HappenOnce = false;
                }

            }
        }
         else if (SkillID == 11 && hasBeenBought == true)
        {
            bool temp = atm.getSkilldata(10);
            if(temp == true)
            {
                //Adds Damage   
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
                atm.updateSkillDataTrue(11);
            
                //Add Damage
                if(skill11HappenOnce)
                {
                var temper = findPlayer.GetComponent<AttributesManager>();
                temper.addDamage(5);
                skill11HappenOnce = false;
                }

            }
        }
        //Dash
        else if(SkillID == 12 && hasBeenBought == true)
        {
           //  Debug.Log("Skill3 found me");
            bool temp = atm.getSkilldata(1);
            bool temp2 = atm.getSkilldata(5);
            //bool temp3 = atm.getSkilldata(9);

            if (temp == true && temp2 == true)
            {
                //Player gets dodge move
                background.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                //Set SkillID to true
               
                atm.updateSkillDataTrue(12);
                GameObject Player = GameObject.FindWithTag("Player");
                var playerScript = Player.GetComponent<AbilityHolder>();
                playerScript.allowSkill1();
                
            }
            else 
                hasBeenBought = false;

        }
        
        
        hasBeenBought = false;
    

    checkAvailabiltiy();
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        Debug.Log("Button is being hovered.");
        // You can add your code here for when the button is hovered.
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        Debug.Log("Button is not being hovered.");
        // You can add your code here for when the button is no longer hovered.
    }

    public void Buy()
    {
      //  Debug.Log("Runs");
        if (currency >= cost)
        {
          var temper = FindGamesManager.GetComponent<Currency>();
          temper.subtractCurrency(cost);
            hasBeenBought = true;
          Debug.Log("I got bought");
        }
    }

    public void skillHover()
    {
        //Activates once player goes over skill with mouse
        Debug.Log("U hovered me bruh");
    }
    
    public void checkAvailabiltiy()
    {  
        var atm = getSkillTree.GetComponent<UI_SkillTree>();
        if(atm.getSkilldata(1) == true && currency >= cost)
        {
            //For HP2
            Lock1.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(2) == true && currency >= cost)
        {
           //For HP3
             Lock2.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(3) == true && currency >= cost)
        {
            //For HP4
             Lock3.GetComponent<Image>().enabled = false;
        }
         if(atm.getSkilldata(1) == true && atm.getSkilldata(5) == true && currency >= cost)
        {
            Lock4.GetComponent<Image>().enabled = false;

        }
         if(atm.getSkilldata(5) == true && currency >= cost)
        {
            Lock5.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(6) == true && currency >= cost) 
        {
            Lock6.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(8) == true && currency >= cost)
        {
            Lock7.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(9) == true && currency >= cost)
        {
            Lock8.GetComponent<Image>().enabled = false;
        }
          if(atm.getSkilldata(10) == true && currency >= cost)
        {
            Lock9.GetComponent<Image>().enabled = false;
        }
    }
}   
