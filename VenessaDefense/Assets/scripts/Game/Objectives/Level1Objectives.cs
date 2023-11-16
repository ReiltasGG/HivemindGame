using System;
using System.Collections;
using UnityEngine;
using Objective = ObjectivesManager.Objective;
using Difficulty = ObjectivesManager.Difficulty;
using System.Linq;

public class Level1Objectives : MonoBehaviour
{
    private int EnemiesKilledGoal = 60;
    private int TowersPlacedGoal = 5;

//    private int HivesProtectedGoal = 3;
//    private float HivesProtectedGoalTime = 90.0f;

    public bool[] selectedChallengeObjectives = null;
    ObjectivesManager objectivesManager = null;

    private GameTimer timer = null;

    private const float MUTATE_MODIFIER = 0.3f;
    private const float SPAWN_RATE_MODIFIER = 2.0f;

    public void Initialize(ObjectivesManager objectivesManager, bool[] selectedChallengeObjectives)
    {
        this.objectivesManager = objectivesManager;
        this.selectedChallengeObjectives = selectedChallengeObjectives;

        timer = gameObject.AddComponent<GameTimer>();
    }

    public void Initialize(ObjectivesManager objectivesManager)
    {
        // This is a shell constructor. It's purpose is to use the basic get/set functions without needing rest of params.
        // Please use other one if you need to fully run this.
        this.objectivesManager = objectivesManager;

        selectedChallengeObjectives = null;
    }

    private void Update()
    {
        if (timer != null)
            objectivesManager.UpdateObjectivesCompletionStatus();
    }

    public Objective[] CreateObjectives()
    {
        Objective[] challengeObjectives = CreateChallengeObjectives();
        Objective[] baseObjectives = CreateBaseObjectives();

        int baseObjectivesLength = baseObjectives.Length;
        int totalNumberOfObjectives = baseObjectives.Length + (challengeObjectives != null ? challengeObjectives.Length : 0);

        Objective[] objectives = new Objective[totalNumberOfObjectives];

        for (int i = 0; i < totalNumberOfObjectives; i++)
        {
            if (i < baseObjectivesLength) 
                objectives[i] = baseObjectives[i];

            else objectives[i] = challengeObjectives[i - baseObjectivesLength];
        }

    //    StartCoroutine(ObjectiveCompletionTimer(HivesProtectedGoalTime, baseObjectives[1])); // Timer for time based objective

        return objectives;
    }
    private Objective[] CreateBaseObjectives()
    {
        int numberOfBaseObjectives = 2;
        Objective[] baseObjectives = new Objective[numberOfBaseObjectives];

        baseObjectives[0] = new Objective(objectivesManager, () => { return $"Kill {objectivesManager.GetEnemiesLeftToKill(EnemiesKilledGoal)} Enemies"; },
            () => { return objectivesManager.GetEnemiesDead() >= EnemiesKilledGoal; }, Difficulty.Easy);

        baseObjectives[1] = new Objective(objectivesManager, () => { return $"Place {objectivesManager.GetTowersLeftToPlace(TowersPlacedGoal)} Towers"; },
            () => { return objectivesManager.GetTowersPlaced() >= TowersPlacedGoal; }, Difficulty.Easy);

        

    //    baseObjectives[1] = new Objective(objectivesManager, () => { return $"Protect {HivesProtectedGoal} Hives for {objectivesManager.GetTimeLeft(HivesProtectedGoalTime, timer)} seconds"; },
    //        () => { return (objectivesManager.GetNumberOfHives() >= HivesProtectedGoal) && (timer == null || timer.GetTimePassed() >= HivesProtectedGoalTime); }, Difficulty.Medium);

        return baseObjectives;
    }
    private Objective[] CreateChallengeObjectives()
    {
        Objective challengeObjective1 = new Objective(objectivesManager, () => { return $"Double Enemies Spawned"; },
            () => { return true; }, Difficulty.Medium);

        Objective challengeObjective2 = new Objective(objectivesManager, () => { return $"Survive with 1 HP"; },
            () => { return true; }, Difficulty.Hard);

        // Return all objectives if nothing is passed in
        if (selectedChallengeObjectives == null || selectedChallengeObjectives.Length == 0) 
            return new Objective[] { challengeObjective1, challengeObjective2};

        Objective[] challengeObjectives = new Objective[selectedChallengeObjectives.Count(x => x == true)];

        int counter = 0;

        if (selectedChallengeObjectives[0] == true)
        {
            Waves wavesCode = GetComponent<Waves>();
            wavesCode.SetSpawnRateModifier(SPAWN_RATE_MODIFIER);

            challengeObjectives[counter] = challengeObjective1;
            counter++;
        }
        
        if (selectedChallengeObjectives[1] == true)
        {
            GameObject Player = GameObject.FindWithTag("Player");
            AttributesManager playerHP = Player.GetComponent<AttributesManager>();
            if(playerHP != null)
            {
                Debug.Log("change player hp");
                playerHP.takeDamage(99);
            }
            challengeObjectives[counter] = challengeObjective2;
            counter++;
            
        }

        return challengeObjectives;
    }
    

    IEnumerator ObjectiveCompletionTimer(float timeInSeconds, Objective objective)
    {
        yield return new WaitForSeconds(timeInSeconds);

        objective.checkCompleted();
        Destroy(timer);
    }

    public int GetEnemiesKilledGoal() { return EnemiesKilledGoal; }
    public int GetTowersPlaced() { return TowersPlacedGoal; }
    public int GetBaseObjectivesCount() { return CreateBaseObjectives().Length; }
    public int GetChallengeObjectivesCount() { return CreateChallengeObjectives().Length; }
    public Objective[] GetChallengeObjectives() { return CreateChallengeObjectives(); }
    public Objective[] GetBaseObjectives() { return CreateBaseObjectives(); }
}
