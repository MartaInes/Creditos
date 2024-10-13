using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] AudioSource hurryUp;
    [SerializeField] List<AudioSource> mainSong;
    [SerializeField] List<AudioSource> saxo1;
    [SerializeField] AudioSource saxo3;
    [SerializeField] AudioSource saxo4;

    [SerializeField] GameObject credits;

    [SerializeField] TextMeshProUGUI timerText;
    public float timeInMinutes = 3f; 
    private float timeRemaining;
    private bool isSaxo1 = false;
    private bool isSaxo2 = false;
    private bool isHurryUp = false;
    void Start()
    {
        timeRemaining = timeInMinutes * 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            timeRemaining = Mathf.Max(timeRemaining, 0);

            UpdateTimerDisplay(timeRemaining);
            if (timeRemaining <= 2.4f * 60 && !isSaxo1)
            {
                isSaxo1 = true;
                foreach (var song in saxo1)
                {
                    song.mute = true;
                }
                saxo3.Play();
            }
            if (timeRemaining <= 1.84f * 60 && !isSaxo2)
            {
                isSaxo2 = true;
                saxo3.mute = true;

                saxo4.Play();
            }
            if (timeRemaining <= 1.3f*60 && !isHurryUp)
            {
                isHurryUp = true;
                foreach (var song in mainSong)
                {
                    song.mute = true;
                }
                saxo4.mute = true;
                hurryUp.Play();
            }
            if (timeRemaining <= 0f)
            {
                credits.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    void UpdateTimerDisplay(float time)
    {
        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        // Format the text as "MM:SS"
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
