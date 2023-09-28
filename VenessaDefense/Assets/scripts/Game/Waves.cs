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
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
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
    }

    void StartWave()
    {
        if (currentWave < numberOfWaves)
        {
            currentWave++;
            enemiesPerWave = 5 + currentWave * 5;
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

    void SpawnEnemy()
    {
        if (remainingEnemiesInWave > 0)
        {
            if (spawnerTransforms.Count > 0)
            {
                int rand = Random.Range(0, enemyPrefabs.Length);
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

            if (remainingEnemiesInWave == 0)
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
        textTemp.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>(), false);

        while (remainingEnemiesInWave > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(delay);
            Destroy(textTemp);
        
        StartWave();

    }
}
