using System;
using UnityEngine;
using UnityEngine.UI;


public class SoundEffectSlider : MonoBehaviour
{
    public SoundEffectManager soundEffectManager;
    public Slider slider;

    void Start()
    {
        if (slider == null)
            throw new ArgumentNullException("No slider attached to script");

        if (soundEffectManager == null)
            throw new ArgumentNullException("No sound effect manager attached to script");

        slider.onValueChanged.AddListener((volume) =>
        {
            soundEffectManager.ChangeSoundEffectVolume(volume);
        });
    }
}
