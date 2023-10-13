using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    private float timeWhenLastEnemySpawned = 0f;
    private float spawnInterval = 1.0f;
    public float bufferTime = 15.0f;

    private int currentWave = 1;
    private int enemiesDead = 0;
    private bool isWaveInProgress = false;
    private bool waitToStartRound = false;
    private bool isFinalWaveSpawned = false;
    private bool isWaveTextCreated = false;

    public int level = 1;

    private List<Transform> spawnerTransforms = new List<Transform>();
    [SerializeField] private GameObject[] enemyPrefabs;

    public Text waveText;
    public GameObject uiCanvas;

    private AudioSource roundStartSoundSource;
    public AudioClip roundStartSound;

    enum Enemies
    {
        Ant,
        Beetle,
        Spider,
        ExplodingAnt,
        ExplodingBeetle,
    }

    private class EnemyWave
    {
        public int waveNumber;
        public Dictionary<Enemies, int> enemyCounts = new Dictionary<Enemies, int>();
    }

    List<EnemyWave> level1Waves = new List<EnemyWave>
    {
        new EnemyWave
        {
            waveNumber = 1,
            enemyCounts = new Dictionary<Enemies, int>
            {
                { Enemies.Ant, 5 },
            }
        },
        new EnemyWave
        {
            waveNumber = 2,
            enemyCounts = new Dictionary<Enemies, int>
            {
                { Enemies.Ant, 8 },
                { Enemies.Beetle, 2 },
            }
        },
        new EnemyWave
        {
            waveNumber = 3,
            enemyCounts = new Dictionary<Enemies, int>
            {
                { Enemies.Ant, 10 },
                { Enemies.Beetle, 4 },
                { Enemies.ExplodingAnt, 2 }
            }
        },
        new EnemyWave
        {
            waveNumber = 4,
            enemyCounts = new Dictionary<Enemies, int>
            {
                { Enemies.Ant, 6 },
                { Enemies.Beetle, 3 },
                { Enemies.ExplodingAnt, 4 },
                { Enemies.ExplodingBeetle, 2 },
            }
        },
        new EnemyWave
        {
            waveNumber = 5,
            enemyCounts = new Dictionary<Enemies, int>
            {
                { Enemies.Ant, 10 },
                { Enemies.Beetle, 5 },
                { Enemies.ExplodingAnt, 4 },
                { Enemies.ExplodingBeetle, 3 },
                { Enemies.Spider, 2 }
            }
        },

    };

    void Start()
    {

        roundStartSoundSource = GetComponent<AudioSource>(); ;
        roundStartSoundSource.clip = roundStartSound;

        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawnerObjects)
        {
            spawnerTransforms.Add(spawner.transform);
        }
    
    }

    void Update()
    {
        CheckLevelCleared();

        if (waveFinished() && !waitToStartRound && !isFinalWaveSpawned)
        {
            startNextWave();
        }

        if (waveFinished() && waitToStartRound && !isFinalWaveSpawned && !isWaveTextCreated)
        {
            StartCoroutine(startWaveText(bufferTime));
        }
            
    }

    private void startNextWave()
    {
        isWaveInProgress = true;
        waitToStartRound = true;

        StartCoroutine(startWave(getWave(level, currentWave)));
        currentWave++;
    }

    private EnemyWave getWave(int level, int wave)
    {
        EnemyWave enemyWave = null;

        if (level == 1)
        {
            enemyWave = level1Waves[wave - 1];
        }
            
        else
            throw new ArgumentException($"{level} does not have waves setup");
        
        return enemyWave;
    }

    private bool waveFinished()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (!isWaveInProgress && enemyObjects.Length == 0)
            return true;

        return false;
    }

    private void CheckLevelCleared()
    {

        if (level == 1)
            isFinalWaveSpawned =  currentWave > level1Waves.Count;

        else isFinalWaveSpawned = false;

        Debug.Log($"current Wave: {currentWave} total waves: {level1Waves.Count}\n isFinalWaveSpawned {isFinalWaveSpawned} ");

    }

    private IEnumerator startWave(EnemyWave wave)
    {
        playRoundStartSound();

        List<GameObject> enemies = GetEnemiesToSpawn(wave);

        foreach (GameObject enemy in enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(spawnInterval);
        }

        isWaveInProgress = false;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Vector3 enemySpawnPosition = getRandomSpawnerLocation();

        Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
    }

    private Vector3 getRandomSpawnerLocation()
    {
        if (spawnerTransforms.Count == 0)
            throw new ArgumentNullException("There needs to be spawners placed to spawn bugs");

        int randomSpawnerIndex = UnityEngine.Random.Range(0, spawnerTransforms.Count);
        Vector3 enemySpawnPosition = spawnerTransforms[randomSpawnerIndex].position;

        return enemySpawnPosition;
    }

    private List<GameObject> GetEnemiesToSpawn(EnemyWave wave)
    {
        if (wave == null) 
            throw new ArgumentNullException("Cannot use an enemy wave that is not instantiated");

        if (wave.enemyCounts.Count == 0) 
            throw new ArgumentException ($"Enemy wave {wave.waveNumber} must have enemies");

        List<GameObject> enemies = new List<GameObject>();

        foreach (var keyValuePair in wave.enemyCounts)
        {
            Enemies enemyType = keyValuePair.Key;

            for (int i = 0; i < keyValuePair.Value; i++)
                enemies.Add(enemyPrefabs[(int)enemyType]);
        }

        enemies.Shuffle();

        return enemies;
    }

    void playRoundStartSound()
    {
        if (roundStartSoundSource == null)
            throw new ArgumentNullException("Round start source not instantiated");

        if (roundStartSoundSource.clip == null)
            throw new ArgumentNullException("Round start sound not attached to AudioSource");

        roundStartSoundSource.Play();
    }

    private IEnumerator startWaveText(float seconds)
    {
        isWaveTextCreated = true;

        Text text = Instantiate(waveText);
        waveText.transform.SetParent(uiCanvas.GetComponent<RectTransform>(), false);

        yield return new WaitForSeconds(seconds);

        Destroy(text);

        waitToStartRound = false;
        isWaveTextCreated = false;

    }

    public void incrementDeadEnemies()
    {
        enemiesDead++;
    }
}
