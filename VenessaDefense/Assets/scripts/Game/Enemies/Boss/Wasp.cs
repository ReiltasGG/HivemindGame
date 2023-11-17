using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
  public bool tempStunOnce = true;
    //Knockback
    public float meleeAttackKnockback = 4f;
    //Health and important stuffs
    public int health;
    public int halfHealth;
    public int rageHealth;
    public bool isInvul = false;

    public float timeBetweenAbilities = 10.0f;
    public float currentTimeBetweenAbilities;

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
    public bool doingDashAttack = false;

    //Stun Stuff
    public float stunTime = 2.0f;
    public float currentStunTime = 0.0f;
    public bool stunOnce = true;
    public float actualStunTime = 5.0f;


    public GameObject currentWarningRectangle;

    //Laser Stuff
    public GameObject laserBullet;
    public bool laserHappenOnce = true;
    public float currentLaserTime = 0.0f;
    public float laserFollowTime = 2.0f;

    //Laser Beam Stuff
    public GameObject LaserBeam;
    public bool laserbeamhappenOnce = true;
    public GameObject stingerOffset;
    public float timerforbeam = 0.0f;
    private Vector3 direction;
    public float laserBeamTimer = 5.0f;
    public float currentLaserBeamTimer = 0.0f;
    public GameObject createdLaser;
    //Movement
    public float dashSpeed = 200.0f;


    //Default attack stuff
    public bool defaultAttacking = false;
    public float meleeAttackRadius = 2.0f;
    //public float attackCooldownTicker;
    public float meleeAttackCooldown = 2.0f;
    //Water and Blinding stuff for boss effect 
    public GameObject waterEffect;
    public float timerForWater;
    public float currentTimeForWater;
    public bool isDomainActive = false;
    public bool isDomainHappenOnce = true;
    public GameObject domainObject;
    

    //Ability stuff
    public float currentAbilityTimer= 0.0f;
    public float AbilityTimer = 1.0f;
    public GameObject disableObject;
    

    //Grab Attack Stuff
    public GameObject grabAttackHitBox;
    public bool grabHappenOnceForce = true;
     public float burstSpeed = 3f; // Adjust the burst speed as needed
    public float pauseDuration = 0.5f; // Duration to pause before the burst
    public float grabInitalLast = .5f;
    private bool tempHasHappened = false;
    public GameObject MashingBar;
    public GameObject mashText;
    public int grabDamagePerSec = 5;
    public float grabIntervalDamageSec = 1.0f;



    private Animator anim;

    public enum AIstate

    {
       dashAttack,
       grabAttack,
       stun,
       followLaser,
       resetPos,
       defaultAttackPlayer,
       laserBeam,
       ability
       
    } //end enum AIstate


    // Start is called before the first frame update
    void Start()
    {
       defaultState = AIstate.defaultAttackPlayer;
        body = GetComponent<Rigidbody2D>();
        state = defaultState;
         health = GetComponent<AttributesManager>().getHealth();
          halfHealth = health/2;
      rageHealth = health/10;
      MashingBar.SetActive(false);
      mashText.SetActive(false);
      anim = GetComponent<Animator>();
      //grabAttackHitBox.SetActive(false);
    }

    // Update is called once per frame
   
    public virtual void Update()
    {
      //  Debug.Log(dashAttackTimer);
    
        UpdateAI();
        waterCreation();
        updateHealth();
    } 

    public void updateHealth()
    {
      health = GetComponent<AttributesManager>().getHealth();
    
    }

    public void waterCreation()
    {
     var domainScript =  domainObject.GetComponent<DomainEffect>();
     isDomainActive = domainScript.isDomainActive();
     if(isDomainActive)
     {
      if(currentTimeForWater > timerForWater)
      {
        currentTimeForWater = 0.0f;
        Instantiate(waterEffect, transform.position, Quaternion.identity);
      }
      currentTimeForWater+=Time.deltaTime;

     }
    }
    
    public void  UpdateAI()
    {

      if(state == AIstate.stun)
      {
          body.velocity = body.velocity.normalized * 0.0f;
            body.angularVelocity = 0.0f;
            anim.SetTrigger("Stun");
        if(actualStunTime < 0)
        {
          actualStunTime = 5.0f;
          state = AIstate.defaultAttackPlayer;
        }
        actualStunTime-=Time.deltaTime;
      }
      //Code for grab attack
      if(state == AIstate.grabAttack)
      {
        grabAttackHitBox.SetActive(true);
        doGrabAttack();
        if(tempHasHappened == true)
        {
        grabActive();
        }
      }
      //Code for dash attack AI state
        if(state == AIstate.dashAttack)
        {
          anim.SetTrigger("Dive");
            if(dashPos == true)
            {
                  int randomNumber = Random.Range(1, 6);
                dashPos = false;
            if(randomNumber == 1)
           {
            transform.position = new Vector3(30, 30, 0);
           
           }
           else if(randomNumber == 2)
           {
            transform.position = new Vector3(-25, -25, 0);
           }
           else if(randomNumber == 3)
           {
             transform.position = new Vector3(-30, 0, 0);
           }
           else if(randomNumber == 4)
           {
             transform.position = new Vector3(30, 0, 0);
           }
           else if(randomNumber == 5)
           {
             transform.position = new Vector3(0, -35, 0);
           }

            }
          //  body.isKinematic = false;
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
              doingDashAttack = false;
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
          // int randomNumber = Random.Range(1, 5); // Generates a random integer between 1 (inclusive) and 4 (inclusive)
            body.velocity = body.velocity.normalized * 0.0f;
            body.angularVelocity = 0.0f;
            body.isKinematic = true;
           
             // transform.position = new Vector3(0, 0, 0);
        transform.position = new Vector3(30,30, 30);
        
        }
        
            currentStunTime += Time.deltaTime;
        if(currentStunTime>= stunTime)
        {
            currentStunTime = 0.0f;
            stunOnce = true;
             state = AIstate.dashAttack;
        }
        
       
        laserHappenOnce = true;
      }

        if(state == AIstate.followLaser)
        {
              Vector2 directionToPlayer = (Vector2)target.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
            if(laserHappenOnce)
            {
            body.velocity = body.velocity.normalized * 0.0f;
            //Code so that it faces the player
         
            Instantiate(laserBullet, transform.position, Quaternion.identity);
            laserHappenOnce = false;
            }
            if(currentLaserTime > laserFollowTime)
            {
            laserHappenOnce = true;
            currentLaserTime = 0.0f;
            state = AIstate.defaultAttackPlayer;
          

            }
            currentLaserTime +=Time.deltaTime; 
          
       
        }

        if(state == AIstate.defaultAttackPlayer)
        {
          anim.SetTrigger("Fly");
          moveDefaultAttack();
          defaultAttackPlayer();
        
          if(health <= rageHealth)
          state = AIstate.resetPos;
          if(health < halfHealth && isDomainActive == false && isDomainHappenOnce)
          {
          //  Debug.Log("This runs");
            isDomainHappenOnce = false;
            state = AIstate.ability;
            
          }
          if(currentTimeBetweenAbilities> timeBetweenAbilities)
          {
            if(health > halfHealth || isDomainActive == false )
            {
            int rand = Random.Range(1, 4);
        
            if(rand == 1)
              state = AIstate.grabAttack;
            if(rand == 2)
              state = AIstate.grabAttack;
            if(rand == 3)
            {
              state = AIstate.grabAttack;
            }
            }
            
            else if(health <= halfHealth && isDomainActive == false)
            {
               int rand = Random.Range(1, 5);
              //Debug.Log("i access this");
            if(rand == 1)
              state = AIstate.laserBeam;
            if(rand == 2)
              state = AIstate.followLaser;
            if(rand == 3)
              state = AIstate.ability;
            if(rand == 4)
              state = AIstate.grabAttack;
              
            } else if(health <= halfHealth && isDomainActive == true)
            {
               int rand = Random.Range(1, 4);
              Debug.Log("i access this");
            if(rand == 1)
              state = AIstate.laserBeam;
            if(rand == 2)
              state = AIstate.followLaser;
            if(rand == 3)
              state = AIstate.grabAttack;
              
            }
            
                 currentTimeBetweenAbilities = 0;
            }
          

          currentTimeBetweenAbilities+=Time.deltaTime;
          
        }


        if(state == AIstate.laserBeam)
        {
            body.velocity = body.velocity.normalized * 0.0f;
            body.angularVelocity = 0.0f;
          if(laserbeamhappenOnce == true)
          {
           createdLaser = Instantiate(LaserBeam, stingerOffset.transform.position , Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 180)));

          createdLaser.transform.SetParent(this.gameObject.transform);
          
          laserbeamhappenOnce = false;
          Vector3 direction = target.transform.position - transform.position;

            // Calculate the angle in radians
            float angle = Mathf.Atan2(direction.y, direction.x);

            // Convert the angle to degrees and set the rotation
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90f);
          }
          if(laserBeamTimer < currentLaserBeamTimer)
          {
            currentLaserBeamTimer = 0.0f;
            laserbeamhappenOnce = true;
            Destroy(createdLaser);
            state = AIstate.defaultAttackPlayer;
          }
          currentLaserBeamTimer+=Time.deltaTime;
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
    if(state == AIstate.ability)
      {
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, 0, 0);
        if(AbilityTimer< currentAbilityTimer)
        {
        var tempScript = GameObject.Find("Domain").GetComponent<DomainEffect>();
        
        tempScript.changeDomain();
        currentAbilityTimer = 0.0f;
           Vector3 spawnPosition = new Vector3(
            Random.Range(-10f, 10f), // Adjust these values based on the desired X-axis range
            Random.Range(-5f, 5f),   // Adjust these values based on the desired Y-axis range
            0f                        // Assuming a 2D game, so Z-axis is 0
        );

        // Instantiate the object at the random position
        Instantiate(disableObject, spawnPosition, Quaternion.identity);
        state = AIstate.defaultAttackPlayer;

        }
        currentAbilityTimer +=Time.deltaTime;
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
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Vector2 directionToPlayer = (Vector2)(player.transform.position - transform.position);
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
            transform.rotation = targetRotation;
        }

      }
    public void defaultAttackPlayer()
    {
        meleeAttackCooldown -= Time.deltaTime;

       if (!defaultAttacking && meleeAttackCooldown <= 0)
        {
         //   Debug.Log("Works Here");
            //Check the distance to the target
            Vector3 targetOffset = target.GetComponent<Collider2D>().offset;
            Vector3 myOffset = GetComponent<Collider2D>().offset;
            float distance = Vector2.Distance(target.position + targetOffset, transform.position + myOffset);
            //Debug.Log(distance);
            //If the target is in range, start a melee attack
            if (distance <= meleeAttackRadius)
            {
                //The animation will call the Attack() method for the blob
                anim.SetTrigger("defaultAttack");
                //The animation will call the EndAttack() method to exit isAttacking

                defaultAttacking = true;

            }
        }//end melee check
    }

    public void defaultAttackPlayerAnim()
    {
      //Do Through Anim
       //Search for players within the meleeAttackRadius
            Collider2D[] things = Physics2D.OverlapCircleAll(transform.position, meleeAttackRadius);
            foreach (Collider2D item in things)
            {
                if (item.gameObject.CompareTag("Player"))
                {
                    //If it's a player, deal melee damage to it
                    var attributeScript = item.gameObject.GetComponent<AttributesManager>();

                    //Calculate the direction of force
                    float angle = 45.0f; // Set the desired angle

                   // Rotate the direction vector by the specified angle
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    Vector2 hitForce = (item.transform.position - transform.position).normalized * meleeAttackKnockback * 10.0f;
                    item.gameObject.GetComponent<PlayerInteraction>().addKnockBack(hitForce);
                    //Apply Knockback and damage to player
                    attributeScript.takeDamage(10);
                    //EndAttack();
                 
                }
            } //end of searching for Players

           
            meleeAttackCooldown = 2.0f;
    }
  
    public void EndAttack()
    {
        isAttacking = false;
        defaultAttacking = false;
    }

   public void DoDashAttack()
{//Code
  body.isKinematic = true;
        body.useFullKinematicContacts = true;
    doingDashAttack = true;
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

  //Code for grab attack
  public void doGrabAttack()
  {
   
    bool temp = grabAttackHitBox.GetComponent<grabHurtBox>().hasTouchedPlayer();
    // Debug.Log(temp);
    if(grabHappenOnceForce)
      {
        grabHappenOnceForce = false;
        body.velocity = body.velocity * 4.0f;
      }
         if(temp)
    {
      tempHasHappened = true;
      //Debug.Log("This ran");
    }
      if(grabInitalLast < 0)
      {
       // body.velocity = body.velocity/4.0f;
        grabInitalLast = 1.0f;
        
        if(!tempHasHappened)
        {
          grabHappenOnceForce = true;
          state = AIstate.defaultAttackPlayer;
        }
      
      }
      grabInitalLast -=Time.deltaTime;

    
 
    
  
  }

  public void grabActive()
  {
    //Debug.Log("Grab Active");
    //Debug.Log("Grab Happened");
    //Freeze Player and Enemy
     body.velocity = body.velocity.normalized * 0.0f;
    body.angularVelocity = 0.0f;

    //Setactive the bar
    MashingBar.SetActive(true);
    mashText.SetActive(true);

    //If bar is max then they get out of grab
    float tempValue = MashingBar.GetComponent<MashingBar>().getSliderValue();
    if(tempValue >= 100)
    {   
      var tempPoison = GetComponent<PoisonManager>();
      tempPoison.ApplyPoison(GameObject.FindWithTag("Player"));
      tempHasHappened = false;
      MashingBar.GetComponent<MashingBar>().restartMash();
      MashingBar.SetActive(false);
      mashText.SetActive(false);
      //grabAttackHitBox.SetActive(false);
      grabIntervalDamageSec = 1.0f;
      GameObject findPlayer = GameObject.FindWithTag("Player");
      var playerScript = findPlayer.GetComponent<PlayerMovement>();
      playerScript.changeGrabFalse();
      grabHappenOnceForce = true;
      state = AIstate.defaultAttackPlayer;
   

    }
    else
    {
      GameObject findPlayer = GameObject.FindWithTag("Player");
      var playerScript = findPlayer.GetComponent<PlayerMovement>();
      playerScript.changeGrabTrue();
      if(grabIntervalDamageSec <= 0)
      {
        GameObject PlayerTemp = GameObject.FindWithTag("Player");
        var atMan = PlayerTemp.GetComponent<AttributesManager>();
        atMan.takeDamage(grabDamagePerSec);
        grabIntervalDamageSec = 1.0f;
      }
    }
    //As they are trapped they lose 5 hp per second
    grabIntervalDamageSec -=Time.deltaTime;
    //At end of GrabActive()
  }

  public void endGrabAnim()
  {
    tempHasHappened = false;
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


    // or
    // playerRigidbody.AddForce(rightRotation * pushDirection, ForceMode.Impulse);
 //   Debug.Log("works");
 if(doingDashAttack)
 {
  collision.gameObject.GetComponent<AttributesManager>().takeDamage(10);
                   Vector2 hitDirection = (collision.transform.position - transform.position).normalized;
                float angle = 45.0f; // Set the desired angle

                // Rotate the direction vector by the specified angle
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Vector2 rotatedDirection = rotation * hitDirection;

                // Calculate the knockback force
                Vector2 hitForce = rotatedDirection * meleeAttackKnockback * 30.0f;
                collision.gameObject.GetComponent<PlayerInteraction>().addKnockBack(hitForce);
 }
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
