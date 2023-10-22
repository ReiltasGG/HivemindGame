using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : Enemy
{
    
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
                          Debug.Log("This runs");
                //The animation will call the Attack() method for the blob
                //The animation will call the EndAttack() method to exit isAttacking
                //if (animator != null)
                    animator.SetTrigger("Attack");
                    

                isAttacking = true;
            }
           
        }
            animator.SetTrigger("Idle");
       
        
       
    }

     private void FixedUpdate()
    {
       
            Move();
        
    }  //end FixedUpdate()

      public override void Attack()
    {
        PBAoEAttack();
    }
}
