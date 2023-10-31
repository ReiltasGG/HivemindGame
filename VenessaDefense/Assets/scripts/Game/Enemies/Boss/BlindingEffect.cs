using UnityEngine;
using UnityEngine.UI;

public class BlindingEffect : MonoBehaviour
{
    public Image blindingEffectImage;
    public float fadeDuration = 5.0f;

    private float startTime;
    private bool isFading = false;

    void Start()
    {
        blindingEffectImage.gameObject.SetActive(false);
    }

    public void ActivateBlindingEffect()
    {
        blindingEffectImage.gameObject.SetActive(true);
        startTime = Time.time;
        isFading = true;
    }

    void Update()
    {
        if (isFading)
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime < fadeDuration)
            {
                // Calculate the current alpha value based on the elapsed time
                float alpha = 1 - (elapsedTime / fadeDuration);

                // Set the alpha value for the blinding effect
                Color imageColor = blindingEffectImage.color;
                imageColor.a = alpha;
                blindingEffectImage.color = imageColor;
            }
            else
            {
                // Fully fade out the blinding effect
                Color imageColor = blindingEffectImage.color;
                imageColor.a = 0;
                blindingEffectImage.color = imageColor;
                isFading = false;
                blindingEffectImage.gameObject.SetActive(false);
            }
        }
    }
}
