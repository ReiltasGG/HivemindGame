using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyEffect : MonoBehaviour
{

    public float timeAlive = 0.5f;
    public float countTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if(countTime > timeAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            // Debug.Log("It touched an enemy");
            //Time to make the honey slow down enemies
            var atm = other.GetComponent<Ant>();
            var atm2 = other.GetComponent<Spiders>();
            if(atm !=null)
            {
                
                atm.changeSpeed();
            }
            if(atm2 !=null)
            {
                atm2.changeSpeed();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         if(other.CompareTag("Enemy"))
        {
            // Debug.Log("It touched an enemy");
            //Time to make the honey slow down enemies
            var atm = other.GetComponent<Ant>();
            var atm2 = other.GetComponent<Spiders>();
            if(atm != null)
            {
               
                atm.originalSpeed();
            }
            if(atm2 !=null)
            {
                atm2.originalSpeed();
            }
        }
    }
}
