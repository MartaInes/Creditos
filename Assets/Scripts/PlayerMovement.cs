using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Vector2 _moveVec1 = Vector2.zero;
    private Vector2 _moveVec2 = Vector2.zero;
    private Rigidbody2D rigidBody1;
    private Rigidbody2D rigidBody2;

    private void Awake()
    {
        rigidBody1 = GetComponentsInChildren<Rigidbody2D>()[0];
        rigidBody2 = GetComponentsInChildren<Rigidbody2D>()[1];
    }

    public void OnMove(InputValue input)
    {
        _moveVec1 = input.Get<Vector2>();
    }
    public void OnMove2(InputValue input)
    {
        _moveVec2 = input.Get<Vector2>();
    }
    private void FixedUpdate()
    {
        rigidBody1.MovePosition(rigidBody1.position + (_moveVec1 * _moveSpeed * Time.fixedDeltaTime));
        rigidBody2.MovePosition(rigidBody2.position + (_moveVec2 * _moveSpeed * Time.fixedDeltaTime));
        //transform.Translate(_moveVec * _moveSpeed * Time.deltaTime);
    }
}
