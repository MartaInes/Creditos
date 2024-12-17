using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalScore : MonoBehaviour
{
    private int team1Points;
    private ScoreCredits[] team1Stamps;

    private int team2Points;
    private ScoreCredits[] team2Stamps;
    void Start()
    {
        team1Stamps = gameObject.transform.Find("team1").GetComponentsInChildren<ScoreCredits>();
        team2Stamps = gameObject.transform.Find("team2").GetComponentsInChildren<ScoreCredits>();
    }

    void Update()
    {
        team1Points = 0;
        team2Points = 0;
        foreach (var stamp in team1Stamps)
        {
            if (stamp.GetComponent<Image>().enabled)
            {
                team1Points++;
            }
        }
        foreach (var stamp in team2Stamps)
        {
            if (stamp.GetComponent<Image>().enabled)
            {
                team2Points++;
            }
        }
        if(team1Points > team2Points)
        {
            Debug.Log("Team 1 is winning!");
        } else if(team1Points < team2Points)
        {
            Debug.Log("Team 2 is winning!");
        }
        if(team1Points + team2Points == 5)
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
