using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteraction : MonoBehaviour
{
    [Tooltip("The \"Ekey Visual\" player child object")]
  //  public SpriteRenderer eKey;
    public GameObject textPopUp;
    public KeyCode keyChoice;

    private bool isInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        textPopUp = GameObject.Find("InteractText");
     //   eKey.enabled = false;
     textPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //When the player releases the "use button" currently "e".
        // Can use "Input.GetKeyUp("e")/
        if (Input.GetKeyUp("e"))
        {
            Vector2 position = (Vector2)transform.position + GetComponent<CircleCollider2D>().offset;
            float radius = GetComponent<CircleCollider2D>().radius;

            //Get all interactables within range
            Collider2D[] things = Physics2D.OverlapCircleAll(position, radius);
    
        }
        if(Input.GetKeyDown(keyChoice) && isInRange)
        {
            GameObject temp = GameObject.Find("SkillTreeOpener");
            var temper = temp.GetComponent<UI_SkillTreeOpener>();
            temper.ToggleTree();
            Debug.Log("runs");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
           textPopUp.SetActive(true);
           isInRange = true;
         
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            isInRange = false;
            textPopUp.SetActive(false);
        
        }
    }
}
