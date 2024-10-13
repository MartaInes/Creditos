using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputHandling : MonoBehaviour
{
    private Player player1;
    private Player player2;

    private void Start()
    {
        player1 = GetComponentsInChildren<Player>()[0];
        player2 = GetComponentsInChildren<Player>()[1];
    }

    private void Awake()
    {
    }

    public void OnMove1(InputValue input)
    {
        player1.MoveTo(input.Get<Vector2>());
    }
    public void OnMove2(InputValue input)
    {
        player2.MoveTo(input.Get<Vector2>());
    }
    public void OnActivate1()
    {
        player1.Activate();
    }
    public void OnActivate2()
    {
        player2.Activate();
    }
}
