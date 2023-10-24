using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enemy;


public abstract class Enemy : MonoBehaviour
{
    //Instance Variables
    //Generic
    [Header("Basic")]
    [Tooltip("Name of the enemy type.")]
    public string enemyName;
    [Tooltip("Data Dictionary - Data")]
  //  public DataDictionary data;
    [SerializeField] protected bool debugDrawGizmos = false;
    [SerializeField] protected float offsetY = 0.0f;

    //Components
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D body;
    protected Animator animator;

    //Movement
    [Header("Movement")]
    public float moveSpeed = 3.0f;
    protected Vector2 homePosition;
    protected Transform target; //The thing that the enemy is chasing
    protected Vector2 targetLocation; //Where the enemy wants to go
    protected Vector2 targetDirectionVector; //The vector to the target
    protected Vector2 directionOfEnemy;
    protected float targetLocationDistance;
    protected float moveModifier = 1.0f;
    [Tooltip("Multiplier on enemy movement while in the Patrolling State [Default = 1.0].")]
    public float patrolSpeedModifier = 0.5f;
    [Tooltip("Add Empty GameObjects to this.  The enemy will go through the list in order, and will start at the closest waypoint.")]
    public Transform[] waypointList;
    protected int currentWaypoint = 0;

    //Health
    [Header("Health")]
    [Tooltip("Starting (and max) health of the enemy.  Basic sword deals 10 damage.")]
    public int healthMax = 30;
    [Tooltip("Does this creature respawn when the map reloads?")]
    //  public bool allowRespawn = true;
   // [Tooltip("The default death effect to play when the enemy is defeated and disappears.")]
  //  public GameObject deathEffect;
    protected int health;
    //protected bool isStunned = false;
    //protected float isStunnedTicker = 0.0f;
    //protected float hitFlashTicker = -0.1f;
    //protected float hitFlashDuration = -0.1f;

    //Attacking
    [Header("Combat")]
    [Tooltip("Does this monster want to stop moving towards the player once they are in range of their ranged attack?")]
    public bool preferRanged = true;
    [Tooltip("The radius the enemy begins its melee attack at (and effect of effect for PBAoE melee). This is from the center, so consider the blob's collider side!")]
    public float meleeAttackRadius = 0.9f;
    [Tooltip("The damage of the melee attack")]
    public int meleeAttackDamage = 10;
    [Tooltip("The knockback strength of the melee attack")]
    public float meleeAttackKnockback = 4f;
    [Tooltip("The wait time after the enemy attacks before it can attack again.")]
    public float meleeAttackCooldown = 0.5f;
    [Tooltip("Attack range for the enemy to start a ranged attack")]
    public float rangedAttackRange = 0f;
    [Tooltip("OPTIONAL: Prefab the enemy spawns when doing a ranged attack")]
    public GameObject rangedProjectilePrefab;
    [Tooltip("The wait time after the enemy makes a ranged attack before it can attack again.")]
    public float rangedAttackCooldown = 1.2f;
    protected float attackCooldownTicker = 0f;
    protected bool isAttacking = false;
    //AI Stuff
    [Header("AI")]
    [Tooltip("What AI Behavior to start in: Recommended idle, patrol, or superaggro")]
    public AIstate defaultState = AIstate.chase;
    [Tooltip("The distance at which the monster switches to chasing the player.")]
    public float aggroRadius = 5.0f;
    protected AIstate state; //Current state

    private bool attackTower;
    public float towerAttackRadius = 2.0f;
    GameObject potentialPlayers;

    public Color originalColor;

    public enum AIstate
    {
        //0 - Wait around at its start location
        //1 - Moving between waypoints (like idle, but moves)
        chase, //2 - Follows player for just existing
        aggro, //3 Before monster attacks, warning attack
        chaseTower //When the enemy attacks towers instead of player

    } //end enum AIstate



    // Start is called before the first frame update
    void Start()
    {
        // Link Components
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
       // potentialPlayers = GameObject.FindWithTags("Player");
        // Initialize Important Variables
        health = healthMax;
        homePosition = transform.position;
        targetLocation = homePosition;
        state = AIstate.chase;
        attackCooldownTicker = Mathf.Max(meleeAttackCooldown, rangedAttackCooldown);

        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        Vector3 enemyPosition = transform.position;

        // Calculate the direction vector from the enemy to the player
        Vector3 direction = playerPosition - enemyPosition;

        // Calculate the angle in radians between the forward vector (z-axis) and the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x);

        // Calculate the z-axis rotation in degrees
        float zRotation = angle * Mathf.Rad2Deg;

        // Set the rotation of the enemy only on the z-axis
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
        
        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        originalColor = sp.color;
    }


