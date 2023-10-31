using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour
{
    public float blinkDuration = 1.0f; // Duration of each blink cycle in seconds
    public Color blinkColor = Color.blue; // Color to blink to

    private Color originalColor; // Store the original color
    private SpriteRenderer spriteRenderers; // Reference to the object's SpriteRenderer component
    private bool isBlinking = false;

    private void Start()
    {
        // Get the SpriteRenderer component and store the original color
        spriteRenderers = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderers.color;

        // Start the blink routine
        StartBlinking();
    }

    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            // Blink to the specified color
            spriteRenderers.color = blinkColor;

            // Wait for the specified blink duration
            yield return new WaitForSeconds(blinkDuration);

            // Return to the original color
            spriteRenderers.color = originalColor;

            // Wait for the same duration
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
