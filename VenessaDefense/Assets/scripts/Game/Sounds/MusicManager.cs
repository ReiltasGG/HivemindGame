using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource currentSong;
    private int currentSongNumber = 0;

    void Start()
    {
        if (songs.Length == 0)
            throw new ArgumentNullException("No songs added to Music Manager");

        currentSong = gameObject.AddComponent<AudioSource>();

        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        Debug.Log("songs length: " + songs.Length);
        Debug.Log("current song number: " + currentSongNumber);
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

}
