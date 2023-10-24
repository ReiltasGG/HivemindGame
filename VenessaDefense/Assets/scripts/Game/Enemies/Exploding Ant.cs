using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingAnt : Enemy
{
    /*
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationspeed;

    private Rigidbody2D _rb;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float newSpeed;
    [SerializeField] private float baseSpeed;
    public AttributesManager SelfAttributesManager;


    private GameObject explodingAnt;

    public GameObject Explosion;

    //Code for death or making contact with player
    private int getDamage;
    private int explodeOnce = 1;

    //Code to make it blink Red
    private SpriteRenderer spriteRenderer;
    public float blinkSpeed = 0.5f; // Adjust the speed of the blink

    private Color originalColor;
    private Color redColor = Color.red;

    private bool isBlinking = true;

    private void Start()
    {        
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on child GameObject.");
        }

        StartBlinking();

        SelfAttributesManager = GetComponent<AttributesManager>();

        if (SelfAttributesManager == null)
            throw new ArgumentNullException("Explosion Attribute manager is null");

    }

    public void StartBlinking()
    {
        isBlinking = true;
    }//end StartBlinking

    public void StopBlinking()
    {
        isBlinking = false;
        // Reset the sprite color to its original color when blinking stops
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on child GameObject.");
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }
    private void Update()
    {
        if (isBlinking)
        {
            // Find the SpriteRenderer component on the child GameObject
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float t = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
                spriteRenderer.color = Color.Lerp(originalColor, redColor, t);
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on child GameObject.");
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }

    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationspeed * Time.deltaTime);

        _rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.velocity = _targetDirection * _speed;
        }
    }
    public void UpdateSpeed(float newSpeed)
    {
        _speed = newSpeed;

    }
    public void ResetSpeed()
    {
        _speed = baseSpeed;

    }


    //Code for exploding
    public void Explode()
    {
        SelfAttributesManager.die();
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);   
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //   Debug.Log("True");
        if (other.gameObject.CompareTag("Player"))
        {
            if (explodeOnce == 1)
            {
                Explode();
                explodeOnce -=1;
            }
            //Hit a player
            //If it's a player, deal melee damage to it
            //Player script = other.gameObject.GetComponent<Player>();

            //Calculate the direction of force
            //  Vector2 hitForce = (other.transform.position - transform.position).normalized * knockback * 10.0f;

            //Apply Knockback and damage to player
            //   script.Hit(damage, hitForce);

            //  Impact();
        }
        //else if (other.name.Contains("Blocking"))
        //{
        //  Impact();
        //}
    }//end OnTriggerEnter2D

   */
    public GameObject Explosion;
    public AttributesManager SelfAttributesManager;

    public float blinkSpeed = 0.5f; // Adjust the speed of the blink

  
    private Color redColor = Color.red;

    private bool isBlinking = true;

   public override void Update()
    {
        base.Update();
        if(target != null  && !isAttacking && attackCooldownTicker <= 0)
        {
              //Check the distance to the target
            Vector3 targetOffset = target.GetComponent<Collider2D>().offset;
            Vector3 myOffset = GetComponent<Collider2D>().offset;
            float distance = Vector2.Distance(target.position + targetOffset, transform.position + myOffset);
           // Debug.Log(distance);
            //If the target is in range, start a melee attack
            if (distance <= meleeAttackRadius)
            {
                        //  Debug.Log("This runs");
                //The animation will call the Attack() method for the blob
                //The animation will call the EndAttack() method to exit isAttacking
                //if (animator != null)
                    Explode();
                    

                isAttacking = true;
            }
           
        }
            animator.SetTrigger("Idle");

               if (spriteRenderer != null)
        {
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

            // Change color based on whether it's time to display red or original color
            if (t >= 0.5f)
            {
                spriteRenderer.color = Color.Lerp(originalColor, redColor, (t - 0.5f) * 2);
            }
            else
            {
                spriteRenderer.color = Color.Lerp(redColor, originalColor, t * 2);
            }
        }
        
       
    }

     private void FixedUpdate()
    {
       
            Move();
        
    }  //end FixedUpdate()

      public override void Attack()
    {
        Explode();
    }
    public void changeSpeed()
    {
        moveSpeed = 1.0f;
    }

    public void originalSpeed()
    {
        moveSpeed = 3.0f;
    }

        public void Explode()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        //Debug.Log("Runs");
        SelfAttributesManager.die();
        
        Destroy(gameObject);   
    }

        public void StopBlinking()
    {
        isBlinking = false;
        // Reset the sprite color to its original color when blinking stops
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on child GameObject.");
        }
    }
}
