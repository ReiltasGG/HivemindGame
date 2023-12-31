using System;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public float volume = 1.0f;

    private const string ENEMY_DEATH_FILE_NAME = "EnemyDeath";
    private const string GUN_SHOOT_FILE_NAME = "gun_shoot";
    private const string ROUND_START_FILE_NAME = "Round-Start";
    private const string OBJECTIVE_COMPLETED_FILE_NAME = "ObjectiveCompleted";
    private const string BUTTON_CLICK_FILE_NAME = "Button_Click";
    private const string SOUNDS_FOLDER = "Sounds/";

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void ChangeSoundEffectVolume(float volume_)
    {
        if (volume_ > 1 || volume_ < 0)
            throw new ArgumentException("Volume is out of range");

        volume = volume_;
    }

    public void PlayRoundStartSound() { PlaySoundEffect(GetSoundEffect(ROUND_START_FILE_NAME)); }
    public void PlayEnemyDeathSound() { PlaySoundEffect(GetSoundEffect(ENEMY_DEATH_FILE_NAME), 0.7f); }
    public void PlayGunShootSound() { PlaySoundEffect(GetSoundEffect(GUN_SHOOT_FILE_NAME)); }
    public void PlayObjectiveCompletedSound() { PlaySoundEffect(GetSoundEffect(OBJECTIVE_COMPLETED_FILE_NAME)); }
    public void PlayButtonClickSound() { PlaySoundEffect(GetSoundEffect(BUTTON_CLICK_FILE_NAME)); }

    private AudioClip GetSoundEffect(string soundFileName)
    {
        AudioClip soundEffect = Resources.Load<AudioClip>(SOUNDS_FOLDER + soundFileName);

        if (soundEffect == null)
            throw new ArgumentException($"Please ensure sound file name is {SOUNDS_FOLDER + soundFileName}");

        return soundEffect;
    }

    private void PlaySoundEffect(AudioClip soundEffect, float soundVolume = 1)
    {
        audioSource.volume = volume * soundVolume;
        audioSource.PlayOneShot(soundEffect);
    }
}
