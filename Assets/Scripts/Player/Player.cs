using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    private static int playersCounter = 0;
    private static string[] spawnTags = { "Spawn1", "Spawn2", "Spawn3", "Spawn4", "Spawn5", "Spawn6" };
    private static string[] playerLayers = { "Player1", "Player2", "Player3", "Player4", "Player5", "Player6" };

    private float _moveSpeed = 10f;
    private Vector2 _moveVec = Vector2.zero;

    private int playerIndex;
    private GameObject playerChair;
    private Rigidbody2D rigidBody;

    private HashSet<string> nearbyActionEvents = new HashSet<string>();

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerIndex = playersCounter;
        playersCounter += 1;
        playerChair = GameObject.FindWithTag(spawnTags[playerIndex]);

        gameObject.layer = LayerMask.NameToLayer(playerLayers[playerIndex]);
        Respawn();
    }

    public void Respawn()
    {
        gameObject.transform.position = playerChair.gameObject.transform.position;
    }

    public void MoveTo(Vector2 v)
    {
        _moveVec = v;
    }
    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + (_moveVec * _moveSpeed * Time.fixedDeltaTime));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter:" + other.name);
        nearbyActionEvents.Add(other.name);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit:" + other.name);
        nearbyActionEvents.Remove(other.name);
    }

    public void Activate()
    {
        if (nearbyActionEvents.Count == 0)
        {
            Debug.Log("No Action Events nearby");
        }
        else if (nearbyActionEvents.Contains("LightSwitch"))
        {
            Debug.Log("Turn off lights");
            ActivateLightSwitch();
        }
    }

    public void ActivateLightSwitch()
    {
        Light2D globalLight = GameObject.FindAnyObjectByType<Light2D>();
        globalLight.color = new Color(0.1f, 0.05f, 0.05f);
    }
}
