using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int currency;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void subtractCurrency(int amount)
    {
        currency -= amount;
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
