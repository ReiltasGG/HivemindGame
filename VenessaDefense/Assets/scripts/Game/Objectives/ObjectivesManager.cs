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

    // Objective Goals
    private int Level1EnemiesKilledGoal = 50;
    private int Level1HivesProtectedGoal = 3;
    private float Level1HivesProtectedGoalTime = 120.0f;

    private Waves wavesCode = null;

    public class Objective
    {
        private string description;
        private bool completed = false;
        private Func<bool> checkObjectiveIsComplete;
        private Func<string> setDescriptionDynamically;
        private ObjectivesManager objectivesManager;

        public Objective(ObjectivesManager objectivesManager, Func<string> setDescriptionDynamically, Func<bool> checkObjectiveIsComplete)
        {
            this.objectivesManager = objectivesManager;
            this.setDescriptionDynamically = setDescriptionDynamically;
            this.checkObjectiveIsComplete = checkObjectiveIsComplete;

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
            createEnemiesDeadHandler();
        }
    }

    private void createEnemiesDeadHandler()
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
        objectives = new Objective[2];

        objectives[0] = new Objective(this, () =>{ return $"Kill {getEnemiesLeftToKill(Level1EnemiesKilledGoal)} Enemies"; }, 
            () => { return enemiesDead >= Level1EnemiesKilledGoal; });

        objectives[1] = new Objective(this, () => { return $"Protect {Level1HivesProtectedGoal} Hives for {getTimeLeft(Level1HivesProtectedGoalTime)} seconds"; }, 
            () => { return (numberOfHives >= Level1HivesProtectedGoal) && (timer == null || timer.GetTimePassed() >= Level1HivesProtectedGoalTime); });

        StartCoroutine(Level1Timer(Level1HivesProtectedGoalTime, objectives[1]));
    }
    IEnumerator Level1Timer(float timeInSeconds, Objective objective)
    {
        yield return new WaitForSeconds(timeInSeconds);
        if (countHives() < Level1HivesProtectedGoal)
        {
            CallGameOverScene();
        }
        else
        {
            objective.checkCompleted();
            Destroy(timer);
        }
            
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

    private int getEnemiesLeftToKill(int goal)
    {
        return enemiesDead >= goal ? goal : goal - enemiesDead;
    }

    private float getTimeLeft(float goal)
    {
        if (timer == null) return goal;

        float timeLeft =  timer.GetTimePassed() >= goal ? goal : goal - timer.GetTimePassed();

        return (float)Math.Round(timeLeft, 0);
    }

    public void updateNumberOfHives()
    {
        numberOfHives = countHives();
        UpdateObjectivesCompletionStatus();
    }

    private int countHives()
    {
        GameObject[] hiveObjects = GameObject.FindGameObjectsWithTag("Hive");
        return hiveObjects.Length;
    }

}
