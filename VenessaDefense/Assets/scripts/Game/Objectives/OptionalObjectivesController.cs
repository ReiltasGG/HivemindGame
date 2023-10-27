using System;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionalObjectivesController : MonoBehaviour
{
    public GameObject togglePrefab;
    public GameObject OptionalObjectivesCanvasPrefab;
    private GameObject optionalObjectivesCanvas;

    public bool[] selectedOptionalObjectives = null;
    public int numberOfOptionalObjectives;

    private ObjectivesManager objectivesManager = null;

    public void Initialize() {
        InitializeObjects();

        /*for (int i = 0; i < numberOfOptionalObjectives; i++)
        {
            Debug.Log("looped");
            GameObject toggleGameObject = Instantiate(togglePrefab, transform);
            Toggle toggle = toggleGameObject.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(i, isOn); });
        }*/


        int yPixelLower = 0;
        for (int i = 0; i < numberOfOptionalObjectives; i++)
        {
            int localIndex = i;
            GameObject toggleGameObject = Instantiate(togglePrefab, optionalObjectivesCanvas.transform);
            Toggle toggle = toggleGameObject.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(localIndex, isOn); });

            yPixelLower -= 50;
        }


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

        numberOfOptionalObjectives = objectivesManager.GetOptionalObjectivesCount();
        selectedOptionalObjectives = new bool[numberOfOptionalObjectives];

        for (int i=0; i < numberOfOptionalObjectives; i++)
        {
            selectedOptionalObjectives[i] = false;
        }

        optionalObjectivesCanvas = Instantiate(OptionalObjectivesCanvasPrefab);

    }

    public void ResumeGame()
    {
        Destroy(optionalObjectivesCanvas);
        UnpauseGame();
    }

    public void ReturnOptionalObjectives()
    {
        objectivesManager.StartRound(selectedOptionalObjectives);
    }

   
    private void OnToggleValueChanged(int index, bool isOn)
    {
        ToggleObjective(index);
        Debug.Log($"Toggle {index} to {isOn}");
    }

    public void ToggleObjective(int objectiveNumber)
    {
        if (objectiveNumber < 0 || objectiveNumber >= selectedOptionalObjectives.Length)
            throw new ArgumentOutOfRangeException("Index out of range");

        Debug.Log($"Trying to toggle {objectiveNumber}");


        selectedOptionalObjectives[objectiveNumber] = !selectedOptionalObjectives[objectiveNumber];

        Debug.Log($"Toggled {objectiveNumber} to {selectedOptionalObjectives[objectiveNumber]}");
    }

    private void PauseGame() { Time.timeScale = 0f; }
    private void UnpauseGame() { Time.timeScale = 1f; }
    private void OpenCanvas() { optionalObjectivesCanvas.SetActive(true); }
    private void CloseCanvas() { optionalObjectivesCanvas.SetActive(false); }

}
