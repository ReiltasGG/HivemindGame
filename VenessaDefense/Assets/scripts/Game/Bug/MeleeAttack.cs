using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public float attackDelay;
    private float timeBetweenAttack = 0;
    private bool isCollidingWithPlayer = false;
    private Collider2D playerCollider;

    public AttributesManager SelfAttributesManager;
    
    void Start()
    {
        SelfAttributesManager = GetComponent<AttributesManager>();
    }

    void Update()
    {
         timeBetweenAttack -= Time.deltaTime;
        if (isCollidingWithPlayer)
            Attack(playerCollider);
    }

    private void Attack(Collider2D playerCollider)
    {
        if (playerCollider is null) throw new ArgumentNullException("Player collider passed to Attack must not be null");
        if (timeBetweenAttack > 0) return;

        SelfAttributesManager.dealDamage(playerCollider.gameObject);
        timeBetweenAttack = attackDelay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.gameObject.GetComponent<Collider2D>();

        if (collider != null && collider.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
            playerCollider = collider;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Collider2D collider = collision.gameObject.GetComponent<Collider2D>();

        if (collider != null && collider.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;
            playerCollider = null;
        }
    }
}
