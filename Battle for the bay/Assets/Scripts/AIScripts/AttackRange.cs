using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

	public AI ai;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player entered the attack range");
			ai.stateMachine.ChangeState(IdleState.Instance);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player exited the attack range");
			ai.stateMachine.ChangeState(ChaseState.Instance);
		}
	}
}
