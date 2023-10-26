using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObjectivesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject objectivesPrefab;
    private GameObject objectivesCanvas;

    private GameTimer timer = null;

    // Keep track for objectives
    private int enemiesDead = 0;
    private int numberOfHives = 5;
    public bool[] selectedOptionalObjectives = null;

    // Objective Goals
    private int Level1EnemiesKilledGoal = 70;
    private int Level1HivesProtectedGoal = 3;
    private float Level1HivesProtectedGoalTime = 90.0f;

    private Waves wavesCode = null;

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public class Objective
    {
        private string description;
        private bool completed = false;
        private Func<bool> checkObjectiveIsComplete;
        private Func<string> setDescriptionDynamically;
        private ObjectivesManager objectivesManager;
        private Difficulty difficulty;

        public Objective(ObjectivesManager objectivesManager, Func<string> setDescriptionDynamically, Func<bool> checkObjectiveIsComplete, Difficulty difficulty)
        {
            this.objectivesManager = objectivesManager;
            this.setDescriptionDynamically = setDescriptionDynamically;
            this.checkObjectiveIsComplete = checkObjectiveIsComplete;
            this.difficulty = difficulty;

            description = setDescriptionDynamically();
        }

        public void completeObjective()
        { 
            completed = true;
        }

        public void updateDescription()
        {
            description = setDescriptionDynamically();
        }

        public bool getCompleted() { return completed; }
        public string getDescription() { return description; }
        public Difficulty getDifficulty() { return difficulty; }
        public bool checkCompleted()
        {
            if(checkObjectiveIsComplete()) 
                completeObjective();

            return completed;
        }

    }

    public Objective[] objectives = null;
    void Start()
    {
        selectedOptionalObjectives = new bool[] { false, false };
        timer = gameObject.AddComponent<GameTimer>();
        wavesCode = GetComponent<Waves>();

        if (wavesCode == null)
            throw new Exception("Waves code is null when checking component");

        CreateObjectives(wavesCode.level);
        CreateHandlers(wavesCode.level);
        CreateCanvasText();

    }

    private void Update()
    {
        if (timer != null)
        {
            UpdateObjectivesCompletionStatus();
        }
    }

    private void UpdateObjectivesCompletionStatus()
    {
        bool allObjectivesCompleted = true;

        foreach (Objective objective in objectives)
        {
            objective.updateDescription();
            
            objective.checkCompleted();

            if (!objective.getCompleted())
                allObjectivesCompleted = false;
        }

        if (allObjectivesCompleted)
            CallDayClearedScene();
        else
            UpdateCanvasText();
    }

    private void CreateHandlers(int level)
    {
        if (level == 1)
        {
            CreateEnemiesDeadHandler();
        }
    }

    private void CreateEnemiesDeadHandler()
    {
        wavesCode.OnEnemiesDeadUpdated += HandleEnemiesDeadUpdated; // adds this Action when enemies dead is updated
    }

    private void HandleEnemiesDeadUpdated(int newEnemiesDead)
    {
        enemiesDead = newEnemiesDead;
        UpdateObjectivesCompletionStatus();
    }

    private void CreateCanvasText()
    {
        if (objectivesPrefab == null)
            throw new Exception("Objectives prefab must be assigned");

        if (objectivesCanvas != null)
        {
            Destroy(objectivesCanvas);
        }

        objectivesCanvas = Instantiate(objectivesPrefab);

        Transform objectivesTextTransform = objectivesCanvas.transform.Find("objectivesText");
        TextMeshProUGUI textMeshProObject = objectivesTextTransform.GetComponent<TextMeshProUGUI>();

        textMeshProObject.text = "";
        float yOffset = 0f;

        foreach (Objective objective in objectives)
        {
            TextMeshProUGUI objectiveText = Instantiate(textMeshProObject, textMeshProObject.transform.parent);

            RectTransform objectiveTextPosition = objectiveText.GetComponent<RectTransform>();
            objectiveTextPosition.anchoredPosition += new Vector2(0f, yOffset);

            objectiveText.text = objective.getDescription();

            if (objective.getCompleted())
            {
                objectiveText.fontStyle |= FontStyles.Strikethrough;
            }

            float textHeight = objectiveTextPosition.rect.height;
            yOffset -= textHeight;
        }
    }

    public void UpdateCanvasText()
    {
        CreateCanvasText();
    }

    private void CreateObjectives(int level)
    {
        if (level == 1)
            CreateLevel1Objectives();
        else
            throw new ArgumentException($"No Objectives created for level {level}");
    }
    private void CreateLevel1Objectives()
    {
        Objective[] optionalObjectives = CreateOptionalLevel1Objectives(selectedOptionalObjectives);

        int numberOfBaseObjectives = 2;
        int totalNumberOfObjectives = numberOfBaseObjectives + (optionalObjectives!=null? optionalObjectives.Length : 0);
        objectives = new Objective[totalNumberOfObjectives];

        objectives[0] = new Objective(this, () =>{ return $"Kill {GetEnemiesLeftToKill(Level1EnemiesKilledGoal)} Enemies"; }, 
            () => { return enemiesDead >= Level1EnemiesKilledGoal; }, Difficulty.Easy);

        objectives[1] = new Objective(this, () => { return $"Protect {Level1HivesProtectedGoal} Hives for {GetTimeLeft(Level1HivesProtectedGoalTime)} seconds"; }, 
            () => { return (numberOfHives >= Level1HivesProtectedGoal) && (timer == null || timer.GetTimePassed() >= Level1HivesProtectedGoalTime); }, Difficulty.Medium);

        for (int i = 0; i<totalNumberOfObjectives-numberOfBaseObjectives; i++)
        {
            objectives[numberOfBaseObjectives + i] = optionalObjectives[i];
        }

        StartCoroutine(Level1Timer(Level1HivesProtectedGoalTime, objectives[1])); // Timer for time based objective

    }

    private Objective[] CreateOptionalLevel1Objectives(bool[] selectedObjectives)
    {
        if (selectedObjectives == null || selectedObjectives.Length == 0) return null;

        Objective optionalObjective1 = new Objective(this, () => { return $"Double Enemies Spawned";},
            () => { return true; }, Difficulty.Hard);

        Objective optionalObjective2 = new Objective(this, () => { return $"More exploding enemies spawned"; },
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
    IEnumerator Level1Timer(float timeInSeconds, Objective objective)
    {
        yield return new WaitForSeconds(timeInSeconds);

        objective.checkCompleted();
        Destroy(timer);
            
    }

    private void CallGameOverScene()
    {
        ManageScenes manageScenes = new ManageScenes();
        manageScenes.StartGameOverScene(wavesCode.enemiesDead);
    }

    private void CallDayClearedScene()
    {
        ManageScenes manageScenes = new ManageScenes();
        manageScenes.StartDayClearedScene(wavesCode.level);
    }

    private int GetEnemiesLeftToKill(int goal)
    {
        return enemiesDead >= goal ? goal : goal - enemiesDead;
    }

    private float GetTimeLeft(float goal)
    {
        if (timer == null) return goal;

        float timeLeft =  timer.GetTimePassed() >= goal ? goal : goal - timer.GetTimePassed();

        return (float)Math.Round(timeLeft, 0);
    }

    public void DestroyHive()
    {
        numberOfHives -=1;

        NumberOfHivesPassesLevel(numberOfHives);
        UpdateObjectivesCompletionStatus();
    }

    private void NumberOfHivesPassesLevel(int numberOfHives)
    {
        if (wavesCode.level == 1)
        {
            if (numberOfHives < Level1HivesProtectedGoal)
                CallGameOverScene();
        }
    }

    private int CountHives()
    {
        string hivePrefabName = "Hive";
        int numberOfHives = 0;

        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.name.StartsWith(hivePrefabName))
            {
                numberOfHives++;
            }
        }

        return numberOfHives;
    }

}
