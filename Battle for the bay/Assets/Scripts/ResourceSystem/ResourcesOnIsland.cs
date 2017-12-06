using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesOnIsland : MonoBehaviour {

	public int MoneyOnIsland = 0;
	private GameObject player;
	private CollectResources PlayerResources;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").gameObject;
		PlayerResources = player.GetComponent<CollectResources>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetMoneyFromPlayer(){
		MoneyOnIsland += PlayerResources.Money;
		PlayerResources.Money = 0;
	}
}
