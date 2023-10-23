using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class DecoyAbility : Ability
{
    public GameObject decoy;
    
   public override void Activate(GameObject parent)
   {
     GameObject stinger = GameObject.FindWithTag("Stinger");
     Vector3 _StingerOffset = stinger.transform.position;
     Quaternion newRotation = parent.transform.rotation * Quaternion.Euler(180f, 180f, 180f);
        Instantiate(decoy, _StingerOffset, newRotation);
   }
}
