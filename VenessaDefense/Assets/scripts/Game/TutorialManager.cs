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
    public GameObject welcome;
    //public static event Action closeButton;
    private int popUpIndex;
    

    public GameObject enemy;
    /// <summary>
    /// Using this as condition for the enemy pop up.... Couldnt get it to work tho
    /// </summary>




    // Start is called before the first frame update
    void Start()
    {
        welcome.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void runPopUps()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
        }

        if (popUpIndex == 0) // Movement Pop Up
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                popUpIndex++;

        }
        else if (popUpIndex == 1) // Shooting Pop Up
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                popUpIndex++;
        }
        else if (popUpIndex == 2) // Enemy
        {
            if (Input.GetKeyDown(KeyCode.Space))
                popUpIndex++;
        }
        else if (popUpIndex == 3) // Currency
        {
            if (Input.GetKeyDown(KeyCode.Space))
                popUpIndex++;
        }
        else if (popUpIndex == 4) // Shop
        {
            if (Input.GetKeyDown(KeyCode.Space))
                popUpIndex++;
        }
        else if (popUpIndex == 5) // Skills
        {
            if (Input.GetKeyDown(KeyCode.Space))
                popUpIndex++;
        }
    }
}
