using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Vector3 _moveVec = Vector3.zero;

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        _moveVec = new Vector3(inputVec.x, inputVec.y, 0);
    }
    public void Update()
    {
        transform.Translate(_moveVec * _moveSpeed * Time.deltaTime);
    }
}
