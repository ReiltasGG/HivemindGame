using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    //Health and important stuffs
    public int health;
    public bool isInvul = false;


    public Transform target; //The thing that the enemy is chasing
    public Vector2 targetLocation; //Where the enemy wants to go
    public AIstate state;
    public AIstate defaultState = AIstate.defaultAttackPlayer;
    public Vector2 targetDirectionVector;

    public Rigidbody2D body;

    public GameObject warningEffect;
    public bool isWarning = true;
    public float dashAttackTimer = 0.0f;

    public bool isAttacking = true;
    public int happenOnceAgain = 1;
    public float dashForce = 1000.0f;
    public bool dashPos = true;


    public float dashAttackTimerCoolDown = 0.0f;

    //Stun Stuff
    public float stunTime = 5.0f;
    public float currentStunTime = 0.0f;
    public bool stunOnce = true;


    public GameObject currentWarningRectangle;

    //Laser Stuff
    public GameObject laserBullet;
    public bool laserHappenOnce = true;

    //Laser Beam Stuff
    public GameObject LaserBeam;
    public bool laserbeamhappenOnce = true;
    public GameObject stingerOffset;
    public float timerforbeam = 0.0f;
    private Vector3 direction;
    //Movement
    public float dashSpeed = 200.0f;
    public enum AIstate
    {
       dashAttack,
       poisonAttack,
       stun,
       followLaser,
       resetPos,
       defaultAttackPlayer,
       laserBeam
       
    } //end enum AIstate


    // Start is called before the first frame update
    void Start()
    {
       defaultState = AIstate.laserBeam;
        body = GetComponent<Rigidbody2D>();
        state = defaultState;
    }

    // Update is called once per frame
   
    public virtual void Update()
    {
      //  Debug.Log(dashAttackTimer);
        UpdateAI();
    }
    
    public void  UpdateAI()
    {
      //Code for dash attack AI state
        if(state == AIstate.dashAttack)
        {
            if(dashPos == true)
            {
                  int randomNumber = Random.Range(1, 5);
                dashPos = false;
            if(randomNumber == 1)
           {
            transform.position = new Vector3(10, 10, 0);
           
           }
           else if(randomNumber == 2)
           {
            transform.position = new Vector3(-10, -10, 0);
           }
           else if(randomNumber == 3)
           {
             transform.position = new Vector3(-20, 0, 0);
           }
           else if(randomNumber == 4)
           {
             transform.position = new Vector3(25, 0, 0);
           }
           else if(randomNumber == 5)
           {
             transform.position = new Vector3(0, -15, 0);
           }

            }
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
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));

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
                dashPos = true;
                state = AIstate.resetPos;
                
            }
        }//End Dash Attack
        //Stun state
        else if(state == AIstate.resetPos)
        {
            if(stunOnce == true)
            {
            stunOnce = false;
           int randomNumber = Random.Range(1, 5); // Generates a random integer between 1 (inclusive) and 4 (inclusive)
            body.velocity = body.velocity.normalized * 0.0f;
            body.angularVelocity = 0.0f;
            body.isKinematic = true;
           
             // transform.position = new Vector3(0, 0, 0);
           if(randomNumber == 1)
           {
            transform.position = new Vector3(0, 0, 0);
           
           }
           else if(randomNumber == 2)
           {
            transform.position = new Vector3(-10, 1, 0);
           }
           else if(randomNumber == 3)
           {
             transform.position = new Vector3(2, -6, 0);
           }
           else if(randomNumber == 4)
           {
             transform.position = new Vector3(-6, -6, 0);
           }
           else if(randomNumber == 5)
           {
             transform.position = new Vector3(9, -6, 0);
           }
        
        }
            currentStunTime += Time.deltaTime;
        if(currentStunTime>= stunTime)
        {
            currentStunTime = 0.0f;
            stunOnce = true;
            state = AIstate.followLaser;
        }
        laserHappenOnce = true;
      }

        if(state == AIstate.followLaser)
        {
          transform.position = new Vector3(5,5, 0);
          if(laserHappenOnce == true)
          {
            

            //Code so that it faces the player
             Vector2 directionToPlayer = (Vector2)target.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
            Instantiate(laserBullet, transform.position, Quaternion.identity);
            laserHappenOnce = false;
          }
       
        }

        if(state == AIstate.defaultAttackPlayer)
        {
          moveDefaultAttack();
        }


        if(state == AIstate.laserBeam)
        {
          if(laserbeamhappenOnce == true)
          {
          GameObject createdLaser = Instantiate(LaserBeam, stingerOffset.transform.position , Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 180)));

          createdLaser.transform.SetParent(this.gameObject.transform);
          
          laserbeamhappenOnce = false;
          Vector3 direction = target.transform.position - transform.position;

            // Calculate the angle in radians
            float angle = Mathf.Atan2(direction.y, direction.x);

            // Convert the angle to degrees and set the rotation
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90f);
          }
          
            GameObject playerObject = GameObject.FindWithTag("Player");
        Transform targetchange = playerObject != null ? playerObject.transform : null;

