using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] AudioSource intro;
    [SerializeField] AudioSource bgMusic;

    private bool audio1Played;
    void Start()
    {
        intro.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!intro.isPlaying && !audio1Played)
        {
            audio1Played = true;
            bgMusic.Play();
        }
    }
}
