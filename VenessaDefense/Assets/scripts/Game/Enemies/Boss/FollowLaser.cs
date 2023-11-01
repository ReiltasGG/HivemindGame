using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FollowLaser : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Transform player;
    public GameObject blindingEffect;
    public Volume bloomVolume; // Assign the Post-Processing Volume in the Unity editor.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Calculate the rotation angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Set the rotation of the laser to face the player
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Move the laser towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject temp = other.gameObject;
            var playerAttributeMan = temp.GetComponent<AttributesManager>();
            playerAttributeMan.takeDamage(20);
            Destroy(this.gameObject);

            Instantiate(blindingEffect, transform.position, transform.rotation);

            // Activate the bloom effect by enabling the Post-Processing Volume.
            if (bloomVolume != null)
            {
                bloomVolume.enabled = true;
            }
        }
    }
}
