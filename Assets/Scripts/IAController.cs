﻿using UnityEngine;
using System.Collections;

public class IAController : MonoBehaviour, IPlayable
{

    public float respawnTime;

    public GameObject hoverPrefab;

    public float turnSensitivity;

    public float distanceLock;

    public float turnLock;

    public float predictPos;

    public float thrustSensitivity;

    private GameObject hover;

    private GameObject target = null;

    private int deathCount = 0;

    private int fragCount = 0;

    // Use this for initialization
    void Start ()
    {
        Invoke("Respawn", 1);
	}

    // Update before physics
    void FixedUpdate()
    {
        if (hover != null)
        {
            ActionUpdate();
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void ActionUpdate()
    {
        HoverController hoverController = hover.GetComponent<HoverController>();
        if (!hoverController.IsAlive())
        {
            hover = null;
            Invoke("Respawn", respawnTime);

            GameObject playerStatus = GameObject.FindGameObjectWithTag("PlayerStatus");
            if (playerStatus != null)
            {
                playerStatus.GetComponent<IPlayable>().IncrementFrag();
            }

            return;
        }
        if (target == null || !target.GetComponent< HoverController >().IsAlive())
        {
            FindTarget();
        }
        if (target != null)
        {
            Vector3 posPredicted = target.transform.position + target.GetComponent<Rigidbody>().velocity * predictPos * Time.fixedDeltaTime;
            Vector3 deltaPos = hoverController.transform.position - posPredicted;
            float distance = Vector3.Distance(hoverController.transform.position, posPredicted);
            float dotForward = Vector3.Dot(hoverController.transform.forward, deltaPos.normalized);
            float dotRight = Vector3.Dot(hoverController.transform.right, deltaPos.normalized);
            float turnPositive = dotForward > 0 ? turnSensitivity : -turnSensitivity;
            float turnNegative = -dotForward > 0 ? turnSensitivity : -turnSensitivity;
            float thust = distance > distanceLock ? thrustSensitivity : 0;
            float strafe = distance < distanceLock ? 1 : 0;
            //Debug.Log(dotForward);
            if (dotForward < 0)
                if (dotRight > 0)
                {
                    hoverController.Move(turnPositive, thust, strafe);
                }
                else
                {
                    hoverController.Move(turnNegative, thust, strafe);
                }
            else
            {
                if (dotRight > 0)
                {
                    hoverController.Move(turnNegative, -thust, strafe);
                }
                else
                {
                    hoverController.Move(turnPositive, -thust, strafe);
                }
            }
            if (dotForward < -turnLock)
            {
                hoverController.ShotWeapon1();
            }
        }
    }

    private void FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            HoverController hc = player.GetComponent<HoverController>();
            if (hc != null && hc.IsAlive())
            {
                target = player;
            }
        }
    }

    public void Respawn()
    {
        GameObject[] respawnList = GameObject.FindGameObjectsWithTag("Respawn Enemy");
        if (respawnList.Length > 0)
        {
            int selIndex = Random.Range(0, respawnList.Length);
            hover = Instantiate(hoverPrefab, respawnList[selIndex].transform.position, respawnList[selIndex].transform.rotation) as GameObject;
            hover.tag = "IA";
        }
    }

    public void IncrementFrag()
    {
        fragCount++;
    }

    public void DecrementFrag()
    {
        fragCount--;
    }

    public void IncrementDeaths()
    {
        deathCount++;
    }

}
