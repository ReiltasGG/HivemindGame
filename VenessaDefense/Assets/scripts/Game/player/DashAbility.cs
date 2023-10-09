using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class DashAbility : Ability
{
    

    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
    
    PlayerMovement movement = parent.GetComponent<PlayerMovement>();
    Rigidbody2D Rigidbody2D = parent.GetComponent<Rigidbody2D>();

    Rigidbody2D.velocity = movement.movementInput.normalized * dashVelocity;
     }

public override void BeginCoolDown(GameObject parent)
     {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        movement.acceleration = movement.normalAcceleration;
     }
}
