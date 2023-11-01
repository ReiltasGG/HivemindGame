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
    //public GameObject welcome;
    //public static event Action closeButton;
    private int popUpIndex = 0;
    private int movementCounter = 0;
    private int shootCount = 0;



    public GameObject ant;

    /// <summary>
    /// Using this as condition for the enemy pop up.... Couldnt get it to work tho
    /// </summary>




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < popUps.Length; i++)
            popUps[i].SetActive(false);
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
            
            if(movementCounter == 2)
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
            if (shootCount == 1)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        
        else if (popUpIndex == 3) // enemy
        {

            popUps[popUpIndex].SetActive(true);
            Time.timeScale = 1f;
            ant.SetActive(true);
            if (GameObject.FindWithTag("Enemy") == null)
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
            Time.timeScale = 1f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }
        }
        /*
        else if (popUpIndex == 5) // Skills
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                //popUps[popUpIndex].SetActive(true);
            }

        }
        */
        }
    
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                movementCounter++;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            shootCount++;
        }


    
}
