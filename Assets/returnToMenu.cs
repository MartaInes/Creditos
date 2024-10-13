using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToMenu : MonoBehaviour
{
    [SerializeField] AudioSource song;

    // Update is called once per frame
    void Update()
    {
        if (!song.isPlaying)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
