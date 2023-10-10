using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics;

public class HealingBee : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask playerMask;

    [Header("Attribute")]
    [SerializeField] private float healingRange = 4f;
    [SerializeField] private float healCooldownInSeconds = 10f;
    [SerializeField] private int healingAmount = 20;
    [SerializeField] private GameObject healingCircle;
    private AttributesManager playerAttributesManager;


    private List<GameObject> cursedEnemies = new List<GameObject>();
    private float timePassedSinceHeal;

    private void Start()
    {
        timePassedSinceHeal = healCooldownInSeconds;
    }
    private void Update()
    {
        timePassedSinceHeal += Time.deltaTime;

        if (timePassedSinceHeal >= healCooldownInSeconds)
        {
            ShowCircle();
            HealPlayer();
        }
        else HideCircle();
            
    }
    private void HealPlayer()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, healingRange, (Vector2)transform.position, 0f, playerMask);
        HealAllInCircle(hits);

    }

    private void HealAllInCircle(RaycastHit2D[] selectedPlayers)
    {
        UnityEngine.Debug.Log("Selected Players hit length: " + selectedPlayers.Length);
        if (selectedPlayers == null) throw new ArgumentNullException("RaycastHit2D passed in is NULL");
        if (selectedPlayers.Length == 0) return;


        for (int i = 0; i < selectedPlayers.Length; i++)
        {
            RaycastHit2D hit = selectedPlayers[i];
            GameObject player = hit.collider.gameObject;
            playerAttributesManager = player.GetComponent<AttributesManager>();

            if (!isFullHealth(playerAttributesManager))
            {
                playerAttributesManager.heal(healingAmount);
                timePassedSinceHeal = 0f;
            }
        }
    }

    private bool isFullHealth(AttributesManager entityAttributesManager)
    {
        return !(entityAttributesManager.health < entityAttributesManager.maxHealth);
    }

    private void ShowCircle()
    {
        if (healingCircle != null)
            healingCircle.SetActive(true);
    }

    private void HideCircle()
    {
        if (healingCircle != null)
            healingCircle.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, healingRange);
    }
}
