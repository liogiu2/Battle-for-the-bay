using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResources : MonoBehaviour {

	public int Money;
	public int MaxAmountOfMoneyInShip;
	public int MoneyForTreasure;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Money > MaxAmountOfMoneyInShip){
			Money = MaxAmountOfMoneyInShip;
		}
	}

	public void AddMoney(int money){
		Money += money;
	}

	public void GetTreasure(){
		Money += MoneyForTreasure;
		Debug.Log("Got Treasure");
	}

	public void inIsland(){
		
	}

	public void OutIsland(){
		
	}
}
