using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static Currency main;
    public int currency;
    // Start is called before the first frame update
    private void Start()
    {
        currency = 30;
    }
    private void Awake()
    {
        main = this;
    }

    public bool subtractCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            return true;
        }
        else { return false; }
    }

    public void addCurrency(int amount)
    {
        currency += amount;
    }

    public int getCurrency()
    {
        return currency;
    }
}
