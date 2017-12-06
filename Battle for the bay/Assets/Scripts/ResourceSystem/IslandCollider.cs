using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandCollider : MonoBehaviour {

	public ResourcesOnIsland resourcesOnIsland;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
			collider.gameObject.SendMessage("inIsland");
			resourcesOnIsland.GetMoneyFromPlayer();
			Debug.Log("Player inside island");
        }
    }
	void OnTriggerExit(Collider collider)
    {
		if (collider.tag == "Player")
        {
			collider.gameObject.SendMessage("OutIsland");
			Debug.Log("Player outside island");
			
        }
    }
}
