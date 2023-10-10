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

    private GameObject wavesFinder;
    private Camera cam;
    private int workOnce = 1;
    [SerializeField] private int bulletDamage = 10;

    private Transform target;

    private void Start()
    {
        wavesFinder = GameObject.FindWithTag("GamesManager");
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var collidedAttributeManager = collision.GetComponent<AttributesManager>();

            if (collidedAttributeManager == null)
                throw new ArgumentNullException("The Enemy does not have an attribute manager assigned");

            collidedAttributeManager.takeDamage(bulletDamage);

            if (collidedAttributeManager.health <= 0)
                IncrementDeadEnamies();

            Destroy(gameObject);
        }
    }

    private void IncrementDeadEnamies()
    {
        if (workOnce != 1) return;
        else workOnce = 0;

        var waveScript = wavesFinder.GetComponent<Waves>();
        waveScript.enemiesDeadAdd();
    }

    private void DestroyWhenOffScreen()
    {
        Debug.Log("DestroyWhenOffScreen for tower bullet called");
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
