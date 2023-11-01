using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public event Action<int> OnEnemiesDeadUpdated;

    private float spawnInterval = 1.0f;
    public float bufferTime = 10.0f;

    private int currentWave = 0;
    public int enemiesDead = 0;

    private bool isWaveInProgress = false;
    private bool waitToStartRound = true;
    private bool isWaveTextCreated = false;

    public int level = 1;

    private List<Transform> spawnerTransforms = new List<Transform>();
    public GameObject[] enemyPrefabs;

    public Text waveText;
    public GameObject uiCanvas;

    EnemyIntroManager enemyIntroManager = null;
    SoundEffectManager soundEffectManager = null;

    private float spawnRateModifier = 1.0f;
    private float mutationRateModifier = 0.0f;

    public enum Enemies
    {
        Ant,
        Beetle,
        Spider,
        ExplodingAnt,
        ExplodingBeetle,
    }

    public class EnemyWave
    {
        public int waveNumber;
        public Dictionary<Enemies, int> enemyCounts = new Dictionary<Enemies, int>();
    }

    public readonly List<EnemyWave> level1Waves = new List<EnemyWave>
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
                { Enemies.ExplodingAnt, 2 }
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
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawnerObjects)
        {
            spawnerTransforms.Add(spawner.transform);
        }

        soundEffectManager = GetComponent<SoundEffectManager>();
        enemyIntroManager = GetComponent<EnemyIntroManager>();

        if (soundEffectManager == null)
            throw new ArgumentNullException("Sound Effect Manager not found");

        if (enemyIntroManager == null)
            throw new ArgumentNullException("Enemy Intro Manager not found");

        if (spawnerObjects == null)
            throw new ArgumentNullException("No spawner objects found");
    }

    void Update()
    {
        if (!WaveFinished()) return;
        if (CheckLevelCleared())
        {
            ManageScenes manageScenes = new ManageScenes();
            manageScenes.StartDayClearedScene(level);
            return;
        }

        if (!waitToStartRound)
        {
            Debug.Log($"Starting wave {currentWave}");
            StartNextWave();
        }

        if (waitToStartRound && !isWaveTextCreated)
        {
            StartCoroutine(enemyIntroManager.DisplayNewIntros(currentWave));
            StartCoroutine(StartWaveText(bufferTime));
        }

    }

    private void StartNextWave()
    {
        isWaveInProgress = true;
        waitToStartRound = true;

        StartCoroutine(StartWave(GetWave(level, currentWave)));
        currentWave++;
    }
    private EnemyWave GetWave(int level, int wave)
    {
        EnemyWave enemyWave = null;

        if (level == 1)
        {
            enemyWave = level1Waves[wave];
        }

        else
            throw new ArgumentException($"{level} does not have waves setup");

        return enemyWave;
    }

    private List<GameObject> GetEnemiesToSpawn(EnemyWave wave)
    {
        if (wave == null)
            throw new ArgumentNullException("Cannot use an enemy wave that is not instantiated");

        if (wave.enemyCounts.Count == 0)
            throw new ArgumentException($"Enemy wave {wave.waveNumber} must have enemies");

        List<GameObject> enemies = new List<GameObject>();

        foreach (var keyValuePair in wave.enemyCounts)
        {
            Enemies enemyType = keyValuePair.Key;
            for (int i = 0; i < Math.Round(keyValuePair.Value * spawnRateModifier); i++)
                enemies.Add(enemyPrefabs[(int)MutateEnemy(enemyType)]);
        }

        enemies.Shuffle();

        return enemies;
    }
    private void SpawnEnemy(GameObject enemy)
    {
        Vector3 enemySpawnPosition = GetRandomSpawnerLocation();

        Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
    }

    private bool WaveFinished()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (!isWaveInProgress && enemyObjects.Length == 0)
            return true;

        return false;
    }
    private bool CheckLevelCleared() { return (WaveFinished() && (currentWave > level1Waves.Count)); }

    private IEnumerator StartWave(EnemyWave wave)
    {

        soundEffectManager.PlayRoundStartSound();

        List<GameObject> enemies = GetEnemiesToSpawn(wave);

        foreach (GameObject enemy in enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(spawnInterval);
        }

        isWaveInProgress = false;
    }
    private IEnumerator Delay(float seconds)
    { yield return new WaitForSeconds(seconds); }
    private IEnumerator StartWaveText(float seconds)
    {
        isWaveTextCreated = true;

        Text text = Instantiate(waveText);
        text.transform.SetParent(uiCanvas.GetComponent<RectTransform>(), false);

        yield return new WaitForSeconds(seconds);

        Destroy(text);

        waitToStartRound = false;
        isWaveTextCreated = false;

    }

    private Enemies MutateEnemy(Enemies enemy)
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (mutationRateModifier == 0 || mutationRateModifier < randomValue) return enemy;

        return MutatedEnemy(enemy);
    }
    private Enemies MutatedEnemy(Enemies enemy)
    {
        Debug.Log("Enemy mutated");
        if (enemy == Enemies.Ant)
            return Enemies.ExplodingAnt;

        else if (enemy == Enemies.Beetle)
            return Enemies.ExplodingBeetle;
        
        return enemy;
    }

    private Vector3 GetRandomSpawnerLocation()
    {
        if (spawnerTransforms.Count == 0)
            throw new ArgumentNullException("There needs to be spawners placed to spawn bugs");

        int randomSpawnerIndex = UnityEngine.Random.Range(0, spawnerTransforms.Count);
        Vector3 enemySpawnPosition = spawnerTransforms[randomSpawnerIndex].position;

        return enemySpawnPosition;
    }

    public void incrementDeadEnemies()
    {
        enemiesDead++;
        OnEnemiesDeadUpdated?.Invoke(enemiesDead);
    }
    public int GetNumberDeadEnemies(){ return enemiesDead; }
    public void SetSpawnRateModifier(float modifier) { spawnRateModifier = modifier; }
    public void SetMutateRateModifier(float modifier) { mutationRateModifier = modifier; }
}
