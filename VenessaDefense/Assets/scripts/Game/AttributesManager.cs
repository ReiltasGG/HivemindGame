using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int attackDamage;

    private GameObject wavesFinder;
    private UI_SkillTree skillTree;

    public HealthBar healthBar;
    public bool iscurseActive = false;

    [SerializeField] private int currencyWorth;

    private float damageFlashDurationInSeconds = 0.2f;
    private Color spriteDefaultColor;
    private Color prefabSpriteDefaultColor;
    public bool playerHitOnce = false;

    private void Start()
    {
        LoadPlayerStats();
        health = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);

        wavesFinder = GamesManager();

        spriteDefaultColor = GetSprite().color;
        prefabSpriteDefaultColor = spriteDefaultColor;
    }

    public void SetSpriteDefaultColor(Color color)
    {
        spriteDefaultColor = color;
    }
    public Color GetSpriteDefaultColor()
    {
        return spriteDefaultColor;
    }
    public void ResetSpriteDefaultColor()
    {
        spriteDefaultColor = prefabSpriteDefaultColor;
    }

    private GameObject GamesManager()
    {
        GameObject gamesManager = GameObject.FindWithTag("GamesManager");

        if (gamesManager is null)
            throw new ArgumentNullException("Games manager not found in scene");

        return gamesManager;
    }

    private Waves FindWavesCode()
    {
        GameObject gamesManager = GamesManager();
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

    public void ResetColorToDefault()
    {
        SpriteRenderer sprite = GetSprite();
        sprite.color = spriteDefaultColor;
    }

    public SpriteRenderer GetSprite()
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
        SavePlayerStats();
        PlayerShoot playerBullet = gameObject.GetComponent<PlayerShoot>();
        float bulletSpeed = playerBullet.fireRateReturn();
        float previousbulletSpeed = PlayerPrefs.GetFloat("PreviousScenePlayerBulletSpeed", bulletSpeed);

        playerBullet.fireRateSet(previousbulletSpeed);
        maxHealth = PlayerPrefs.GetInt("PreviousScenePlayerMaxHealth", maxHealth);
        attackDamage = PlayerPrefs.GetInt("PreviousScenePlayerAttackDamage", attackDamage);
        CallGameOverScene();
    }

    public void HandleEnemyDeath(GameObject enemy)
    {
        
        IncrementDeadEnamies();

        ShowCurrency(enemy.transform.position, currencyWorth);
        Currency.main.addCurrency(currencyWorth);

        PlayEnemyDieSound();
        
    }

    private void ShowCurrency(Vector3 position, int currencyWorth)
    {
        CurrencyDropText currencyDrop = GetCurrencyDropText();
        currencyDrop.ShowCurrency(position, currencyWorth);
    }
    private CurrencyDropText GetCurrencyDropText()
    {
        GameObject gameManager = GameObject.FindWithTag("GamesManager");

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return null;
        }

        return gameManager.GetComponent<CurrencyDropText>();

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

        GameObject gamesManager = GamesManager();
        ObjectivesManager objectivesManager = gamesManager.GetComponent<ObjectivesManager>();

        objectivesManager.DestroyHive();

    }
    private void CallGameOverScene()
    {
        Transform findSkillTree = GameObject.Find("Skill Tree Canvas")?.transform.Find("Skill Tree");
        skillTree = findSkillTree.GetComponent<UI_SkillTree>();
        skillTree.ResetSkillsOnGameOver();

        ManageScenes manageScenes = new ManageScenes();
        Waves wavesCode = FindWavesCode();
        manageScenes.CurrentDayScene(wavesCode.level);
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
        if(gameObject.name != "Wasp")
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

    public void addHealthSkill(int addHealth)
    {
        maxHealth += addHealth;
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

    private void PlayEnemyDieSound()
    {
        SoundEffectManager soundEffectManager = GamesManager().GetComponent<SoundEffectManager>();

        soundEffectManager.PlayEnemyDeathSound();
    }

    public void SavePlayerStats()
    {
     if (gameObject.CompareTag("Player"))
     {
        PlayerShoot playerBullet = gameObject.GetComponent<PlayerShoot>();
        float bulletSpeed = playerBullet.fireRateReturn();

        PlayerPrefs.SetFloat("PreviousScenePlayerBulletSpeed", bulletSpeed);
        PlayerPrefs.SetInt("PreviousScenePlayerMaxHealth", maxHealth);
        PlayerPrefs.SetInt("PreviousScenePlayerAttackDamage", attackDamage);

        PlayerPrefs.SetFloat("PlayerBulletSpeed", bulletSpeed);
        PlayerPrefs.SetInt("PlayerMaxHealth", maxHealth);
        PlayerPrefs.SetInt("PlayerAttackDamage", attackDamage);
        PlayerPrefs.Save();
     }
    }

   public void LoadPlayerStats()
   {
     if (gameObject.CompareTag("Player"))
     {
        PlayerShoot playerBullet = gameObject.GetComponent<PlayerShoot>();
        float bulletSpeed = playerBullet.fireRateReturn();
        float bullet = PlayerPrefs.GetFloat("PlayerBulletSpeed", bulletSpeed);

        playerBullet.fireRateSet(bullet);
        maxHealth = PlayerPrefs.GetInt("PlayerMaxHealth", maxHealth);
        attackDamage = PlayerPrefs.GetInt("PlayerAttackDamage", attackDamage);
     }
   }

   public int getHealth()
   {
    return health;
   }

   public int getCurrency()
   {
    return currencyWorth;
   }

}
