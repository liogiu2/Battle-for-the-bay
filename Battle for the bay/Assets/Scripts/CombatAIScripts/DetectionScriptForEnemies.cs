﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScriptForEnemies : MonoBehaviour
{

    public AIRootScript rootScript;
    private bool _exitUpdate = false;
    private string _currentTag;

    // Use this for initialization
    void Start()
    {
        _currentTag = rootScript.tag;
    }

    // Update is called once per frame
    void Update()
    {
        //clear and update che enemy list
        if (rootScript.enemies.Count > 0)
        {
            rootScript.enemies.Clear();
        }

        rootScript.detected.RemoveAll(obj => obj == null);

        foreach (GameObject detectedObject in rootScript.detected)
        {
            if (CheckWho(detectedObject))
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


    void OnTriggerEnter(Collider collider)
    {
        if (CheckWho(collider.gameObject))
        {
            rootScript.detected.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (rootScript.detected.Contains(collider.gameObject))
        {
            rootScript.detected.Remove(collider.gameObject);
            rootScript.StopCorutineFire();
        }
    }

    private bool CheckWho(GameObject collider)
    {
        bool _check = false;
        if (collider.tag == TagCostants.Player && _currentTag == TagCostants.EnemyMinion)
        {
            _check = true;
        }

        else if (collider.tag == TagCostants.EnemyMinion && _currentTag == TagCostants.PlayerMinion)
        {
            _check = true;
        }

        else if (collider.tag == TagCostants.PlayerMinion && _currentTag == TagCostants.EnemyMinion)
        {
            _check = true;
        }

        else if (collider.tag == TagCostants.PlayerTower && _currentTag == TagCostants.EnemyMinion)
        {
            _check = true;
        }

        else if (collider.tag == TagCostants.EnemyTower && _currentTag == TagCostants.PlayerMinion)
        {
            _check = true;
        }

        else if (collider.tag == TagCostants.PlayerBase && _currentTag == TagCostants.EnemyMinion)
        {
            _check = true;
        }

        else if (collider.tag == TagCostants.EnemyBase && _currentTag == TagCostants.PlayerMinion)
        {
            _check = true;
        }
        return _check;
    }
}
