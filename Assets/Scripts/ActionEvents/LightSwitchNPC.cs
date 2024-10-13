using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Wilberforce;

public class LightSwitchNPC : MonoBehaviour
{
    int curStep = 0;
    Transform initialPosition;
    public float MovementSpeed = 4;
    private Rigidbody2D rbody;
    private Transform curTarget;
    private Light2D globalLight;
    void Start()
    {
        rbody = transform.parent.GetComponentInChildren<Rigidbody2D>();
        initialPosition = rbody.transform;
        curTarget = transform;
        globalLight = GameObject.FindAnyObjectByType<Light2D>();
    }

    void Update()
    {
        curTarget = transform.GetChild(curStep);
        float curDistance = Vector3.Distance(curTarget.position, rbody.position);
        if (globalLight.color == Color.white && curStep != 0)
        {

        }
            if (curDistance < 0.5 && curStep == 0 && globalLight.color == Color.white)
        {
            // Light it's on
            // NPC it's in its place
            // Everything it's ok
            // Don't do anything
            return;
        }
        else
        {
            rbody.position = Vector2.MoveTowards(rbody.position, curTarget.position, MovementSpeed * Time.deltaTime);
        }

        curDistance = Vector3.Distance(curTarget.position, rbody.position);
        if (curDistance >= 0.5 && curStep > 0 && globalLight.color == Color.white)
        {
            curStep -= 1;
        } else if (curDistance >= 0.5 && curStep > 0 && globalLight.color != Color.white)
        {
            curStep -= 1;
        } else if (curDistance < 0.5 && curStep ==  && globalLight.color != Color.white)




            if (curStep >= transform.childCount)
        {
            curStep = 0;
            globalLight.color = Color.white;
        } else
        {
            {
                curStep += 1;
            }
            else
            {
                
            }
        }
    }
}
