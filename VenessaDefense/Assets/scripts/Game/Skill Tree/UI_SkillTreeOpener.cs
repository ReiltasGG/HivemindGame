using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_SkillTreeOpener : MonoBehaviour
{
    public GameObject skillTreeFinder;
  //  public GameObject skillTreeButton = null;
    public int countClicks = 0;
    public GameObject skillTreeBackground;

    public bool skillTreeIsOpen = false;

    public void ToggleTree()
    {
        if (skillTreeIsOpen)
        {
            //Text treeButton = skillTreeButton.GetComponentInChildren<Text>();
          //  treeButton.text = "Close Tree";
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

    private void Pause() { Time.timeScale = 0f; }
    private void Unpause() { Time.timeScale = 1f; }
    private void ToggleSkillTreeOpen() { skillTreeIsOpen = !skillTreeIsOpen; }

}
