using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
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

        if (health <= 0)
            Despawn();

    }

    public void heal(int amount)
    {
        if (health < 0) {
            Despawn();
            return; 
        }

        health += amount;

        if (health > maxHealth) 
            health = maxHealth;

        healthBar.SetHealth(health);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    public void dealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();

        if(atm != null)
        {
            atm.takeDamage(attackDamage);
        }
    }
}
