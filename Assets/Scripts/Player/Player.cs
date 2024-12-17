using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private static int playersCounter = 0;
    private static string[] spawnTags = { "Spawn1", "Spawn2", "Spawn3", "Spawn4", "Spawn5", "Spawn6" };

    private float _moveSpeed = 10f;
    private Vector2 _moveVec = Vector2.zero;

    private int playerIndex;
    private GameObject playerChair;
    private Rigidbody2D rigidBody;
    private GameObject voices;

    private HashSet<GameObject> nearbyActionEvents = new HashSet<GameObject>();

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        voices = gameObject.transform.Find("Voices").gameObject;
        playerIndex = playersCounter;
        playersCounter += 1;
        playerChair = transform.parent.Find("Chair").gameObject;

        playerChair.transform.position = GameObject.FindWithTag(spawnTags[playerIndex]).transform.position;
        Respawn();
    }

    public void Respawn()
    {
        playerChair.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        foreach (Collider2D collider in playerChair.GetComponentsInChildren<Collider2D>(true))
        {
            if (!collider.isTrigger)
            {
                collider.enabled = false;
            }
        }
        gameObject.transform.position = playerChair.transform.position + new Vector3(0,2.77f,0);
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
        nearbyActionEvents.Add(other.gameObject);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "ChairCollider")
        {
            playerChair.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            foreach (Collider2D collider in playerChair.GetComponentsInChildren<Collider2D>(true))
            {
                collider.enabled = true;
            }
        } else
        {
            nearbyActionEvents.Remove(other.gameObject);
        }
    }

    public void Activate(string tag)
    {
        if (nearbyActionEvents.Count == 0)
        {
            Debug.Log("No Action Events nearby");
        }
        List<GameObject> actionEvents = new List<GameObject>(nearbyActionEvents);
        for (int i = 0; i < actionEvents.Count; i++)
        {
            GameObject actionEvent = actionEvents[i];
            switch (actionEvent.name)
            {
                case "Monitor": actionEvent.tag = tag; actionEvent.SetActive(false); break;
                case "Drawings": actionEvent.tag = tag; actionEvent.SetActive(false); break;
                case "Piano": actionEvent.tag = tag; actionEvent.SetActive(false); break;
                case "Scripts": actionEvent.tag = tag; actionEvent.SetActive(false); break;
                case "Keyboard": actionEvent.tag = tag; actionEvent.SetActive(false); break;
                case "Trashcan": actionEvent.tag = tag; break;
                case "LightSwitch": actionEvent.tag = actionEvent.tag.StartsWith("Team") ? "Untagged" : tag; break;
                case "CandyTable": actionEvent.tag = tag; break;
                case "NoButton": actionEvent.tag = tag; break;
                default: continue;
            }
            voices.transform.GetChild(Random.Range(0,voices.transform.childCount)).GetComponent<AudioSource>().Play();
        }
    }
}
