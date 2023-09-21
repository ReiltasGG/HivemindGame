using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRate = 1f;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField]
    private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawner());
    }
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemytoSpawn = enemyPrefabs[rand];

            Instantiate(enemytoSpawn, transform.position, Quaternion.identity);
        }
    }
}
