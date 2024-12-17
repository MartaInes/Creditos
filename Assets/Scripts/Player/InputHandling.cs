using UnityEngine;
using UnityEngine.InputSystem;

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
    public void OnRespawn()
    {
        player1.Respawn();
        player2.Respawn();
    }
    public void OnMove1(InputValue input)
    {
        if (player1 != null)
        {
            player1.MoveTo(input.Get<Vector2>());
        }
    }
    public void OnMove2(InputValue input)
    {
        if (player2 != null)
        {
            player2.MoveTo(input.Get<Vector2>());
        }
    }
    public void OnActivate1()
    {
        if (player1 != null)
        {
            player1.Activate("Team1");
        }
    }
    public void OnActivate2()
    {
        if (player2 != null)
        {
            player2.Activate("Team2");
        }
    }
}
