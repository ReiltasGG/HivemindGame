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
    public GameObject collidedObject;
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
            Debug.Log("Ran");
            GameObject temp = GameObject.Find("Domain");
            var temper = temp.GetComponent<DomainEffect>();
            temper.changeDomain();
            Destroy(collidedObject);
            //Debug.Log("runs");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            collidedObject = other.gameObject;
           textPopUp.SetActive(true);
           isInRange = true;
         
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            isInRange = false;
            textPopUp.SetActive(false);
        
        }
    }
}
