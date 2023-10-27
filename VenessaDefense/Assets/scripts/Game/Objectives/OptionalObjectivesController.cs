using System;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Objective = ObjectivesManager.Objective;
using Difficulty = ObjectivesManager.Difficulty;
using Toggle = UnityEngine.UI.Toggle;
using Button = UnityEngine.UI.Button;

public class OptionalObjectivesController : MonoBehaviour
{
    public GameObject objectiveTogglePrefab;
    public GameObject objectiveTextPrefab;
    public GameObject OptionalObjectivesCanvasPrefab;
    private GameObject optionalObjectivesCanvas;

    public bool[] selectedOptionalObjectives;

    private ObjectivesManager objectivesManager;
    private Objective[] optionalObjectives;
    private Objective[] requiredObjectives;

    private int yGapForTasks = 50;

    public void Initialize(GameObject OptionalObjectivesCanvasPrefab, GameObject objectiveTogglePrefab, GameObject objectiveTextPrefab,
        Objective[] optionalObjectives, Objective[] requiredObjectives) 
    {
        this.OptionalObjectivesCanvasPrefab = OptionalObjectivesCanvasPrefab;
        this.objectiveTogglePrefab = objectiveTogglePrefab;
        this.objectiveTextPrefab = objectiveTextPrefab;

        this.optionalObjectives = optionalObjectives;
        this.requiredObjectives = requiredObjectives;

        InitializeObjects();
        InitializeContinueButton();

        CreateOptionalObjectiveToggles();
        CreateRequiredObjectiveText();

        OpenCanvas();
        PauseGame();
    }

    private void CreateOptionalObjectiveToggles()
    {
        int yPositionShift = 0;

        for (int i = 0; i < optionalObjectives.Length; i++)
        {
            int localIndex = i;

            GameObject toggleGameObject = Instantiate(objectiveTogglePrefab, optionalObjectivesCanvas.transform);
            Toggle toggle = toggleGameObject.GetComponent<Toggle>();

            toggle.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(localIndex, isOn); });

            SetYPos(toggleGameObject, yPositionShift);
            SetDescriptionAndColor(toggle, optionalObjectives[i]);

            yPositionShift -= yGapForTasks;
        }
    }
    private void CreateRequiredObjectiveText()
    {
        int yPositionShift = 0;
        for (int i = 0; i < requiredObjectives.Length; i++)
        {
            GameObject requiredObjectiveGameObject = Instantiate(objectiveTextPrefab, optionalObjectivesCanvas.transform);
            Text text = requiredObjectiveGameObject.GetComponent<Text>();

            SetYPos(requiredObjectiveGameObject, yPositionShift);
            SetDescriptionAndColor(text, requiredObjectives[i]);

            yPositionShift -= yGapForTasks;
        }
    }

    private void InitializeObjects()
    {
        objectivesManager = GetComponent<ObjectivesManager>();

        if (objectivesManager == null)
            throw new ArgumentNullException("Objectives Manager is null");

        if (OptionalObjectivesCanvasPrefab == null)
            throw new ArgumentNullException("Canvas Prefab is null");

        selectedOptionalObjectives = new bool[optionalObjectives.Length];

        for (int i=0; i < optionalObjectives.Length; i++)
        {
            selectedOptionalObjectives[i] = false;
        }

        optionalObjectivesCanvas = Instantiate(OptionalObjectivesCanvasPrefab);
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

    private void InitializeContinueButton()
    {
        Button continueButton = optionalObjectivesCanvas.transform.Find("Container/Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(ReturnOptionalObjectives);
    }

    private void SetYPos(GameObject gameObject, int yPositionShift)
    {
        RectTransform toggleTransform = gameObject.GetComponent<RectTransform>();
        toggleTransform.anchoredPosition += new Vector2(0f, yPositionShift);
    }
    public void ResumeGame()
    {
        Destroy(optionalObjectivesCanvas);
        UnpauseGame();
    }

    public void ReturnOptionalObjectives()
    {
        ResumeGame();
        objectivesManager.StartRound(selectedOptionalObjectives);
    }

   
    private void OnToggleValueChanged(int index, bool isOn)
    {
        ToggleObjective(index);
    }

    public void ToggleObjective(int objectiveNumber)
    {
        if (objectiveNumber < 0 || objectiveNumber >= selectedOptionalObjectives.Length)
            throw new ArgumentOutOfRangeException("Index out of range");

        selectedOptionalObjectives[objectiveNumber] = !selectedOptionalObjectives[objectiveNumber];
    }

    private void PauseGame() { Time.timeScale = 0f; }
    private void UnpauseGame() { Time.timeScale = 1f; }
    private void OpenCanvas() { optionalObjectivesCanvas.SetActive(true); }
    private void CloseCanvas() { optionalObjectivesCanvas.SetActive(false); }

}
