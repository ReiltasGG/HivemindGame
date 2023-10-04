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
    private int workOnce = 1;

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
        GameObject temp = collision.gameObject;

        if (collision.CompareTag("Enemy"))
        {
            var joe = collision.GetComponent<AttributesManager>();

            if (joe != null) // Ensures there's a health bar attached to it
            {
                joe.takeDamage(damage);

                if (joe.health <= 0) // Destroys object if health is 0
                {
                    Destroy(temp);
                    Destroy(gameObject);
                    if (workOnce == 1)
                    {
                        Debug.Log("Tower Bullet has killed");
                        var waveScript = wavesFinder.GetComponent<Waves>();
                        waveScript.enemiesDeadAdd();
                        workOnce = 0;
                    }
                }
            }

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
