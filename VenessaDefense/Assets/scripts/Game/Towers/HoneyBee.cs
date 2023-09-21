using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HoneyBee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float aps= 0.25f;//attacks per second
    [SerializeField] private float stickTime = 1f;

    private float timeUntilFire;
    private void Update()
    {
  
     
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                StickEnemies();
                timeUntilFire = 0f;
            }

    }
    private void StickEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

               AntMovement am = hit.transform.GetComponent<AntMovement>();
               am.UpdateSpeed(1f);
               
                StartCoroutine(ResetAntSpeed(am));
                
            }
        }
       
    }
   private IEnumerator ResetAntSpeed(AntMovement am)
    {
        yield return new WaitForSeconds(stickTime);
        am.ResetSpeed();
       
    }
  
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
