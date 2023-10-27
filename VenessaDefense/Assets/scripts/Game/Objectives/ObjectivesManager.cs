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

    // Keep track for objectives
    private int enemiesDead = 0;
    private int numberOfHives = 5;

    private Waves wavesCode = null;
    private Level1Objectives level1Objectives = null;
    private Objective[] objectives = null;

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

        public Objective(ObjectivesManager objectivesManager, string description, Func<bool> checkObjectiveIsComplete, Difficulty difficulty)
        {
            this.objectivesManager = objectivesManager;
            this.setDescriptionDynamically = () => { return description; };
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

    void Start()
    {
        level1Objectives = gameObject.AddComponent<Level1Objectives>();
        level1Objectives.Initialize(this);
        wavesCode = GetComponent<Waves>();

        if (wavesCode == null)
            throw new Exception("Waves code is null when checking component");

        CreateObjectives(wavesCode.level);
        CreateHandlers(wavesCode.level);
        CreateCanvasText();

    }

    public void UpdateObjectivesCompletionStatus()
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
        enemiesDead++;
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
            objectives = level1Objectives.CreateObjectives(this);
        else
            throw new ArgumentException($"No Objectives created for level {level}");

        UpdateObjectivesCompletionStatus();
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

    public int GetEnemiesLeftToKill(int goal)
    {
        return enemiesDead >= goal ? goal : goal - enemiesDead;
    }

    public float GetTimeLeft(float goal, GameTimer timer)
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
            Level1Objectives level1Objectives = new Level1Objectives();

            if (numberOfHives < level1Objectives.GetHivesProtectedGoal())
                CallGameOverScene();

            Destroy(level1Objectives);
        }
    }

    public int GetNumberOfHives() { return numberOfHives; }
    public int GetEnemiesDead() { return enemiesDead; }
}
