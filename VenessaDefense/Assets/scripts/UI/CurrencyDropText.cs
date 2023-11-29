using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyDropText : MonoBehaviour
{
    [SerializeField]
    private GameObject TextPrefab;

    public void ShowCurrency(Vector3 position, int currencyWorth)
    {
        GameObject currencyText = Instantiate(TextPrefab, position, Quaternion.identity);
        TextMeshPro textBox = currencyText.GetComponent<TextMeshPro>();

        textBox.text = $"+{currencyWorth} Honey";

        StartCoroutine(FadeOutCoroutine(currencyText));
    }

    private IEnumerator FadeOutCoroutine(GameObject gameObject, float fadeDuration=1.0f)
    {
        CanvasGroup canvasGroup = gameObject.AddComponent<CanvasGroup>();
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            canvasGroup.alpha = alpha;

            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;

        Destroy(gameObject);
    }
}
