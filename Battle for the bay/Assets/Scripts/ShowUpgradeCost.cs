using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUpgradeCost : MonoBehaviour {

    private GameObject cost;

	// Use this for initialization
	void Start () {
        cost = gameObject.transform.Find("Cost").transform.gameObject;
        cost.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseEnter()
    {
        cost.SetActive(true);
        Debug.Log("Mouse is over GameObject.");
    }

    public void OnMouseExit()
    {
        cost.SetActive(false);
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
