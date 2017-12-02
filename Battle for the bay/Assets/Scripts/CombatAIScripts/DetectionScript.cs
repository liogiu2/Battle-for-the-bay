using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionScript : MonoBehaviour
{

    public AIRootScript rootScript;
    // Use this for initialization
    void Start()
    {
        rootScript = gameObject.GetComponentInParent<AIRootScript>();
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
            if (detectedObject == null)
            {
                rootScript.detected.Remove(detectedObject);
            }

            if (detectedObject.tag == "ship")
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

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Terrain" && collider.tag != "obstacle" && collider.tag != "Untagged")
        {
            rootScript.detected.Add(collider.GetComponent<AIRootScript>());
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        rootScript.detected.Remove(collider.GetComponent<AIRootScript>());
    }
}
