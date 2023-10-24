using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera cam;
    public GameObject player;
    private int damage;
    //private int damage = player
    private GameObject wavesFinder;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        var atm = player.GetComponent<AttributesManager>();
        damage = atm.getDamage();
        wavesFinder = GameObject.FindWithTag("GamesManager");
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        var atm = player.GetComponent<AttributesManager>();
        damage = atm.attackDamage;

        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            var collidedAttributeManager = collision.GetComponent<AttributesManager>();

            if (collidedAttributeManager == null)
                throw new ArgumentNullException("The Enemy does not have an attribute manager assigned");
        
            GameObject shamanBee = GameObject.FindWithTag("Tower");
            //checks if shaman bee is present, otherwise ignore the damage boost
            if (shamanBee != null)
            {
                var shamanEffect = shamanBee.GetComponent<ShamanBee>();
                if (shamanEffect != null)
                {
                    float damageBoost = shamanEffect != null ? shamanEffect.GetDamageBoost(collision.gameObject) : 1f;
                    damage = Mathf.RoundToInt(damage * damageBoost);
                }

            }
        
            collidedAttributeManager.takeDamage(damage);

            Destroy(gameObject);

        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
           screenPosition.x > cam.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > cam.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}
