using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public int attackDamage;
    private GameObject wavesFinder;

    public HealthBar healthBar;
    public bool iscurseActive = false;

    [SerializeField] private int currencyWorth;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        wavesFinder = FindGamesManager();
    }

    private GameObject FindGamesManager()
    {
        GameObject gamesManager = GameObject.FindWithTag("GamesManager");

        if (gamesManager is null)
            throw new ArgumentNullException("Games manager not found in scene");

        return gamesManager;
    }

    public int getDamage()
    { return attackDamage; }

    public void takeDamage(int amount)
    {
        health -= amount;
        if(healthBar!=null)
        healthBar.SetHealth(health);

        if (health <= 0)
            die();
    }

    public void die()
    {
         //Debug.Log("Runs");
        if (gameObject.CompareTag("Enemy"))
        {
             IncrementDeadEnamies();
             Currency.main.addCurrency(currencyWorth);
        }
           

       
        Despawn();
        
    }

    public void heal(int amount)
    {
        if (health < 0)
            return;

        health += amount;

        if (health > maxHealth)
            health = maxHealth;

        healthBar.SetHealth(health);
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
       
    }

    private void IncrementDeadEnamies()
    {
        var waveScript = wavesFinder.GetComponent<Waves>();
        waveScript.enemiesDeadAdd();
    }

    public void dealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();

        if (atm != null)
        {
            atm.takeDamage(attackDamage);
        }
    }

    public void addDamage(int addedDamage)
    {
        attackDamage+= addedDamage;
    }

    public void addHealth(int addHealth)
    {
        health+=addHealth;
        maxHealth+=addHealth;
    }
    
    public void curseActive()
    {
        iscurseActive = true;
    }
    public void curseInactive()
    {
        iscurseActive = false;
    }
    public bool getCurseStatus()
    {
        return iscurseActive;
    }
}
