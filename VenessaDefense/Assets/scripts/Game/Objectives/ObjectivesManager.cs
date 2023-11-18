using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObjectivesManager : MonoBehaviour
{

    public GameObject objectiveTogglePrefab;
    public GameObject objectiveTextPrefab;
    public GameObject ChallengeObjectivesCanvasPrefab;

    [SerializeField]
    private GameObject objectivesPrefab;
    private GameObject objectivesCanvas;

    // Keep track for objectives
    private int enemiesDead = 0;
    private int numberOfHives = 5;
    private int towersPlaced = 0;


    private Waves wavesCode = null;
    private TowerManager towerManageCode = null;
    private Level1Objectives level1Objectives = null;
    private Level2Objectives level2Objectives = null;
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
            if (completed) return;

            completed = true;
            objectivesManager.GainSkillPoints((int)difficulty);
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
        wavesCode = GetComponent<Waves>();

        if (wavesCode == null)
            throw new Exception("Waves code is null when checking component");

        towerManageCode = GetComponent<TowerManager>();

        if (towerManageCode == null)
            throw new Exception("Tower Manager code is null when checking component");

        DisplayObjectivesCanvas();
    }

    public void StartRound(bool[] selectedChallengeObjectives)
    {
        CreateObjectives(wavesCode.level, selectedChallengeObjectives);
        CreateHandlers();
        CreateCanvasText();
        UpdateObjectivesCompletionStatus();
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
    private void CreateObjectives(int level, bool[] selectedChallengeObjectives)
    {
        if (level == 1)
        {
            level1Objectives = gameObject.AddComponent<Level1Objectives>();
            level1Objectives.Initialize(this, selectedChallengeObjectives);

            objectives = level1Objectives.CreateObjectives();
        }

        else if (level == 2)
        {
            level2Objectives = gameObject.AddComponent<Level2Objectives>();
            level2Objectives.Initialize(this, selectedChallengeObjectives);

            objectives = level2Objectives.CreateObjectives();
        }
        else
            throw new ArgumentException($"No Objectives created for level {level}");

    }
    public Objective[] GetPossibleChallengeObjectives(int level)
    {
        if (level == 1)
        {
            level1Objectives = gameObject.AddComponent<Level1Objectives>();
            level1Objectives.Initialize(this);

            return level1Objectives.GetChallengeObjectives();
        }

        else if (level == 2)
        {
            level2Objectives = gameObject.AddComponent<Level2Objectives>();
            level2Objectives.Initialize(this);

            return level2Objectives.GetChallengeObjectives();
        }
        else throw new ArgumentException($"No Objectives created for level {level}");
    }
    public Objective[] GetRequiredObjectives(int level)
    {
        if (level == 1)
        {
            level1Objectives = gameObject.AddComponent<Level1Objectives>();
            level1Objectives.Initialize(this);

            return level1Objectives.GetBaseObjectives();
        }

        if (level == 2)
        {
            level2Objectives = gameObject.AddComponent<Level2Objectives>();
            level2Objectives.Initialize(this);

            return level2Objectives.GetBaseObjectives();
        }
        else throw new ArgumentException($"No Objectives created for level {level}");
    }
    private void CreateHandlers()
    {
        CreateEnemiesDeadHandler();
        CreateTowersPlacedHandler();
    }
    
    private void CreateTowersPlacedHandler()
    {
        towerManageCode.towersPlacedUpdated += HandleTowersPlacedUpdated;
    }

    private void HandleTowersPlacedUpdated(int newTowerPlaced)
    {
        towersPlaced++;
        UpdateObjectivesCompletionStatus();
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
    private void UpdateCanvasText()
    {
        CreateCanvasText();
    }
    private void DisplayObjectivesCanvas()
    {
        Objective[] challengeObjectives = GetPossibleChallengeObjectives(wavesCode.level);
        Objective[] requiredObjectives = GetRequiredObjectives(wavesCode.level);

        ChallengeObjectivesController challengeObjectivesController = gameObject.AddComponent<ChallengeObjectivesController>();
        challengeObjectivesController.Initialize(ChallengeObjectivesCanvasPrefab, objectiveTogglePrefab, objectiveTextPrefab, challengeObjectives, requiredObjectives);
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

    public void DestroyHive()
    {
        numberOfHives -=1;

        NumberOfHivesPassesLevel(numberOfHives);
        UpdateObjectivesCompletionStatus();
    }

    private void NumberOfHivesPassesLevel(int numberOfHives)
    {
        if (wavesCode.level == 2)
        {
            Level2Objectives level2Objectives = new Level2Objectives();

            if (numberOfHives < level2Objectives.GetHivesProtectedGoal())
                CallGameOverScene();

        }
    }
    
    public void GainSkillPoints(int numberOfSkillPoints)
    {
        SkillPoints skillPoints = FindGamesManager().GetComponent<SkillPoints>();
        if (skillPoints == null)
            throw new ArgumentNullException("Skill Points script must be attached to Games Manager");

        skillPoints.GainSkillPoints(numberOfSkillPoints);
    }

    private GameObject FindGamesManager()
    {
        GameObject GamesManager = GameObject.FindWithTag("GamesManager");
        if (GamesManager == null)
            throw new ArgumentNullException("Games manager is not found");
            
       return GamesManager;
    }


    public int GetNumberOfHives() { return numberOfHives; }
    public int GetTowersPlaced() {return towersPlaced; }
    public int GetTowersLeftToPlace(int goal) {return towersPlaced >= goal ? goal : goal - towersPlaced; }
    public int GetEnemiesDead() { return enemiesDead; }
    public int GetEnemiesLeftToKill(int goal) { return enemiesDead >= goal ? goal : goal - enemiesDead; }
    public float GetTimeLeft(float goal, GameTimer timer)
    {
        if (timer == null) return goal;

        float timeLeft = timer.GetTimePassed() >= goal ? goal : goal - timer.GetTimePassed();

        return (float)Math.Round(timeLeft, 0);
    }

}
