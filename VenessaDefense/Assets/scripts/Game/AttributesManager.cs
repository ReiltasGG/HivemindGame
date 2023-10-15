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
        healthBar.SetHealth(health);

        if (health <= 0)
            die();
    }

    public void die()
    {
        Currency.main.addCurrency(currencyWorth);
        Despawn();

        if (gameObject.CompareTag("Enemy"))
            IncrementDeadEnamies();

        else if (gameObject.CompareTag("Player"))
        {
            ManageGameOver manageGameOver = new ManageGameOver();
            manageGameOver.StartGameOverScreen(GetNumberDeadEnemies());
        }

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
        Destroy(gameObject);
    }

    private void IncrementDeadEnamies()
    {
        var waveScript = wavesFinder.GetComponent<Waves>();

        waveScript.incrementDeadEnemies();
    }

    private int GetNumberDeadEnemies()
    {
        var waveScript = wavesFinder.GetComponent<Waves>();

        return waveScript.getNumberDeadEnemies();
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
}
