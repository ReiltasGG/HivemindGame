using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;
    public bool skill1 = false;


    [SerializeField] private TrailRenderer tr;

    enum AbilityState{
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

    public KeyCode key;
    
    // Update is called once per frame
    void Update()
    {
       
       if(skill1 == true)
        {
        switch(state)
        {
            case AbilityState.ready:
            if(Input.GetKeyDown(key))
            {
              //  ability.Activate();
                ability.Activate(gameObject);
                state = AbilityState.active;
                activeTime = ability.activeTime;
                tr.emitting = true;
            }
            break;
            case AbilityState.active:
            if(activeTime > 0)
            {
                activeTime -= Time.deltaTime;
                
            }
            else
            {
                ability.BeginCoolDown(gameObject);
                state = AbilityState.cooldown;
                cooldownTime = ability.cooldownTime;
                tr.emitting = false;
            }
            break;
            case AbilityState.cooldown:
             if(cooldownTime > 0)
            {
                tr.emitting = false;
                cooldownTime -= Time.deltaTime;
            }
            else
            {
                state = AbilityState.ready;
            }
            break;
        }
        }
    }

    public void allowSkill1()
    {
        skill1= true;
        Debug.Log("Allowskill1 ran");
    }
}
