using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiders : Enemy
{
    //Skeleton Specific Variables
    private float rangedAttackDelay = 0.5f;  //How long of a pause from the start of the attack animation to the spawning of the projectile
    private float rangedAttackDelayTicker = 0f;
    [Tooltip("The default idle direction of the skeleton.  (0, -1) is facing down.")]
    public Vector2 defaultFacing = new Vector2(0, -1);

    // Update is called once per frame
    public override void Update()
    {
        
        //Call the Enemy's Update() method for all the generic monster stuff
        base.Update();

        //If the skeleton is aiming (or recovering from a ranged attack) don't move
        if (isAttacking)
        {
            moveModifier *= 0.0f;
        }


        //Code here runs in Update() unique to the Skeleton
        //Attacking Logic for the Skeleton, who prefers ranged combat
        //Melee Attack
        // None, but it will still try to do a ranged attack even in melee range

        //Ranged Attack
        if (target != null && !isAttacking && attackCooldownTicker <= 0)
        {
            //Get the range to the player if the Skeleton is able to attack
            //float distance = Vector2.Distance(target.position, transform.position);  //Uses the Center of the object, not the center of the collider
            Vector3 targetOffset = target.GetComponent<Collider2D>().offset;
            Vector3 myOffset = GetComponent<Collider2D>().offset;
            float distance = Vector2.Distance(target.position + targetOffset, transform.position + myOffset);

            //If the target is in range, start a ranged attack
            if (distance <= rangedAttackRange)
            {

                isAttacking = true;
                //Start the timer to count down until it's time to spawn the projectile
                rangedAttackDelayTicker = rangedAttackDelay;

            }
        } //end ifRangedAttack

        //Decrease the ticker
        if (rangedAttackDelayTicker > 0)
        {
            rangedAttackDelayTicker -= Time.deltaTime;
            //If the timer's up, it's time to spawn the projectile
            if (rangedAttackDelayTicker <= 0)
            {
                Attack();
            }
        }

    } //end Update()


    private void FixedUpdate()
    {

        Move();


    }  //end FixedUpdate()

    //This is triggered by the animation
    public override void Attack()
    {

        RangedAttack();
        isAttacking = false; //We can override the RangedAttack setting it to true here.

    } //end Attack()


}
