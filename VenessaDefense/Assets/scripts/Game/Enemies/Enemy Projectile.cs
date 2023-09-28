using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Tooltip("How fast the projectile travels (Recommended >= 7)")]
    public float moveSpeed = 8.0f;

    [Tooltip("How much damage the projectile deals")]
    public int damage = 15;

    [Tooltip("How much knockback the projectile deals")]
    public float knockback = 6.0f;

    [Tooltip("Number of units the fireball travels before disappearing.")]
    public float range = 7;

    [Tooltip("Does this have an \"Impact\" animation? (The bone does not)")]
    public bool hasImpactAnimation = true;

    [Tooltip("Offset: Positive Numbers make this object more likely to be behind things.")]
    public float offsetY = -0.3f;

    //Components
    private Rigidbody2D body;
    private Collider2D myCollider;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 movement = Vector2.zero;
    private Vector2 startingPosition;
    private bool isActive = true;


    // Start is called before the first frame update
    void Start()
    {
        //Connect all of the components
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       // animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        isActive = true;
        startingPosition = transform.position;
    } //end Start()

    // Update is called once per frame
    void Update()
    {
        //Calculate Layer order
        //Note: The value must be between -32768 and 32767.
        spriteRenderer.sortingOrder = 30000 - (int)((spriteRenderer.bounds.min.y + offsetY) * 100);

        //Check distance vs range
        if (Vector2.Distance(transform.position, startingPosition) > range && isActive)
            Impact();
    } //end Update()

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Hit a player
            //If it's a player, deal melee damage to it
            //Player script = other.gameObject.GetComponent<Player>();

            //Calculate the direction of force
            Vector2 hitForce = (other.transform.position - transform.position).normalized * knockback * 10.0f;

            //Apply Knockback and damage to player
         //   script.Hit(damage, hitForce);

            Impact();
        }
        else if (other.name.Contains("Blocking"))
        {
            Impact();
        }
    }//end OnTriggerEnter2D

    public void Impact()
    {
        /*
        if (hasImpactAnimation)
        {
            //Has an Impact animation (Firebolt, Waterbolt, Lightningbolt, Earthbolt)
            animator.SetTrigger("Impact");
            myCollider.enabled = false;
            isActive = false;
            Destroy(this.gameObject, 0.5f); //Destroy after one second, incase animation doesn't work
        }
        */
       
        
            //Does not have an impact animation (Darkbolt, Bone)
            Destroy(this.gameObject);
       
    }

    private void FixedUpdate()
    {
        if (isActive)
            body.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
