using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShamanBee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 2f;
    [SerializeField] private float aps= 0.25f;//attacks per second
    [SerializeField] private float curseTime = 3f;
    [SerializeField] private float damageBoost = 2f;

    private List<GameObject> cursedEnemies = new List<GameObject>();
    private float timeUntilFire;
    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f / aps)
            {
                CurseEnemies();
                timeUntilFire = 0f;
            }

    }
    private void CurseEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                GameObject enemy = hit.collider.gameObject;
                if (!cursedEnemies.Contains(enemy)){
                    cursedEnemies.Add(enemy);
                    StartCoroutine(RemoveCurseAfterTime(enemy));
                }            
            }
        }
       
    }

   private IEnumerator RemoveCurseAfterTime(GameObject enemy)
    {
        yield return new WaitForSeconds(curseTime);
        cursedEnemies.Remove(enemy);   
    }

    public bool IsCursed(GameObject enemy){
        return cursedEnemies.Contains(enemy);
    }

    public float GetDamageBoost(GameObject enemy){
        return IsCursed(enemy) ? damageBoost : 1f;
    }
  
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
