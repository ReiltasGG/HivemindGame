using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public int numberOfWaves = 20;
    public int wavesPerDay = 3;
    public int enemiesPerWave = 5;
    public float spawnInterval = 1.0f;
    public float bufferTime = 30.0f;

    private int currentWave = 0;
    private int currentDay = 1;
    private bool spawning = false;
    private float timeSinceLastSpawn = 0f;
    private float timeSinceWaveCompleted = 0f;
    private bool waveCompleted = false;
    private List<Transform> spawnerTransforms = new List<Transform>();
    [SerializeField] private GameObject[] enemyPrefabs;
    public Text waveText;
    public GameObject uiCanvas;

    private int remainingEnemiesInWave;


    public int enemiesDead = 0;

    public class EnemyWave
    {
        public int waveNumber;
        public int ants;
        public int beetles;
        public int explodingAnt;
        public int explodingBeetle;
        public int spider;
        public int totalEnemies;
        // Add more enemy types as needed
    }

    public List<EnemyWave> waves = new List<EnemyWave>
{
    new EnemyWave { waveNumber = 1, ants = 4, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 6},
    new EnemyWave { waveNumber = 2, ants = 5, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 7},
    new EnemyWave { waveNumber = 3, ants = 7, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 10},
    new EnemyWave { waveNumber = 4, ants = 6, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 12},
    new EnemyWave { waveNumber = 5, ants = 4, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 5},
    new EnemyWave { waveNumber = 6, ants = 5, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 7},
    new EnemyWave { waveNumber = 7, ants = 7, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0,  totalEnemies = 10},
    new EnemyWave { waveNumber = 8, ants = 6, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 12},
    new EnemyWave { waveNumber = 9, ants = 4, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 5},
    new EnemyWave { waveNumber = 10, ants = 5, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 7},
    new EnemyWave { waveNumber = 11, ants = 7, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0,totalEnemies = 10},
    new EnemyWave { waveNumber = 12, ants = 6, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 12},
    new EnemyWave { waveNumber = 13, ants = 4, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 5},
    new EnemyWave { waveNumber = 14, ants = 5, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 7},
    new EnemyWave { waveNumber = 15, ants = 7, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 10},
    new EnemyWave { waveNumber = 16, ants = 6, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 12},
    new EnemyWave { waveNumber = 17, ants = 4, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 5},
    new EnemyWave { waveNumber = 18, ants = 5, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 7},
    new EnemyWave { waveNumber = 19, ants = 7, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 10},
    new EnemyWave { waveNumber = 20, ants = 6, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 12},
    // Define more waves as needed
};

    void Start()
    {
        // Find all GameObjects with the "Spawner" tag and add their transforms to the list
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawnerObjects)
        {
            spawnerTransforms.Add(spawner.transform);
        }

        StartWave();
    }

    void Update()
    {

        if (spawning && !waveCompleted)
        {
            Debug.Log(enemiesDead + " :)");
            int currentWaveTemp = currentWave - 1;

            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                if (waves[currentWaveTemp].ants != 0)
                {
                    waves[currentWaveTemp].ants = waves[currentWaveTemp].ants - 1;
                    SpawnEnemy(0);
                    timeSinceLastSpawn = 0f;
                }
                else if (waves[currentWaveTemp].beetles != 0)
                {
                    waves[currentWaveTemp].beetles = waves[currentWaveTemp].beetles - 1;
                    SpawnEnemy(1);
                    timeSinceLastSpawn = 0f;
                }
                else if (waves[currentWaveTemp].explodingAnt != 0)
                {
                    waves[currentWaveTemp].explodingAnt = waves[currentWaveTemp].explodingAnt - 1;
                    SpawnEnemy(2);
                    timeSinceLastSpawn = 0f;
                }
                else if (waves[currentWaveTemp].explodingBeetle != 0)
                {
                    waves[currentWaveTemp].explodingBeetle = waves[currentWaveTemp].explodingAnt - 1;
                    SpawnEnemy(3);
                    timeSinceLastSpawn = 0f;
                }
                else if (waves[currentWaveTemp].spider != 0)
                {
                    waves[currentWaveTemp].spider = waves[currentWaveTemp].explodingAnt - 1;
                    SpawnEnemy(4);
                    timeSinceLastSpawn = 0f;
                }
            }
        }
        else if (waveCompleted)
        {
            timeSinceWaveCompleted += Time.deltaTime;
            if (timeSinceWaveCompleted >= bufferTime)
            {
                StartWave();
                waveCompleted = false;
            }
        }
        // Debug.Log(enemiesDead + " " + enemiesPerWave);

        if (enemiesDead == enemiesPerWave)
        { //Debug.Log("Wave " + currentWave + " completed.");

            if (currentWave % wavesPerDay == 0)
            {
                currentDay++;
                //Debug.Log("Starting Day " + currentDay);
            }
            StartCoroutine(StartWaveWithDelay(bufferTime));

        }

    }

    void StartWave()
    {
        if (currentWave < numberOfWaves)
        {
            enemiesDead = 0;
            currentWave++;
            int currentWaveTemp = currentWave - 1;
            enemiesPerWave = waves[currentWaveTemp].totalEnemies;
            SpawnWave();
        }
        else
        {
            Debug.Log("All waves completed");
        }
    }

    void SpawnWave()
    {
        spawning = true;
        timeSinceLastSpawn = 0f;
        remainingEnemiesInWave = enemiesPerWave; // Initialize remaining enemies
    }

    void SpawnEnemy(int whichEnemy)
    {
        if (remainingEnemiesInWave > 0)
        {
            if (spawnerTransforms.Count > 0)
            {

                int rand = whichEnemy;







                GameObject enemyToSpawn = enemyPrefabs[rand];

                int randomSpawnerIndex = Random.Range(0, spawnerTransforms.Count);
                Vector3 spawnPosition = spawnerTransforms[randomSpawnerIndex].position;
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("No spawners found.");
            }

            remainingEnemiesInWave--; // Decrease remaining enemies

            if (enemiesDead == enemiesPerWave)
            {
                spawning = false;
                Debug.Log("Wave " + currentWave + " completed.");
                if (currentWave % wavesPerDay == 0)
                {
                    currentDay++;
                    Debug.Log("Starting Day " + currentDay);
                }
                StartCoroutine(StartWaveWithDelay(bufferTime));
            }
        }
    }

    private IEnumerator StartWaveWithDelay(float delay)
    {
        Text textTemp = Instantiate(waveText);
        textTemp.transform.SetParent(GameObject.Find("Background_Canvas").GetComponent<RectTransform>(), false);

        while (remainingEnemiesInWave > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(delay);
        Destroy(textTemp);

        StartWave();

    }

    public void enemiesDeadAdd()
    {
        enemiesDead += 1;
    }
}
