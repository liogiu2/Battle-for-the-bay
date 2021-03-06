﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{

    public AIRootScript rootScript;
    private bool _exitUpdate = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //clear and update che enemy list
        if (rootScript.enemies.Count > 0)
        {
            rootScript.enemies.Clear();
        }
        rootScript.detected.RemoveAll(item => item == null);

        foreach (GameObject detectedObject in rootScript.detected)
        {
            if (_exitUpdate)
            {
                _exitUpdate = false;
                return;
            }
            if (!_exitUpdate && detectedObject == null)
            {
                rootScript.detected.Remove(detectedObject);
            }

            if (!_exitUpdate && detectedObject.tag == "Ship")
            {
                rootScript.enemies.Add(detectedObject);
            }
        }

        //Change the state
        if (rootScript.enemies.Count > 0)
        {
            rootScript.ChangeState(AIRootScript.STATE.Combat);
        }
        else
        {
            rootScript.ChangeState(AIRootScript.STATE.Idle);
        }
    }

    void OnDeleteShip()
    {
        _exitUpdate = true;
    }

    void OnEnable()
    {
        UpdateEnemyList.OnDeleteShip += OnDeleteShip;
    }


    void OnDisable()
    {
        UpdateEnemyList.OnDeleteShip -= OnDeleteShip;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Treasure")
        {
            transform.parent.GetComponent<CollectResources>().GetTreasure();
            Destroy(collider.gameObject);
        }
        if (collider.tag == "PlayerBase")
        {
            collider.gameObject.transform.parent.GetComponent<ResourcesOnIsland>().GetMoneyFromPlayer();
        }
        if (collider.tag != "Terrain" && collider.tag != "Untagged")
        {
            rootScript.detected.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        rootScript.detected.Remove(collider.gameObject);
    }
}
