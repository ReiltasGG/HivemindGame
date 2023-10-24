using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public MusicManager musicManager;
    public Slider slider;

    void Start()
    {
        if (slider == null)
            throw new ArgumentNullException("No slider attached to script");

        if (musicManager == null)
            throw new ArgumentNullException("No music manager attached to script");

        slider.onValueChanged.AddListener((volume) =>
        {
            musicManager.ChangeMusicVolume(volume);
        });
    }
}
