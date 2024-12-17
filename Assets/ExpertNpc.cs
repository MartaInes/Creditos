using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class ExpertNpc : MonoBehaviour
{
    private GameObject lightNpc;
    private GameObject fireAlarmNpc;
    private GameObject fireNpc;
    private GameObject candyNpc;
    private GameObject noButtonNpc;

    public NpcDuties inChargeOf;
    private Vector3 initialPosition;
    private NavMeshAgent agent;
    private NavMeshObstacle agentAsObstacle;
    public enum NpcDuties {
        LightSwitch,
        FireExtinguisher,
        CandyTable,
        NoButton,
        FireAlarm,
    }

    private Light2D light2D;

    private GameObject lightSwitch;
    private GameObject fireExtinguisher;
    private GameObject candyTable;
    private GameObject noButton;
    private GameObject fireAlarm;
    private GameObject trashcan;
    private GameObject fire;
    private Light2D fireLight;
    private Vector3 fireExtinguisherInitialPosition;
    private GameObject fireExtinguisherParent;

    void Start()
    {
        fireNpc = GameObject.FindWithTag("FireNpc").gameObject;
        lightNpc = GameObject.FindWithTag("LightNpc").gameObject;
        fireAlarmNpc = GameObject.FindWithTag("FireAlarmNpc").gameObject;
        candyNpc = GameObject.FindWithTag("CandyNpc").gameObject;
        noButtonNpc = GameObject.FindWithTag("NoButtonNpc").gameObject;

        light2D = GameObject.FindAnyObjectByType<Light2D>();
        initialPosition = transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agentAsObstacle = gameObject.GetComponent<NavMeshObstacle>();
        lightSwitch = GameObject.FindWithTag("LightSwitch");
        fireExtinguisher = GameObject.FindWithTag("FireExtinguisher");
        candyTable = GameObject.FindWithTag("CandyTable");
        noButton = GameObject.FindWithTag("NoButton");
        fireAlarm = GameObject.FindWithTag("FireAlarm");
        trashcan = GameObject.FindWithTag("Trashcan");
        fire = trashcan.transform.Find("Fire").gameObject;
        fireLight = fire.GetComponentInChildren<Light2D>();

        fireExtinguisherInitialPosition = fireExtinguisher.transform.position;
        fireExtinguisherParent = fireExtinguisher.transform.parent.gameObject;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            player.Respawn();
        }
    }

    void Update()
    {
        var vision = agent.transform.Find("VisionRotationCenter");
        var nextDirection = agent.velocity.normalized;
        float visionRotation;
        switch(nextDirection.y)
        {
            case > 0: visionRotation = 180 + nextDirection.x * 90; break;
            default:  visionRotation = 0   + nextDirection.x * -90; break;
        }
        vision.transform.localEulerAngles = new Vector3(0,visionRotation, 0);
        switch (inChargeOf) 
        {
            case NpcDuties.LightSwitch:
                if(lightSwitch.tag.StartsWith("Team") && light2D.intensity == 1)
                {
                    agent.destination = lightSwitch.transform.position;
                    lightSwitch.transform.Find("SwitchOffSound").GetComponent<AudioSource>().Play();
                    light2D.color = new Color(0, 0, 0.001f, 1);
                    light2D.intensity = 100;
                }
                else if (lightSwitch.tag == "Untagged" && light2D.intensity == 100)
                {
                    agent.destination = initialPosition;
                    lightSwitch.transform.Find("SwitchOnSound").GetComponent<AudioSource>().Play();
                    light2D.color = new Color(1, 1, 1, 1);
                    light2D.intensity = 1;
                }
                else if (lightSwitch.tag.StartsWith("Team") && light2D.intensity == 100)
                {
                    if (gameObject.GetComponentInChildren<Collider2D>().IsTouching(lightSwitch.GetComponent<Collider2D>()))
                    {
                        lightSwitch.tag = "Untagged";
                    }
                }
                else
                {
                    agent.destination = initialPosition;
                }
                break;
                
            case NpcDuties.FireExtinguisher:
                if (trashcan.tag.StartsWith("Team"))
                {
                    if (!fireAlarm.tag.StartsWith("Team"))
                    {
                        fireAlarm.tag = "TeamNpc";
                        fireAlarm.transform.Find("AlarmSound").GetComponent<AudioSource>().Play();
                    }
                    fireLight.pointLightOuterRadius = (Time.realtimeSinceStartup % 7) + 2;
                }
                if (trashcan.tag.StartsWith("Team") && !fire.activeInHierarchy)
                {
                    fire.SetActive(true);
                    fire.transform.Find("FireSound").GetComponent<AudioSource>().Play();
                    fireAlarm.tag = "TeamNpc";
                    fireAlarm.transform.Find("AlarmSound").GetComponent<AudioSource>().Play();
                    agent.destination = fireExtinguisher.transform.position;
                }
                else if(trashcan.tag.StartsWith("Team") && fireExtinguisher.tag != "TeamNpc" && gameObject.GetComponentInChildren<Collider2D>().IsTouching(fireExtinguisher.GetComponent<Collider2D>())) {
                    fireExtinguisher.tag = "TeamNpc";
                    fireExtinguisher.transform.SetParent(gameObject.transform, true);

                    /*foreach (var item in fireExtinguisher.GetComponentsInChildren<Collider2D>(true))
                    {
                        item.enabled = false;
                    }*/
                }
                else if (trashcan.tag.StartsWith("Team") && fireExtinguisher.tag == "TeamNpc")
                {
                    agent.destination = trashcan.transform.position;
                    if (gameObject.GetComponentInChildren<Collider2D>().IsTouching(trashcan.GetComponent<Collider2D>()))
                    {
                        fireExtinguisher.transform.Find("ExtinguisherSound").GetComponent<AudioSource>().Play();
                        fire.SetActive(false);
                        fire.transform.Find("FireSound").GetComponent<AudioSource>().Stop();
                        trashcan.tag = "Untagged";
                    }
                } else if(!trashcan.tag.StartsWith("Team") && fireExtinguisher.CompareTag("TeamNpc"))
                {
                    agent.destination = fireExtinguisherInitialPosition;
                    if (Math.Abs(Vector3.Distance(fireExtinguisherInitialPosition, agent.transform.position)) < 1.7)
                    {
                        fireExtinguisher.tag = "Untagged";
                        fireExtinguisher.transform.SetParent(fireExtinguisherParent.transform, true);
                        /*foreach (var collider in fireExtinguisher.GetComponentsInChildren<Collider2D>(true))
                        {
                            collider.enabled = true;
                        }*/
                    }
                }
                else if(trashcan.tag == "Untagged" && fireExtinguisher.tag == "Untagged")
                {
                    agent.destination = initialPosition;
                }
                break;

            case NpcDuties.CandyTable:
                if (candyTable.tag.StartsWith("Team"))
                {
                    agent.destination = candyTable.transform.position;
                    if (gameObject.GetComponentInChildren<Collider2D>().IsTouching(candyTable.GetComponent<Collider2D>()))
                    {
                        candyTable.tag = "Untagged";
                    }
                }
                else
                {
                    agent.destination = initialPosition;
                }
                break;

            case NpcDuties.NoButton:
                if (noButton.tag.StartsWith("Team") && noButton.tag != "TeamNpc")
                {
                    agent.destination = noButton.transform.position;
                }
                break;

            case NpcDuties.FireAlarm:
                if (fireAlarm.tag.StartsWith("Team")) {
                    if (light2D.intensity == 1)
                    {
                        light2D.color = new Color(1f, (Time.realtimeSinceStartup % 3) * 0.25f, (Time.realtimeSinceStartup % 3) * 0.25f, 1);
                    }
                    else if (light2D.intensity == 100)
                    {
                        light2D.color = new Color(0.001f, (Time.realtimeSinceStartup % 3) * 0.00025f, (Time.realtimeSinceStartup % 3) * 0.00025f, 1);
                    }
                    if (trashcan.tag == "Untagged" && fireExtinguisher.tag == "Untagged" 
                     && Math.Abs(Vector3.Distance(fireNpc.transform.position, agent.transform.position)) < 4
                     && Math.Abs(Vector3.Distance(initialPosition, agent.transform.position)) < 1)
                    {
                        agent.destination = fireAlarm.transform.position;
                    }
                    if (gameObject.GetComponentInChildren<Collider2D>().IsTouching(fireAlarm.GetComponent<Collider2D>()))
                    {
                        fireAlarm.tag = "Untagged";
                        fireAlarm.transform.Find("AlarmSound").GetComponent<AudioSource>().Stop();
                        agent.destination = initialPosition;
                        light2D.color = new Color(1, 1, 1, 1);
                        light2D.intensity = 1;
                    }
                }
                else
                {
                    agent.destination = initialPosition;
                }
                break;
        }
        //npcAsObstacle();
    }

    float haveBeenWaiting = 0;
    private void npcAsObstacle()
    {
        haveBeenWaiting += Time.deltaTime;
        if (haveBeenWaiting < 0 && !agent.enabled)
        {
            agent.enabled = true;
        }
        else if (agent.velocity == Vector3.zero && agent.enabled && haveBeenWaiting > 1)
        {
            agent.enabled = false;
        }
        else if (agent.velocity == Vector3.zero && !agent.enabled && haveBeenWaiting > 1)
        {
            agentAsObstacle.enabled = true;
        } else if (haveBeenWaiting > 2)
        {
            haveBeenWaiting = -1;
            agentAsObstacle.enabled = false;
        }
    }
}
