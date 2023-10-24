using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public float volume = 1.0f;
    public AudioClip roundStartSound;

    public void ChangeSoundEffectVolume(float volume_)
    {
        if (volume_ > 1 || volume_ < 0)
            throw new ArgumentException("Volume is out of range");

        volume = volume_;
    }

    public void playRoundStartSound()
    {
        if (roundStartSound == null)
            throw new ArgumentNullException("Round start sound not attached to AudioSource");

        GameObject soundObject = new GameObject("RoundStartSoundObject");
        AudioSource roundStartSoundSource = soundObject.AddComponent<AudioSource>();

        roundStartSoundSource.clip = roundStartSound;
        roundStartSoundSource.volume = volume;

        roundStartSoundSource.Play();

        Destroy(soundObject, roundStartSound.length);
    }

}
