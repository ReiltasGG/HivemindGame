using System;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Objective = ObjectivesManager.Objective;
using Difficulty = ObjectivesManager.Difficulty;
using Toggle = UnityEngine.UI.Toggle;
using Button = UnityEngine.UI.Button;

public class ChallengeObjectivesController : MonoBehaviour
{
    public GameObject objectiveTogglePrefab;
    public GameObject objectiveTextPrefab;
    public GameObject ChallengeObjectivesCanvasPrefab;
    private GameObject challengeObjectivesCanvas;

    public bool[] selectedChallengeObjectives;

    private ObjectivesManager objectivesManager;
    private Objective[] challengeObjectives;
    private Objective[] requiredObjectives;

    private int yGapForTasks = 50;

    public void Initialize(GameObject ChallengeObjectivesCanvasPrefab, GameObject objectiveTogglePrefab, GameObject objectiveTextPrefab,
        Objective[] challengeObjectives, Objective[] requiredObjectives) 
    {
        this.ChallengeObjectivesCanvasPrefab = ChallengeObjectivesCanvasPrefab;
        this.objectiveTogglePrefab = objectiveTogglePrefab;
        this.objectiveTextPrefab = objectiveTextPrefab;

        this.challengeObjectives = challengeObjectives;
        this.requiredObjectives = requiredObjectives;

        InitializeObjects();
        InitializeContinueButton();

        CreateChallengeObjectiveToggles();
        CreateRequiredObjectiveText();

        OpenCanvas();
        PauseGame();
    }
    private void InitializeObjects()
    {
        objectivesManager = GetComponent<ObjectivesManager>();

        if (objectivesManager == null)
            throw new ArgumentNullException("Objectives Manager is null");

        if (ChallengeObjectivesCanvasPrefab == null)
            throw new ArgumentNullException("Canvas Prefab is null");

        selectedChallengeObjectives = new bool[challengeObjectives.Length];

        for (int i = 0; i < challengeObjectives.Length; i++)
        {
            selectedChallengeObjectives[i] = false;
        }

        challengeObjectivesCanvas = Instantiate(ChallengeObjectivesCanvasPrefab);
    }
    private void InitializeContinueButton()
    {
        Button continueButton = challengeObjectivesCanvas.transform.Find("Container/Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(ReturnChallengeObjectives);
    }

    private void CreateChallengeObjectiveToggles()
    {
        int yPositionShift = 0;

        for (int i = 0; i < challengeObjectives.Length; i++)
        {
            int localIndex = i;

            GameObject toggleGameObject = Instantiate(objectiveTogglePrefab, challengeObjectivesCanvas.transform);
            Toggle toggle = toggleGameObject.GetComponent<Toggle>();

            toggle.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(localIndex, isOn); });

            SetYPos(toggleGameObject, yPositionShift);
            SetDescriptionAndColor(toggle, challengeObjectives[i]);

            yPositionShift -= yGapForTasks;
        }
    }
    private void CreateRequiredObjectiveText()
    {
        int yPositionShift = 0;
        for (int i = 0; i < requiredObjectives.Length; i++)
        {
            GameObject requiredObjectiveGameObject = Instantiate(objectiveTextPrefab, challengeObjectivesCanvas.transform);
            Text text = requiredObjectiveGameObject.GetComponent<Text>();

            SetYPos(requiredObjectiveGameObject, yPositionShift);
            SetDescriptionAndColor(text, requiredObjectives[i]);

            yPositionShift -= yGapForTasks;
        }
    }

    private void SetDescriptionAndColor(Toggle toggle, Objective objective)
    {
        UnityEngine.Color textColor = GetTextColor(objective);

        Text descriptionText = toggle.transform.Find("Description").GetComponent<Text>();

        descriptionText.text = objective.getDescription();
        descriptionText.color = textColor;
    }
    private void SetDescriptionAndColor(Text text, Objective objective)
    {
        UnityEngine.Color textColor = GetTextColor(objective);

        text.text = objective.getDescription();
        text.color = textColor;
    }

    private UnityEngine.Color GetTextColor(Objective objective)
    {
        switch (objective.getDifficulty())
        {
            case Difficulty.Easy:
                return UnityEngine.Color.green;
            case Difficulty.Medium:
                return UnityEngine.Color.yellow;
            case Difficulty.Hard:
                return UnityEngine.Color.red;
        }

        return UnityEngine.Color.white;
    }
    private void SetYPos(GameObject gameObject, int yPositionShift)
    {
        RectTransform toggleTransform = gameObject.GetComponent<RectTransform>();
        toggleTransform.anchoredPosition += new Vector2(0f, yPositionShift);
    }

    private void OnToggleValueChanged(int index, bool isOn)
    {
        ToggleObjective(index);
    }
    public void ToggleObjective(int objectiveNumber)
    {
        if (objectiveNumber < 0 || objectiveNumber >= selectedChallengeObjectives.Length)
            throw new ArgumentOutOfRangeException("Index out of range");

        selectedChallengeObjectives[objectiveNumber] = !selectedChallengeObjectives[objectiveNumber];
    }

    public void ResumeGame()
    {
        Destroy(challengeObjectivesCanvas);
        UnpauseGame();
    }
    public void ReturnChallengeObjectives()
    {
        ResumeGame();
        objectivesManager.StartRound(selectedChallengeObjectives);
    }

    private void PauseGame() { Time.timeScale = 0f; }
    private void UnpauseGame() { Time.timeScale = 1f; }
    private void OpenCanvas() { challengeObjectivesCanvas.SetActive(true); }
    private void CloseCanvas() { challengeObjectivesCanvas.SetActive(false); }

}
