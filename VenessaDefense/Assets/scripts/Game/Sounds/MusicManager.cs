using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource currentSong;
    private int currentSongNumber = 0;
    public float volume = 1.0f;

    void Start()
    {
        if (songs.Length == 0)
            throw new ArgumentNullException("No songs added to Music Manager");

        currentSong = gameObject.AddComponent<AudioSource>();
        currentSong.volume = volume;

        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        if (currentSongNumber >= songs.Length)
            currentSongNumber = 0;

        if (songs[currentSongNumber] == null)
            throw new ArgumentNullException("Next song is null in music manager");

        AudioClip clip = songs[currentSongNumber];
        currentSong.clip = clip;

        currentSong.Stop();
        currentSong.Play();

        currentSongNumber++;

        Invoke("PlayNextTrack", currentSong.clip.length);
    }

    public void ChangeVolume(float volume_)
    {
        volume = volume_;
        currentSong.volume = volume;
    }

}
