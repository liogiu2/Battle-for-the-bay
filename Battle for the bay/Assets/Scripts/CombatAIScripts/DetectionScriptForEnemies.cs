using System.Collections;
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
        
        foreach (AIRootScript detectedObject in rootScript.detected)
        {
            if (detectedObject.tag == TagCostants.Player && _currentTag == TagCostants.EnemyMinion)
            {
                rootScript.enemies.Add(detectedObject);
            }
            if (detectedObject.tag == TagCostants.PlayerMinion && _currentTag == TagCostants.EnemyMinion)
            {
                rootScript.enemies.Add(detectedObject);
            }
            if (detectedObject.tag == TagCostants.EnemyMinion && _currentTag == TagCostants.PlayerMinion)
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
    // void OnDeleteShip()
    // {
    //     _exitUpdate = true;
    // }

    // void OnEnable()
    // {
    //     UpdateEnemyList.OnDeleteShip += OnDeleteShip;
    // }


    // void OnDisable()
    // {
    //     UpdateEnemyList.OnDeleteShip -= OnDeleteShip;
    // }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == TagCostants.Player && _currentTag == TagCostants.EnemyMinion)
        {
            rootScript.detected.Add(collider.gameObject.GetComponent<AIRootScript>());
        }

        if (collider.tag == TagCostants.EnemyMinion && _currentTag == TagCostants.PlayerMinion)
        {
            rootScript.detected.Add(collider.gameObject.GetComponent<AIRootScript>());
        }

        if (collider.tag == TagCostants.PlayerMinion && _currentTag == TagCostants.EnemyMinion)
        {
            rootScript.detected.Add(collider.gameObject.GetComponent<AIRootScript>());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == TagCostants.Player && _currentTag == TagCostants.EnemyMinion)
        {
            rootScript.detected.Remove(collider.GetComponent<AIRootScript>());
            rootScript.StopCorutineFire();
        }

        if (collider.tag == TagCostants.EnemyMinion && _currentTag == TagCostants.PlayerMinion)
        {
            rootScript.detected.Add(collider.gameObject.GetComponent<AIRootScript>()); 
            rootScript.StopCorutineFire();                       
        }

        if (collider.tag == TagCostants.PlayerMinion && _currentTag == TagCostants.EnemyMinion)
        {
            rootScript.detected.Add(collider.gameObject.GetComponent<AIRootScript>());
            rootScript.StopCorutineFire();
        }
    }
}
