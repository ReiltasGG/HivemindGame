using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;//rigidbody = rb

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 10;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var collidedAttributeManager = collision.GetComponent<AttributesManager>();

            if (collidedAttributeManager == null)
                throw new ArgumentNullException("The Enemy does not have an attribute manager assigned");

            collidedAttributeManager.takeDamage(bulletDamage);

            Destroy(gameObject);
        }
    }
}