    public virtual void Update()
    {
        if(target == null)
        {
            changeTarget();
        }
        UpdateAttackCooldowns();
        UpdateAI();
        updateRotationOfEnemy();
        
    }

    // Update is called once per frame
    //Built-In method that runs after ALL updates are finished
    public virtual void LateUpdate()
    {
        CheckHealth();
    }

    protected void updateRotationOfEnemy()
    {
        // Find the player object
        //GameObject player = GameObject.FindWithTag("Player");

        if (target != null)
        {
            // Calculate the direction vector from this object to the player
            Vector3 direction = target.transform.position - transform.position;

            // Calculate the angle in radians
            float angle = Mathf.Atan2(direction.y, direction.x);

            // Convert the angle to degrees and set the rotation
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90f);
        }


    }
    
    protected virtual void CheckHealth()
    {
       

        if (health <= 0)
        {
            //[Extra] Play Death sound
            // sounds.PlayOneShot(sounds.audio_death);
          //  data.dataBoolean[getUniqueName()] = true;

            //Create the deathEffect
          //  Instantiate(deathEffect, transform.position, Quaternion.identity);

            //[Extra] Change the destroy before based on the death sound
           
                //Delete the Enemy
                Destroy(this.gameObject);
         
        }
    }
    

    protected void UpdateAttackCooldowns()
    {
        if (attackCooldownTicker > 0)
        {
            attackCooldownTicker -= Time.deltaTime;
            if (attackCooldownTicker <= 0)
                isAttacking = false;
        }
    }

    //Code for movement

    protected void Move()
    {
        //Calculate the Vector to the targetLocation.
        targetDirectionVector = targetLocation - (Vector2)transform.position;
        //Normalize this to a unit vector (magnitude of 1)
        targetDirectionVector.Normalize();

        //Calculate Distance to the targetLocation, to prevent overshooting it
        float distance = Vector2.Distance(targetLocation, transform.position);
        Vector2 targetMove = targetDirectionVector * moveSpeed * moveModifier;

        if (distance < targetMove.magnitude)
            targetMove = targetLocation - (Vector2)transform.position;

        //Check to see if the desired move is clear of Players
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
            if (body.velocity.magnitude > moveSpeed * moveModifier)
                body.velocity = body.velocity.normalized * moveSpeed * moveModifier;

            //Old Way
            //body.MovePosition(body.position + targetMove);
        }
        else if (clear == false && blockingAlly == true)
        {
            body.velocity += new Vector2(targetMove.x * -0.05f, targetMove.y * -0.05f);
        }

    } //end Move()


    public abstract void Attack();

  
    protected GameObject RangedAttack()
    {
        GameObject projectile = null;

        if (rangedProjectilePrefab != null && target != null)
        {
            //Calculate angle to target
            float deltaY = target.position.y - transform.position.y;
            float deltaX = target.position.x - transform.position.x;
            float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

            //Spawn projectile rotated towards the player
            projectile = Instantiate(rangedProjectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));

            isAttacking = true;
            attackCooldownTicker = rangedAttackCooldown;
        }

        return projectile;

    } //end RangedAttack

    protected void PBAoEAttack()
    {
       // Debug.Log("I run");
            //Search for players within the meleeAttackRadius
            Collider2D[] things = Physics2D.OverlapCircleAll(transform.position, meleeAttackRadius);
            foreach (Collider2D item in things)
            {
                Debug.Log(item);
                if (item.gameObject.CompareTag("Player"))
                {
                    //If it's a player, deal melee damage to it
                    var script = item.gameObject.GetComponent<AttributesManager>();

                    //Calculate the direction of force
                    Vector2 hitForce = (item.transform.position - transform.position).normalized * meleeAttackKnockback * 10.0f;

                    //Apply Knockback and damage to player
                    script.takeDamage(meleeAttackDamage);

                    //[Extra] Spawn Damage Text
                 //   SpawnText(meleeAttackDamage, item.ClosestPoint(transform.position), false);
                }
                if (item.gameObject.CompareTag("Tower"))
                {
                    Debug.Log("entered comparetag tower for pbaoe");
                    //If it's a tower, deal melee damage to it
                    var tower = item.gameObject.GetComponent<AttributesManager>();
                    if (tower != null)
                    {
                        Debug.Log("entered tower != null function");
                    //Calculate the direction of force
                        Vector2 hitForce = (item.transform.position - transform.position).normalized * meleeAttackKnockback * 10.0f;

                    //Apply Knockback and damage to player
                        tower.takeDamage(meleeAttackDamage);
                        Debug.Log("dealing damage to tower");
                    }

                    



                    else
                    {
                        FindNewTarget();
                    }

                    //[Extra] Spawn Damage Text
                 //   SpawnText(meleeAttackDamage, item.ClosestPoint(transform.position), false);
                }
            } //end of searching for Players

            isAttacking = true;
            attackCooldownTicker = meleeAttackCooldown;
      

    } //end PBAoEAttack

    protected void FindNewTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");

    // Check if the player exists and calculate the distance
    if (player != null)
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        
        // Get all towers in the scene
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        float closestTowerDistance = float.MaxValue;
        Transform closestTower = null;

        foreach (var tower in towers)
        {
            // Check if the tower is not destroyed
            if (tower != null)
            {
                float towerDistance = Vector2.Distance(transform.position, tower.transform.position);
                if (towerDistance < closestTowerDistance)
                {
                    closestTowerDistance = towerDistance;
                    closestTower = tower.transform;
                }
            }
        }

        // Compare distances and update the target
        if (playerDistance < closestTowerDistance)
        {
            target = player.transform;
            targetLocation = target.position;
        }
        else
        {
            target = closestTower;
            targetLocation = closestTower.position;
        }
    }
    }

    protected void UpdateAI()
    {
        moveModifier = 1;

        if (target == null){
            FindNewTarget();
        }

        if (state == AIstate.chase)
        {

            targetLocation = GameObject.FindWithTag("Player").transform.position;
            target =  GameObject.FindWithTag("Player").transform;
            //Debug.Log(targetLocation);
       //    Debug.Log(targetLocation);
            moveModifier = 1.2f;
            Collider2D[] things = Physics2D.OverlapCircleAll(transform.position, aggroRadius);
            if(target == null)
            {
                changeTarget();
            }
    
        
            Collider2D[] towerRadius = Physics2D.OverlapCircleAll(transform.position, towerAttackRadius);
            foreach(Collider2D item in things)
            {
                if(item.gameObject.CompareTag("Tower"))
                {
                    target = item.gameObject.transform;
                    targetLocation = item.gameObject.transform.position;
                    state = AIstate.chaseTower;
                    
                }
            }

                  foreach (Collider2D item in things)
            {
                //If there is a player, set target to that player and go to CHASE state
                if (item.gameObject.CompareTag("Player"))
                {
                    target = item.gameObject.transform;
                    state = AIstate.aggro;
                    
                }
            }
            Move();
        }

        if (state == AIstate.aggro)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            if (distance > aggroRadius * 1.20f)
            {
                //Conditon: Target is outside of aggrorange +20%.
                //Result: Go back to idle or patrol
                state = defaultState;
                target = GameObject.FindWithTag("Player").transform;
            }
            else
            {
                //Condition: Target is within aggrorange +20%
                //Result: Move towards the player (or stay at ranged distance)
                if (preferRanged)
                {
                    //Ranged Perferred
                    if (distance >= rangedAttackRange)
                        targetLocation = target.position; //Out of range: chase player more
                    else
                        targetLocation = transform.position; //In range: stay still
                }
                else
                {
                    //Melee Perferred
                   targetLocation = target.position; //Chase the player!
                }
            }
            if(target == null)
            {
                changeTarget();
            }
        }

         if (state == AIstate.chaseTower)
        {
             Collider2D[] temp = Physics2D.OverlapCircleAll(transform.position, aggroRadius);
               float distance = Vector2.Distance(target.position, transform.position);
                foreach(Collider2D item in temp)
            {
                if(item.gameObject.CompareTag("Player"))
                {
                    target = item.gameObject.transform;
                    state = defaultState;
                }
            }
             if (preferRanged)
                {
                    //Ranged Perferred
                    if (distance >= rangedAttackRange)
                        targetLocation = target.position; //Out of range: chase player more
                    else
                        targetLocation = transform.position; //In range: stay still
                }
                else
                {
                    //Melee Perferred
                    targetLocation = target.position; //Chase the player!
                }
            
            Move();
            if(target == null)
            {
                changeTarget();
            }
        }
    }
     public void EndAttack()
    {
        isAttacking = false;
    }

    public void changeTarget()
    {
       // Debug.Log("y no work");
        state = AIstate.chase;
        target = GameObject.Find("Player with health").transform;
        targetLocation = target.position;
    }
}
