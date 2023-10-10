using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public int numberOfWaves = 20;
    public int wavesPerDay = 20;
    public int enemiesPerWave = 5;
    public float spawnInterval = 1.0f;
    public float bufferTime = 15.0f;

    private int currentWave = 0;
    private int currentDay = 1;
    private bool spawning = false;
    private float timeSinceLastSpawn = 0f;
    private float timeSinceWaveCompleted = 0f;
    public bool waveCompleted = false;
    private List<Transform> spawnerTransforms = new List<Transform>();
    [SerializeField] private GameObject[] enemyPrefabs;
    public Text waveText;
    //private WaveTimer waveTimer;
    public GameObject uiCanvas;

    private AudioSource roundStartSoundSource;
    public AudioClip roundStartSound;

    private int remainingEnemiesInWave;

    private int dontAddMoreThanOne = 1;
    public int enemiesDead = 0;
    private int happenOnceAgain;

    public class EnemyWave
    {
        public int waveNumber;
        public int ants;
        public int beetles;
        public int spider;
        public int explodingAnt;
        public int explodingBeetle;
        public int totalEnemies;
    }

    public List<EnemyWave> waves = new List<EnemyWave>
    {
        new EnemyWave { waveNumber = 1, ants = 5, beetles = 0, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 5 + 0 + 0 + 0 + 0 },
        new EnemyWave { waveNumber = 2, ants = 8, beetles = 2, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 8 + 2 + 0 + 0 + 0 },
        new EnemyWave { waveNumber = 3, ants = 10, beetles = 4, explodingAnt = 0, explodingBeetle = 0, spider = 0, totalEnemies = 10 + 4 + 0 + 0 + 0 },
        new EnemyWave { waveNumber = 4, ants = 6, beetles = 3, explodingAnt = 4, explodingBeetle = 1, spider = 1, totalEnemies = 6 + 3 + 4 + 1 + 1 },
        new EnemyWave { waveNumber = 5, ants = 10, beetles = 5, explodingAnt = 4, explodingBeetle = 1, spider = 0, totalEnemies = 10 + 5 + 4 + 1 + 0 },
        new EnemyWave { waveNumber = 6, ants = 12, beetles = 5, explodingAnt = 3, explodingBeetle = 3, spider = 0, totalEnemies = 12 + 5 + 3 + 3 + 0 },
        new EnemyWave { waveNumber = 7, ants = 12, beetles = 3, explodingAnt = 5, explodingBeetle = 5, spider = 1, totalEnemies = 12 + 3 + 5 + 5 + 1 },
        new EnemyWave { waveNumber = 8, ants = 14, beetles = 4, explodingAnt = 6, explodingBeetle = 6, spider = 2, totalEnemies = 14 + 4 + 6 + 6 + 2 },
        new EnemyWave { waveNumber = 9, ants = 10, beetles = 5, explodingAnt = 7, explodingBeetle = 3, spider = 3, totalEnemies = 10 + 5 + 7 + 3 + 3 },
        new EnemyWave { waveNumber = 10, ants = 15, beetles = 15, explodingAnt = 10, explodingBeetle = 5, spider = 5, totalEnemies = 15 + 15 + 10 + 5 + 5 },
    };

    void Start()
    {

        roundStartSoundSource = GetComponent<AudioSource>(); ;
        roundStartSoundSource.clip = roundStartSound;

        // Find all GameObjects with the "Spawner" tag and add their transforms to the list
        GameObject[] spawnerObjects = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawner in spawnerObjects)
        {
            spawnerTransforms.Add(spawner.transform);
        }
    
/*        GameObject timerObject = GameObject.FindWithTag("Wave Timer");
        if (timerObject != null)
        {
            waveTimer = timerObject.GetComponent<WaveTimer>();
        }
        else
        {
            Debug.LogError("No GameObject with the 'Timer' tag found.");
        }

        if (waveTimer != null)
        {
            waveTimer.StartTimer();
        }*/

        StartWave();

    }

    void Update()
    {

        if (spawning && !waveCompleted)
        {
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
                    waves[currentWaveTemp].spider = waves[currentWaveTemp].spider - 1;
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
                //StartWave();
                waveCompleted = false;
            }
        }

        if (enemiesDead == enemiesPerWave)
        {
            dontAddMoreThanOne = 1;

            /*
            if (currentWave % wavesPerDay == 0)
            {
                currentDay++;
                Debug.Log("Starting Day " + currentDay);
            }
            */
            if (happenOnceAgain == 1)
            {
                StartCoroutine(StartWaveWithDelay(bufferTime));
                happenOnceAgain = 0;
            }
        }

    }

    void playRoundStartSound()
    {
        if (roundStartSoundSource == null)
            throw new ArgumentNullException("Round start source not instantiated");

        if (roundStartSoundSource.clip == null)
            throw new ArgumentNullException("Round start sound not attached to AudioSource");

        roundStartSoundSource.Play();
    }

    void StartWave()
    {
        happenOnceAgain = 1;
        enemiesDead = 0;

        if (dontAddMoreThanOne == 1)
        {
            currentWave++;
            dontAddMoreThanOne = 0;
        }

        int currentWaveTemp = currentWave - 1;
        enemiesPerWave = waves[currentWaveTemp].totalEnemies;

        playRoundStartSound();
        SpawnWave();
    }

    void SpawnWave()
    {
        waveCompleted = false;
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

                int randomSpawnerIndex = UnityEngine.Random.Range(0, spawnerTransforms.Count);
                Vector3 spawnPosition = spawnerTransforms[randomSpawnerIndex].position;
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("No spawners found.");
            }

            remainingEnemiesInWave--; // Decrease remaining enemies

        }
    }

    private IEnumerator StartWaveWithDelay(float delay)
    {
        spawning = false;
        waveCompleted = true;
        Text textTemp = Instantiate(waveText);
        textTemp.transform.SetParent(uiCanvas.GetComponent<RectTransform>(), false);

        yield return new WaitForSeconds(delay);
        Destroy(textTemp);

        StartWave();

    }

    public void enemiesDeadAdd()
    {
        enemiesDead += 1;
    }
}
