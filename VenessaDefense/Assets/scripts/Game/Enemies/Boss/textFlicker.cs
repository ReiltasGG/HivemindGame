using UnityEngine;
using TMPro;

public class testFlicker : MonoBehaviour
{
    public float flickerSpeed = 1f;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("This script requires a TextMeshProUGUI component.");
            enabled = false;
            return;
        }

        // Start the color flickering coroutine
        InvokeRepeating("FlickerRainbow", 0f, 0.05f);
    }

    void FlickerRainbow()
    {
        // Calculate rainbow color based on time
        Color rainbowColor = Color.HSVToRGB(Time.time * flickerSpeed % 1f, 1f, 1f);

        // Apply the rainbow color to the text
        textMeshPro.color = rainbowColor;
    }
}