if (targetchange != null)
{
    Vector3 direction = targetchange.position - transform.position;

    // Calculate the angle in radians
    float angle = Mathf.Atan2(direction.y, direction.x);

    // Interpolate the rotation smoothly
    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90f), 20.0f * Time.deltaTime);
}

        }
    }
      public void moveDefaultAttack()
      {
         targetLocation = GameObject.FindWithTag("Player").transform.position;
            target =  GameObject.FindWithTag("Player").transform;
        //Calculate the Vector to the targetLocation.
        targetDirectionVector = targetLocation - (Vector2)transform.position;
        //Normalize this to a unit vector (magnitude of 1)
        targetDirectionVector.Normalize();

        //Calculate Distance to the targetLocation, to prevent overshooting it
        float distance = Vector2.Distance(targetLocation, transform.position);
        Vector2 targetMove = targetDirectionVector * 5.0f;

        if (distance < targetMove.magnitude)
            targetMove = targetLocation - (Vector2)transform.position;
            bool clear = true;
        bool blockingAlly = false;
        /*
        CircleCollider2D myCollider = GetComponent<CircleCollider2D>();

        Vector2 myFutureCenter = body.position + myCollider.offset + targetMove * Time.deltaTime;
        float myRadius = myCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);

        //Detect the colliders that the Enemy would be hitting if it moved there
        Collider2D[] things = Physics2D.OverlapCircleAll(myFutureCenter, myRadius);
        foreach (Collider2D item in things)
        {
            if (item == myCollider)
                continue;
            if (item.CompareTag("Enemy") || item.CompareTag("Player"))
                clear = false;
            if (item.CompareTag("Enemy"))
                blockingAlly = true;
        }
*/
        if (clear == true)
        {
            //Move the monster
            body.velocity += new Vector2(targetMove.x * 0.2f, targetMove.y * 0.2f);
            //Check to see if the monster is moving too fast
            if (body.velocity.magnitude > 5.0f)
                body.velocity = body.velocity.normalized * 5.0f;

            //Old Way
            //body.MovePosition(body.position + targetMove);
        }
        else if (clear == false && blockingAlly == true)
        {
            body.velocity += new Vector2(targetMove.x * -0.05f, targetMove.y * -0.05f);
        }
      }

    public void defaultAttackPlayer()
    {

    }


   public void DoDashAttack()
{//Code
    // Calculate the Vector to the targetLocation.
    targetDirectionVector = targetLocation - (Vector2)transform.position;
    // Normalize this to a unit vector (magnitude of 1).
    targetDirectionVector.Normalize();

    // Calculate Distance to the targetLocation, to prevent overshooting it
    float distance = Vector2.Distance(targetLocation, transform.position);
    Vector2 targetMove = targetDirectionVector * dashSpeed;

    // Apply velocity for the dash attack
    body.velocity = targetMove;
    
    // Set the warning flag and reset the 'happenOnceAgain' flag
    isWarning = true;
    happenOnceAgain = 1;
}


    void OnCollisionEnter2D(Collision2D collision)
{
if (collision.gameObject.CompareTag("Player"))
{
    // Calculate the direction of the push (perpendicular to the enemy's forward direction)
    Vector3 pushDirection = transform.forward;
    Quaternion leftRotation = Quaternion.Euler(0, -90, 0); // Rotate left by 90 degrees
    Quaternion rightRotation = Quaternion.Euler(0, 90, 0); // Rotate right by 90 degrees

    // Apply a force to the player
    Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

    // You can choose either left or right direction for the push
    playerRigidbody.AddForce(leftRotation * pushDirection, ForceMode2D.Impulse);
    // or
    // playerRigidbody.AddForce(rightRotation * pushDirection, ForceMode.Impulse);
    Debug.Log("works");
}

     if (collision.gameObject.CompareTag("Tower"))
    {
        // Calculate the direction of the push (for example, based on the enemy's forward direction)
        Vector3 pushDirection = transform.forward;
        

    }
    if(collision.gameObject.CompareTag("Bullet"))
    {
     
    }
}



}
