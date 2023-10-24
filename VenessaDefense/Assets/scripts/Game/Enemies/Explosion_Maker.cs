using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Maker : MonoBehaviour
{
    public int explosionDamage = 20;
    // Start is called before the first frame update
    public GameObject Player;
    public AttributesManager SelfAttributesManager;

    void Start()
    {
        //   Explode();
        Player = GameObject.FindWithTag("Player");
        SelfAttributesManager = GetComponent<AttributesManager>();

        if (SelfAttributesManager == null )
            throw new ArgumentNullException("Explosion Attribute manager is null");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var script = Player.GetComponent<AttributesManager>();
            script.takeDamage(explosionDamage);
        }
        if(other.CompareTag("Tower"))
        {
            var script = other.GetComponent<AttributesManager>();
            script.takeDamage(explosionDamage);
        }
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
        SelfAttributesManager.die();
    }
}