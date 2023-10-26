using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public int attackDamage;
    private GameObject wavesFinder;

    public HealthBar healthBar;
    public bool iscurseActive = false;

    [SerializeField] private int currencyWorth;

    private float damageFlashDurationInSeconds = 0.2f;
    private Color spriteDefaultColor;

    private void Start()
    {
        health = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);

        wavesFinder = FindGamesManager();

        spriteDefaultColor = GetSprite().color;
    }

    private GameObject FindGamesManager()
    {
        GameObject gamesManager = GameObject.FindWithTag("GamesManager");

        if (gamesManager is null)
            throw new ArgumentNullException("Games manager not found in scene");

        return gamesManager;
    }

    private Waves FindWavesCode()
    {
        GameObject gamesManager = FindGamesManager();
        return gamesManager.GetComponent<Waves>();
    }

    public int GetDamage()
    { return attackDamage; }

    public void takeDamage(int amount)
    {
        StartCoroutine(DamageFlashAnimation());

        health -= amount;
        if(healthBar!=null)
            healthBar.SetHealth(health);

        if (health <= 0)
            die();
    }

    private IEnumerator DamageFlashAnimation()
    {
        FlashColorRed();

        yield return new WaitForSeconds(damageFlashDurationInSeconds);

        ResetColorToDefault();
    }

    private void FlashColorRed()
    {
        SpriteRenderer sprite = GetSprite();
        sprite.color = Color.red;
    }

    private void ResetColorToDefault()
    {
        Debug.Log("Default Color set");
        SpriteRenderer sprite = GetSprite();
        sprite.color = spriteDefaultColor;
    }

    private SpriteRenderer GetSprite()
    {
        return GetComponent<SpriteRenderer>() != null ? GetComponent<SpriteRenderer>() : transform.Find("Graphics").GetComponent<SpriteRenderer>();

    }

    public void die()
    {
        if (gameObject.CompareTag("Enemy"))
            HandleEnemyDeath(gameObject);

        else if (gameObject.CompareTag("Tower")) // add tower effect tag if needed
            HandleTowerDeath(gameObject);

        else if (gameObject.CompareTag("Player"))
            HandlePlayerDeath(gameObject);

        if (gameObject != null)
            Despawn();
    }

    public void HandlePlayerDeath(GameObject enemy)
    {
        CallGameOverScene();
    }

    public void HandleEnemyDeath(GameObject enemy)
    {
        IncrementDeadEnamies();
        Currency.main.addCurrency(currencyWorth);
    }

    public void HandleTowerDeath(GameObject tower)
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
    private void CallGameOverScene()
    {
        ManageScenes manageScenes = new ManageScenes();
        Waves wavesCode = FindWavesCode();

        manageScenes.StartGameOverScene(wavesCode.enemiesDead);
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
