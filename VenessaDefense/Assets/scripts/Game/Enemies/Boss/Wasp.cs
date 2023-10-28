using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    public Transform target; //The thing that the enemy is chasing
    public Vector2 targetLocation; //Where the enemy wants to go
    public AIstate state;
    public AIstate defaultState = AIstate.dashAttack;
    public Vector2 targetDirectionVector;

    public Rigidbody2D body;

    public GameObject warningEffect;
    public bool isWarning = true;
    public float dashAttackTimer = 0.0f;

    public bool isAttacking = true;
    public int happenOnceAgain = 1;
    public float dashForce = 1000.0f;

    public float dashAttackTimerCoolDown = 0.0f;

    public GameObject currentWarningRectangle;
    //Movement
    public float dashSpeed = 5.0f;
    public enum AIstate
    {
       dashAttack,
       poisonAttack,
       stun
    } //end enum AIstate


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        state = AIstate.dashAttack;
    }

    // Update is called once per frame
   
    public virtual void Update()
    {
      //  Debug.Log(dashAttackTimer);
        UpdateAI();
    }
    
    public void  UpdateAI()
    {
        if(state == AIstate.dashAttack)
        {
            body.isKinematic = false;
            if(isAttacking == true)
            {
           
            //Animation to warn the player where itll attack
            //Does the attack
            if(isWarning)
            {
                if(happenOnceAgain == 1)
                {
                     targetLocation = GameObject.FindWithTag("Player").transform.position;
                    target =  GameObject.FindWithTag("Player").transform;
                    // Define an offset distance in front of the enemy
                    float offsetDistance = 1.0f; // Adjust this value as needed

                     // Calculate the position of the warning rectangle in front of the enemy
                    Vector2 warningPosition = (Vector2)transform.position + (targetDirectionVector.normalized * offsetDistance);

                    // Instantiate the warning rectangle at the calculated position
                     currentWarningRectangle = Instantiate(warningEffect, warningPosition, Quaternion.identity);

                      // Rotate the warning rectangle to face the player

                     Vector2 directionToPlayer = (Vector2)target.position - warningPosition;
                    float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                    currentWarningRectangle.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

                happenOnceAgain = 0;
                }
                dashAttackTimer+=Time.deltaTime;
                if(dashAttackTimer > 1.0f)
                {
                   // Debug.Log("Runs");
                    dashAttackTimer = 0.0f;
                    isWarning = false;
                    Destroy(currentWarningRectangle);
                }
            }
            else
            {
            DoDashAttack();
            isAttacking = false;
             
             
            }
            }
            dashAttackTimerCoolDown+= Time.deltaTime;
            if(dashAttackTimerCoolDown > 5.0f)
            {
                isAttacking = true;
                dashAttackTimerCoolDown = 0.0f;
                state = AIstate.stun;
                
            }
        }//End Dash Attack
        else if(state == AIstate.stun)
        {
           int randomNumber = Random.Range(1, 5); // Generates a random integer between 1 (inclusive) and 4 (inclusive)
            body.velocity = body.velocity.normalized * 0.0f;
            body.angularVelocity = 0.0f;
            body.isKinematic = true;
            state = AIstate.dashAttack;
              transform.position = new Vector3(0, 0, 0);
           if(randomNumber == 1)
           {
            transform.position = new Vector3(0, 0, 0);
           
           }

        }
    }

    public void DoDashAttack()
    {
     //   Debug.Log("runs");
        /*
        //Calculate the Vector to the targetLocation.
        targetDirectionVector = targetLocation - (Vector2)transform.position;
        //Normalize this to a unit vector (magnitude of 1)
        targetDirectionVector.Normalize();

        //Calculate Distance to the targetLocation, to prevent overshooting it
        float distance = Vector2.Distance(targetLocation, transform.position);
        Vector2 targetMove = targetDirectionVector * dashSpeed;

         body.velocity += new Vector2(targetMove.x * 0.2f, targetMove.y * 0.2f);
         isWarning = true;
         happenOnceAgain = 1;
         //Debug.Log("runs");
         */
         // Calculate the Vector to the targetLocation.
        targetDirectionVector = targetLocation - (Vector2)transform.position;
        // Normalize this to a unit vector (magnitude of 1).
        targetDirectionVector.Normalize();

        // Apply force in the direction of the targetLocation
        Vector2 dashForceVector = targetDirectionVector * dashForce;
        body.AddForce(dashForceVector, ForceMode2D.Impulse); // Impulse mode for an instant force
    
        happenOnceAgain = 1;
        isWarning = true;
       
        
    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        // Calculate the direction of the push (for example, based on the enemy's forward direction)
        Vector3 pushDirection = transform.forward;
        
        // Apply a force to the player
        Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        playerRigidbody.AddForce(pushDirection, ForceMode.Impulse);
    }
     if (collision.gameObject.CompareTag("Tower"))
    {
        // Calculate the direction of the push (for example, based on the enemy's forward direction)
        Vector3 pushDirection = transform.forward;
        

    }
}



}
