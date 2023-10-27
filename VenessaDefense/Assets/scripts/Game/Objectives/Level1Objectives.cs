using System;
using System.Collections;
using UnityEngine;
using Objective = ObjectivesManager.Objective;
using Difficulty = ObjectivesManager.Difficulty;
using System.Linq;

public class Level1Objectives : MonoBehaviour
{
    private int EnemiesKilledGoal = 70;
    private int HivesProtectedGoal = 3;
    private float HivesProtectedGoalTime = 90.0f;

    public bool[] selectedOptionalObjectives = null;
    ObjectivesManager objectivesManager = null;
    private GameTimer timer = null;

    public void Initialize(ObjectivesManager objectivesManager)
    {
        this.objectivesManager = objectivesManager;
        timer = gameObject.AddComponent<GameTimer>();
        selectedOptionalObjectives = new bool[] { true, true };
    }

    private void Update()
    {
        if (timer != null)
            objectivesManager.UpdateObjectivesCompletionStatus();
        
    }

    public int GetEnemiesKilledGoal() {  return EnemiesKilledGoal; }
    public int GetHivesProtectedGoal() { return HivesProtectedGoal; }
    public float GetHivesProtectedGoalTime() { return HivesProtectedGoalTime; }
    public Objective[] CreateObjectives(ObjectivesManager objectivesManager)
    {
        Objective[] optionalObjectives = CreateOptionalObjectives(selectedOptionalObjectives, objectivesManager);
        Objective[] baseObjectives = CreateBaseObjectives(objectivesManager);

        int baseObjectivesLength = baseObjectives.Length;
        int totalNumberOfObjectives = baseObjectives.Length + (optionalObjectives != null ? optionalObjectives.Length : 0);

        Objective[] objectives = new Objective[totalNumberOfObjectives];

        for (int i = 0; i < totalNumberOfObjectives; i++)
        {
            if (i < baseObjectivesLength) 
                objectives[i] = baseObjectives[i];

            else objectives[i] = optionalObjectives[i - baseObjectivesLength];
        }

        StartCoroutine(ObjectiveCompletionTimer(HivesProtectedGoalTime, baseObjectives[1])); // Timer for time based objective

        return objectives;
    }
    private Objective[] CreateBaseObjectives(ObjectivesManager objectivesManager)
    {
        int numberOfBaseObjectives = 2;
        Objective[] baseObjectives = new Objective[numberOfBaseObjectives];

        baseObjectives[0] = new Objective(objectivesManager, () => { return $"Kill {objectivesManager.GetEnemiesLeftToKill(EnemiesKilledGoal)} Enemies"; },
            () => { return objectivesManager.GetEnemiesDead() >= EnemiesKilledGoal; }, Difficulty.Easy);

        baseObjectives[1] = new Objective(objectivesManager, () => { return $"Protect {HivesProtectedGoal} Hives for {objectivesManager.GetTimeLeft(HivesProtectedGoalTime, timer)} seconds"; },
            () => { return (objectivesManager.GetNumberOfHives() >= HivesProtectedGoal) && (timer == null || timer.GetTimePassed() >= HivesProtectedGoalTime); }, Difficulty.Medium);

        return baseObjectives;
    }
    private Objective[] CreateOptionalObjectives(bool[] selectedObjectives, ObjectivesManager objectivesManager)
    {
        if (selectedObjectives == null || selectedObjectives.Length == 0) return null;

        Objective optionalObjective1 = new Objective(objectivesManager, () => { return $"Double Enemies Spawned"; },
            () => { return true; }, Difficulty.Hard);

        Objective optionalObjective2 = new Objective(objectivesManager, () => { return $"More exploding enemies spawned"; },
            () => { return true; }, Difficulty.Medium);

        Objective[] optionalObjectives = new Objective[selectedObjectives.Count(x => x == true)];

        int counter = 0;

        if (selectedObjectives[0] == true)
        {
            optionalObjectives[counter] = optionalObjective1;
            counter++;
        }

        if (selectedObjectives[1] == true)
        {
            optionalObjectives[counter] = optionalObjective2;
            counter++;
        }

        return optionalObjectives;
    }
    IEnumerator ObjectiveCompletionTimer(float timeInSeconds, Objective objective)
    {
        Debug.Log("Timer started");
        yield return new WaitForSeconds(timeInSeconds);

        objective.checkCompleted();
        Destroy(timer);
    }

}
