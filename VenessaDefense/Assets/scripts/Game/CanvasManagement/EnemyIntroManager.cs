using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class EnemyIntroManager : MonoBehaviour
{
    [Serializable]
    public class EnemyIntroductionImage
    {
        public Waves.Enemies enemy;
        public Sprite sprite;
    }

    private Dictionary<int, List<Waves.Enemies>> newEnemiesInWave = null;
    public List<EnemyIntroductionImage> enemyIntroductionImages;

    private float introDisplayTime = 5f;

    private GameObject canvas;
    public GameObject EnemyIntroPrefab;
    private GameObject enemyIntroOnScreen = null;

    void Start()
    {
        enemyIntroOnScreen = Instantiate(EnemyIntroPrefab);
        enemyIntroOnScreen.SetActive(false);

        Waves wavesScript = GetComponent<Waves>();

        if (wavesScript == null )
            throw new ArgumentNullException("Wave and EnemyIntroManager must both be attached to GameManager");

        List<Waves.EnemyWave> enemyWaves = wavesScript.level1Waves;

        newEnemiesInWave = new Dictionary<int, List<Waves.Enemies>>();

        checkWhenEnemyFirstAppears(enemyWaves);


        if (wavesScript.uiCanvas == null)
        {
            canvas = GameObject.Find("Canvas");
            throw new ArgumentNullException("Wave Script not assigned a UI Canvas");
        }
        else    
            canvas = wavesScript.uiCanvas;

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
        if (newEnemiesInWave == null)
            throw new ArgumentNullException("List to find new enemies in wave is null");

        if (!newEnemiesInWave.ContainsKey(waveNumber))
            yield break;

        List<Waves.Enemies> enemiesToShow = newEnemiesInWave[waveNumber];

        
        foreach (Waves.Enemies enemy in enemiesToShow)
        {
            Debug.Log($"Displaying intro for enemy: {enemy}");

            DisplayIntro(enemy);
            yield return new WaitForSeconds(introDisplayTime);
        }

        if (enemyIntroOnScreen != null)
            enemyIntroOnScreen.SetActive(false);
    }

    private void DisplayIntro(Waves.Enemies enemy)
    {
        if (canvas == null)
            throw new ArgumentNullException("Enemy Intro Manager not assigned a canvas");

        if (enemyIntroOnScreen == null)
        {
            enemyIntroOnScreen = Instantiate(EnemyIntroPrefab);
        }

        Image enemyIntroImage = enemyIntroOnScreen.GetComponent<Image>();
        enemyIntroImage.sprite = GetSpriteForEnemy(enemy);

        enemyIntroOnScreen.transform.SetParent(canvas.transform, false);
        enemyIntroOnScreen.SetActive(true);

    }

    private Sprite GetSpriteForEnemy(Waves.Enemies enemy)
    {
        if (enemyIntroductionImages == null)
            throw new ArgumentNullException("Enemy introduction images is null");

        foreach (EnemyIntroductionImage introImage in enemyIntroductionImages)
        {
            if (introImage.enemy == enemy)
            {
                return introImage.sprite;
            }
        }

        throw new ArgumentNullException("Enemy introduction sprite not added to list");
    }

}
