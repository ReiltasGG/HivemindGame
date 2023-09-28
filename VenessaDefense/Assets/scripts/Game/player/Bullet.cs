using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AntMovement>())
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
           screenPosition.x > cam.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > cam.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}
