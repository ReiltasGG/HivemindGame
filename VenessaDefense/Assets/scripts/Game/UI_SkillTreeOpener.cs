using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTreeOpener : MonoBehaviour
{
    //This code will open the skill tree through changing the transform

    public GameObject skillTreeFinder;
    public GameObject skillTreeButton = null;
    public int countClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        skillTreeButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnTree()
    {
        countClicks++;
        if (countClicks % 2 == 1)
        {
            //Text treeButton = skillTreeButton.GetComponentInChildren<Text>();
          //  treeButton.text = "Close Tree";
            skillTreeFinder.SetActive(true);
            Debug.Log("This runs");

        }
        else
            skillTreeFinder.SetActive(false);

    }
}
