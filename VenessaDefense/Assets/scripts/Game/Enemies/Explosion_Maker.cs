using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Maker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    void Start()
    {
        //   Explode();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("WE collided");
         var script = Player.GetComponent<AtttributesManager>();
            script.takeDamage(15);
        
        }
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}