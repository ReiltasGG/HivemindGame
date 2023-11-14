using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabHurtBox : MonoBehaviour
{
    // Start is called before the first frame update
    public bool touchedPlayer = false;
    void Start()
    {
        touchedPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            touchedPlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.gameObject.CompareTag("Player"))
        {
            touchedPlayer = false;
        }
    }
    public bool hasTouchedPlayer()
    {
        return touchedPlayer;
    }


}
