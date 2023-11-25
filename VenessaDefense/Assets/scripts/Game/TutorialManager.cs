using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Windows;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    public Waves waves;
    public GameObject healthbar;
    public GameObject shop;
    public GameObject plot; //joe mama
    public UI_SkillTreeOpener skillTreeOpener;
    
    //public GameObject welcome;
    //public static event Action closeButton;
    private int popUpIndex = 0;
    private int movementCounter = 0;
    private int shootCount = 0;
   



    public GameObject ant;
    public GameObject ant2;
    /// <summary>
    /// Using this as condition for the enemy pop up.... Couldnt get it to work tho
    /// </summary>




    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < popUps.Length; i++)
            popUps[i].SetActive(false);
        healthbar.SetActive(false);
        ant.SetActive(false);
        ant2.SetActive(false);
        shop.SetActive(false);
        plot.SetActive(false);
        
    }

    // Update is called once per frame and studd
    void Update()
    {
        


        if (popUpIndex == 0) // weclcome
        { 
            popUps[popUpIndex].SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {

                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                if (skillTreeOpener.skillTreeIsOpen == false)
                {
                    Time.timeScale = 1f;
                }
                else
                {
                    Time.timeScale = 0f;
                }
                //popUps[popUpIndex].SetActive(true);
            }


            

        }
        
        else if (popUpIndex == 1) // move
        {
            popUps[popUpIndex].SetActive(true);
            
            if(movementCounter == 1)
            {

                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                if (skillTreeOpener.skillTreeIsOpen == false)
                {
                    Time.timeScale = 1f;
                }
                else
                {
                    Time.timeScale = 0f;
                }
                //popUps[popUpIndex].SetActive(true);
            }


        }
        
        else if (popUpIndex == 2) // shoot
        {
            popUps[popUpIndex].SetActive(true);
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        
        else if (popUpIndex == 3) // enemy
        {

            popUps[popUpIndex].SetActive(true);
            healthbar.SetActive(true);
            shop.SetActive(true);
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (ant != null)
            {
                ant.SetActive(true);
            }
            
            
            if (waves.getDeadEnemies() > 0)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        
        else if (popUpIndex == 4) // currency
        {
            popUps[popUpIndex].SetActive(true);
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        
        else if (popUpIndex == 5) // shop
        {
            popUps[popUpIndex].SetActive(true);
            plot.SetActive(true);
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        else if (popUpIndex == 6) // Enemy Attack with tower
        {
            popUps[popUpIndex].SetActive(true);
            var num = 1;
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (ant2 != null)
            {
                ant2.SetActive(true);
                //Debug.Log(waves.getDeadEnemies());
            }
                
            if (waves.getDeadEnemies() > num)
            {
                //Debug.Log("We have this many dead enemies now" + waves.getDeadEnemies());
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        else if (popUpIndex == 7) //tower description
        {
            popUps[popUpIndex].SetActive(true);
            //trainer.SetActive(true);  
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        else if (popUpIndex == 8) // Skills
        {
            popUps[popUpIndex].SetActive(true);
            if (GetSkillPoints() == 0)
                GainSkillPoints(20);

            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        else if (popUpIndex == 9) //Special Skills
        {
            popUps[popUpIndex].SetActive(true);
            //trainer.SetActive(true);  
            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        else if (popUpIndex == 10) // Finally
        {
            popUps[popUpIndex].SetActive(true);
            
            Time.timeScale = 1f;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            

            if (skillTreeOpener.skillTreeIsOpen == false)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
            }


        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

    }
    
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                movementCounter++;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            shootCount++;
        }
    public void GainSkillPoints(int numberOfSkillPoints)
    {
        SkillPoints skillPoints = FindGamesManager().GetComponent<SkillPoints>();
        if (skillPoints == null)
            throw new ArgumentNullException("Skill Points script must be attached to Games Manager");

        skillPoints.GainSkillPoints(numberOfSkillPoints);
    }

    public int  GetSkillPoints()
    {
        SkillPoints skillPoints = FindGamesManager().GetComponent<SkillPoints>();
        if (skillPoints == null)
            throw new ArgumentNullException("Skill Points script must be attached to Games Manager");

        return skillPoints.GetSkillPoints();
    }

    private GameObject FindGamesManager()
    {
        GameObject GamesManager = GameObject.FindWithTag("GamesManager");
        if (GamesManager == null)
            throw new ArgumentNullException("Games manager is not found");

        return GamesManager;
    }



}
