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
   // [SerializeField] private float aps= 0.25f;//attacks per second
    [SerializeField] private float curseTime = 3f;
    [SerializeField] private float damageBoost = 2f;
    public GameObject curseEffect;
    private Animator anim;


    private List<GameObject> cursedEnemies = new List<GameObject>();
    public float timeUntilFire = 6f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 6f)
            {
                anim.SetTrigger("Attack");
                timeUntilFire = 0f;
            }
     

    }
    private void CurseEnemies()
    {
        anim.Play("ShamanTower_Idle");
       // Debug.Log("Happened");
        Instantiate(curseEffect, transform.position, transform.rotation);
        
        /*
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
        */
       
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
        /* This only works in editor, cannot build with this
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
        */
    }
    
}
