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
        int temp = GameObject.Find("Domain").GetComponent<DomainEffect>().getAmountToChange();
        textMeshPro.text = "Go to the 4 corners of the screen\n" + temp + "/4.";
       // textMeshPro.alignment = TextAlignmentOptions.Center;
    }
}
