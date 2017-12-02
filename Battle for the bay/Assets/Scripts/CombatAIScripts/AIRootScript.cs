using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRootScript : MonoBehaviour {

	public enum STATE
	{
		Idle, 
		Combat
	}
	public STATE currentState;
	public List<AIRootScript> detected, enemies;
	// Use this for initialization
	void Start () {
		ChangeState(STATE.Idle);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeState(STATE state){
		currentState = state;
	}
}
