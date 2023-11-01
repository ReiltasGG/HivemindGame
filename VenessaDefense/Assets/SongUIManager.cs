using UnityEngine;
using TMPro;
using System;

public class SongUIManager : MonoBehaviour
{

    [SerializeField] public MusicManager musicManager;
    [SerializeField] public GameObject songNameGameObject;
    [SerializeField] public GameObject songTimeGameObject;

    private TextMeshProUGUI songNameTextBox;
    private TextMeshProUGUI songTimeTextBox;

    void Start()    
    {
        songNameTextBox = songNameGameObject.GetComponent<TextMeshProUGUI>();
        songTimeTextBox = songTimeGameObject.GetComponent<TextMeshProUGUI>();

        if (musicManager == null)
            throw new ArgumentNullException("Please assign music manager");
    }

    private void Update()
    {
        SetSongNameText();
        SetSongTimeText();
    }

    public void SkipTrack()
    {
        musicManager.SkipTrack();
    }

    private void SetSongNameText()
    {
        songNameTextBox.text = musicManager.GetSongName();
    }
    private void SetSongTimeText()
    {
        int currentSongTime = musicManager.GetCurrentSongTime();
        int songLength = musicManager.GetSongLength();

        string text = $"{convertSecondsToMinutes(currentSongTime)} / {convertSecondsToMinutes(songLength)}";

        songTimeTextBox.text = text;
    }

    private string convertSecondsToMinutes(int seconds)
    {
        const int SecondsInMinute = 60;
        return $"{seconds / SecondsInMinute}:{seconds % SecondsInMinute:D2}";
    }

}
