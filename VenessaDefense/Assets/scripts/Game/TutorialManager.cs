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
    public GameObject plot;
    public GameObject trainer;
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
        trainer.SetActive(false); 
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
                Time.timeScale = 1f;
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
                Time.timeScale = 1f;
                //popUps[popUpIndex].SetActive(true);
            }


        }
        
        else if (popUpIndex == 2) // shoot
        {
            popUps[popUpIndex].SetActive(true);
            Time.timeScale = 1f;
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
            Time.timeScale = 1f;
            if (ant != null)
            {
                ant.SetActive(true);
            }
            
            
            if (waves.enemiesDead > 0)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        
        else if (popUpIndex == 4) // currency
        {
            popUps[popUpIndex].SetActive(true);
            Time.timeScale = 1f;
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
            Time.timeScale = 1f;
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
            ant2.SetActive(true);
            Time.timeScale = 1f;
            if (ant2 != null)
                ant2.SetActive(true);
            if (waves.getDeadEnemies() > 1)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        else if (popUpIndex == 7) // Skills
        {
            popUps[popUpIndex].SetActive(true);
            trainer.SetActive(true);  
            Time.timeScale = 1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        
        }
    
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                movementCounter++;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            shootCount++;
        }


    
}
