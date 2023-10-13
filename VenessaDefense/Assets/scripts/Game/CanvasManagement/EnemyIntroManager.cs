using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyIntroManager : MonoBehaviour
{
    [Serializable]
    public class EnemyIntroductionImage
    {
        public Waves.Enemies enemy;
        public UnityEngine.UI.Image image;
    }

    private Dictionary<int, List<Waves.Enemies>> newEnemiesInWave;
    public List<EnemyIntroductionImage> enemyIntroductionImages;
    private float introDisplayTime = 5f;

    void Start()
    {
        Waves wavesScript = GetComponent<Waves>();

        if (wavesScript == null )
            throw new ArgumentNullException("Wave and EnemyIntroManager must both be attached to GameManager");

        List<Waves.EnemyWave> enemyWaves = wavesScript.level1Waves;

        newEnemiesInWave = new Dictionary<int, List<Waves.Enemies>>();

        checkWhenEnemyFirstAppears(enemyWaves);

    }

    private void checkWhenEnemyFirstAppears(List<Waves.EnemyWave> enemyWaves)
    {
        if (enemyWaves == null)
            throw new ArgumentNullException("Enemy Waves is null");

        if (enemyWaves.Count == 0)
            throw new ArgumentException("Enemy Waves empty");
        
        foreach ( Waves.Enemies enemy in Enum.GetValues(typeof(Waves.Enemies)) )
        {
            foreach (Waves.EnemyWave wave in enemyWaves)
            {
                if (wave.enemyCounts.ContainsKey(enemy))
                {
                    AddEnemyToDictionary(wave.waveNumber, enemy);
                    break;
                }
            }
        }

    }

    public void AddEnemyToDictionary(int waveNumber, Waves.Enemies enemy)
    {
        if (!newEnemiesInWave.ContainsKey(waveNumber))
            newEnemiesInWave[waveNumber] = new List<Waves.Enemies>();

        newEnemiesInWave[waveNumber].Add(enemy);
    }

    public IEnumerator DisplayNewIntros(int waveNumber)
    {
        if (!newEnemiesInWave.ContainsKey(waveNumber))
            yield break;

        List<Waves.Enemies> enemiesToShow = newEnemiesInWave[waveNumber];

        foreach (Waves.Enemies enemy in enemiesToShow)
        {
            DisplayIntro(enemy);
            yield return new WaitForSeconds(introDisplayTime);
        }
    }

    private void DisplayIntro(Waves.Enemies enemy)
    {
        Debug.Log($"Displaying intro for enemy: {enemy}");

        // TODO: Display on screen
    }

}
