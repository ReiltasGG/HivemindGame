using UnityEngine;
using TMPro;

public class textFlicker : MonoBehaviour
{
    public float flickerSpeed = 1f;
    private TextMeshProUGUI textMeshPro;
    private bool isTextVisible = true;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("This script requires a TextMeshProUGUI component.");
            enabled = false;
            return;
        }

        // Start the flickering coroutine
        InvokeRepeating("FlickerText", 0f, flickerSpeed);
    }

    void FlickerText()
    {
        // Toggle visibility
        isTextVisible = !isTextVisible;

        // Set the text visibility
        textMeshPro.enabled = isTextVisible;
    }
}
