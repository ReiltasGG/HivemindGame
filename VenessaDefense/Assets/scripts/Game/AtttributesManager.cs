using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtttributesManager : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public int attackDamage;

    public HealthBar healthBar;


    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public int getDamage()
        { return attackDamage; }

    public void takeDamage(int amount)
    {
        health -= amount;

        healthBar.SetHealth(health);
    }

    public void dealDamage(GameObject target)
    {
        var atm = target.GetComponent<AtttributesManager>();

        if(atm != null)
        {
            atm.takeDamage(attackDamage);
        }
    }
}
