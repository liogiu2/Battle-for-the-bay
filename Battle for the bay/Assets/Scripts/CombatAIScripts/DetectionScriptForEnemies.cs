using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScriptForEnemies : MonoBehaviour
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

        foreach (AIRootScript detectedObject in rootScript.detected)
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
            if (!_exitUpdate && detectedObject.tag == "Player")
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
        if (collider.tag != "Terrain" && collider.tag != "Untagged" && collider.tag!="Ship")
        {
            rootScript.detected.Add(collider.gameObject.GetComponent<AIRootScript>());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        rootScript.detected.Remove(collider.GetComponent<AIRootScript>());
    }
}
