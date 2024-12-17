using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCredits : MonoBehaviour
{
    public GameObject associatedObject;
    public string associatedTag;

    void Update()
    {
        if (associatedObject && associatedObject.tag == associatedTag)
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
        
    }
}
