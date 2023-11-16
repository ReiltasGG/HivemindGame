using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class ButtonSound : MonoBehaviour
{
    public SoundEffectManager SoundEffectManager;

    private void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        if (SoundEffectManager != null)
        {
            SoundEffectManager.PlayButtonClickSound();
        }
    }
}
