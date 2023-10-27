using System;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Objective = ObjectivesManager.Objective;
using Difficulty = ObjectivesManager.Difficulty;

public class OptionalObjectivesController : MonoBehaviour
{
    public GameObject togglePrefab;
    public GameObject OptionalObjectivesCanvasPrefab;
    private GameObject optionalObjectivesCanvas;

    public bool[] selectedOptionalObjectives;

    private ObjectivesManager objectivesManager;
    private Objective[] optionalObjectives;

    public void Initialize(GameObject OptionalObjectivesCanvasPrefab, GameObject togglePrefab, Objective[] optionalObjectives) {
        this.OptionalObjectivesCanvasPrefab = OptionalObjectivesCanvasPrefab;
        this.togglePrefab = togglePrefab;
        this.optionalObjectives = optionalObjectives;

        InitializeObjects();

        int yPositionShift = 0;
        for (int i = 0; i < optionalObjectives.Length; i++)
        {
            int localIndex = i;

            GameObject toggleGameObject = Instantiate(togglePrefab, optionalObjectivesCanvas.transform);
            Toggle toggle = toggleGameObject.GetComponent<Toggle>();

            toggle.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(localIndex, isOn); });

            SetYPos(toggleGameObject, yPositionShift);
            SetDescriptionAndColor(toggle, optionalObjectives[i]);

            yPositionShift -= 50;
        }

        InitializeButton();

        OpenCanvas();
        PauseGame();
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

    private void InitializeButton()
    {
        Button continueButton = optionalObjectivesCanvas.transform.Find("Container/Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(ReturnOptionalObjectives);
    }

    private void SetYPos(GameObject toggleGameObject, int yPositionShift)
    {
        RectTransform toggleTransform = toggleGameObject.GetComponent<RectTransform>();
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
