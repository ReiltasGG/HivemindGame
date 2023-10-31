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
    private int popUpIndex;
    

    public GameObject ant;
    
    /// <summary>
    /// Using this as condition for the enemy pop up.... Couldnt get it to work tho
    /// </summary>



    
    // Start is called before the first frame update
    void Start()
    {
        //runPopUps();
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
                popUps[popUpIndex].SetActive(true);
            }
            
            

        }
        /*
        else if (popUpIndex == 1) // move
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                //Debug.Log("IF condition met");
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            }
        }
        else if (popUpIndex == 2) // shoot
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            }
        }
        else if (popUpIndex == 3) // enemy
        {

            popUps[popUpIndex].SetActive(true);
            //ant.SetActive(true);
            if (GameObject.FindWithTag("Enemy") == null)
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            }
        }
        else if (popUpIndex == 4) // currency
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            }
        }
        else if (popUpIndex == 5) // shop
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex++;
                popUps[popUpIndex].SetActive(true);
            }
        }
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
    
}
