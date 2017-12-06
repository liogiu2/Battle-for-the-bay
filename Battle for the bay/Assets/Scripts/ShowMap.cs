using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour {

	bool mapOpen;

	public GameObject minimap;
	public GameObject map;
	// Use this for initialization
	void Start () {
		mapOpen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M)  && mapOpen == false)
		{
			mapOpen = true;
			minimap.SetActive(false);
			map.SetActive(true);
		}
		else if(Input.GetKeyDown(KeyCode.M)  && mapOpen == true)
		{
			mapOpen = false;
			minimap.SetActive(true);
			map.SetActive(false);
		}
	}
}
