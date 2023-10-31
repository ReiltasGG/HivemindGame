using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_SkillTreeOpener : MonoBehaviour
{
    //This code will open the skill tree through changing the transform

    public GameObject skillTreeFinder;
  //  public GameObject skillTreeButton = null;
    public int countClicks = 0;
    public GameObject skillTreeBackground;

    public bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        //skillTreeButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTree();
    }

    public void spawnTree()
    {
       // countClicks++;
        if (open)
        {
            //Text treeButton = skillTreeButton.GetComponentInChildren<Text>();
          //  treeButton.text = "Close Tree";
            skillTreeFinder.SetActive(true);
            skillTreeBackground.SetActive(true);
            Time.timeScale = 0f;
           // Debug.Log("This runs");

        }
        else
        {
            skillTreeFinder.SetActive(false);
            skillTreeBackground.SetActive(false);
            Time.timeScale = 1f;
        }

    }

    public void setOpen()
    {
        if(open)
        open = false;
        else
            open = true;
    }

    public void setOpenFalse()
    {
        open = false;
    }
}