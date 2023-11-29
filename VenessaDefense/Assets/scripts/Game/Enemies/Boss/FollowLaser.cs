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
    public float letExistTimer = 1.0f;
    public int damageToPlayer = 10;

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
        letExistTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject temp = other.gameObject;
            var playerAttributeMan = temp.GetComponent<AttributesManager>();
            playerAttributeMan.takeDamage(damageToPlayer);
            Destroy(this.gameObject);


        }

        if(other.CompareTag("Enemy"))
        {
            if(letExistTimer <= 0.0f)
            {
            GameObject temp = other.gameObject;
            var waspScript = temp.GetComponent<Wasp>();
            waspScript.makeStun();
            Destroy(this.gameObject);
             var waspAttMan = temp.GetComponent<AttributesManager>();
            waspAttMan.takeDamage(5);
            }
        }
    }
}
