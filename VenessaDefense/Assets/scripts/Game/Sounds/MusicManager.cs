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
    public void SkipTrack()
    {
        PlayNextTrack();
    }

    public void ChangeMusicVolume(float volume_)
    {
        if (volume_ > 1 || volume_ < 0)
            throw new ArgumentException("Volume is out of range");

        volume = volume_;
        currentSong.volume = volume;
    }
    public int GetCurrentSongTime() { return currentSong.clip == null ? 0 : Mathf.FloorToInt(currentSong.time); }
    public int GetSongLength() { return currentSong.clip == null? 0 : Mathf.FloorToInt(currentSong.clip.length); }
    public string GetSongName() {  return currentSong.clip == null? "": currentSong.clip.name; }

}
