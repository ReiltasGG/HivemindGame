using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackDelay;
    private float timeBetweenAttack = 0;

    public AttributesManager SelfAttributesManager;
    
    void Start()
    {
    }

    void Update()
    {
         timeBetweenAttack -= Time.deltaTime;
    }

    private void Attack(Collider2D playerCollider)
    {
        if (playerCollider is null) throw new ArgumentNullException("Player collider passed to Attack must not be null");
        if (timeBetweenAttack > 0) return;

        AttributesManager playerAttributeManager = playerCollider.GetComponent<AttributesManager>();

        SelfAttributesManager.dealDamage(playerCollider.gameObject);
        timeBetweenAttack = attackDelay;

        Debug.Log(playerAttributeManager.health);
        Debug.Log(playerAttributeManager.healthBar.GetHealth());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.gameObject.GetComponent<Collider2D>();

        if (collider != null && collider.CompareTag("Player"))
        {
            Attack(collider);
        }

    }
}
