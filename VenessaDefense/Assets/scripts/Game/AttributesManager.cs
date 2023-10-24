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
        if (healthBar != null)
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
        if (gameObject.CompareTag("Enemy"))
            HandleEnemy(gameObject);

        else if (gameObject.CompareTag("Tower")) // add tower effect tag if needed
            HandleTower(gameObject);

        if(gameObject != null)
            Despawn();
    }

    public void HandleEnemy(GameObject enemy)
    {
        IncrementDeadEnamies();
        Currency.main.addCurrency(currencyWorth);
    }

    public void HandleTower(GameObject tower)
    {

        string hivePrefabName = "Hive";
        if (tower.name.StartsWith(hivePrefabName))
            HandleHive(tower);

    }

    public void HandleHive(GameObject tower)
    {
        if (gameObject != null)
            Despawn();

        GameObject gamesManager = FindGamesManager();
        ObjectivesManager objectivesManager = gamesManager.GetComponent<ObjectivesManager>();

        objectivesManager.DestroyHive();

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
        waveScript.incrementDeadEnemies();
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
