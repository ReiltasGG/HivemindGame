using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_SkillTreeOpener : MonoBehaviour
{
    public GameObject skillTreeFinder;
  //  public GameObject skillTreeButton = null;
    public int countClicks = 0;
    public GameObject skillTreeBackground;
    [SerializeField] private GameObject SkillTreeText;
    public bool skillTreeIsOpen = false;
    public KeyCode openKey;

    public void Update()
    {
        openKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SkillTreeOpen", "T"));
        if(Input.GetKeyDown(openKey))
        {
            ToggleTree();
        }
    }
    public void ToggleTree()
    {
        if (!skillTreeIsOpen)
        {
            UpdateSkillPointText();

            skillTreeFinder.SetActive(true);
            skillTreeBackground.SetActive(true);

            Pause();
        }
        else
        {
            skillTreeFinder.SetActive(false);
            skillTreeBackground.SetActive(false);
            Unpause();
        }
        ToggleSkillTreeOpen();
    }
    public void UpdateSkillPointText()
    {
        SkillPoints skillPointScript = GameObject.FindWithTag("GamesManager").GetComponent<SkillPoints>();

        if (skillPointScript == null ) 
            throw new ArgumentNullException("Skill points not found in Games Manager"); 

        string newText = $"Available Skill Points: {skillPointScript.GetSkillPoints()}";
        SkillTreeText.GetComponent<TextMeshProUGUI>().text = newText;
        int numberOfSkillPoints = skillPointScript.GetSkillPoints();
    }


    private void Pause() { Time.timeScale = 0f; }
    private void Unpause() { Time.timeScale = 1f; }
    private void ToggleSkillTreeOpen() { skillTreeIsOpen = !skillTreeIsOpen; }

}
