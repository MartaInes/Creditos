using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePathFollow : MonoBehaviour
{
    int curStep = 0;
    public float MovementSpeed = 4;
    private Rigidbody2D rbody;
    private Transform curTarget;
    void Start()
    {
        rbody = transform.parent.GetComponentInChildren<Rigidbody2D>();
        curTarget = transform;
    }
    void Update()
    {
        if (curStep < transform.childCount)
        {
            curTarget = transform.GetChild(curStep);

            if (Vector3.Distance(curTarget.position, rbody.position) < 0.5)
            {
                curStep += 1;
            } else
            {
                rbody.position = Vector2.MoveTowards(rbody.position, curTarget.position, MovementSpeed * Time.deltaTime);
            }
        }
    }
}
