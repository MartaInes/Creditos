using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Vector2 _moveVec = Vector2.zero;
    private Rigidbody2D myRigidBody;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input)
    {
        _moveVec = input.Get<Vector2>();
    }
    private void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + (_moveVec * _moveSpeed * Time.fixedDeltaTime));
        //transform.Translate(_moveVec * _moveSpeed * Time.deltaTime);
    }
}
