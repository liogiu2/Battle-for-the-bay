using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour {

	public AI ai;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player entered the vision range");
			ai.stateMachine.ChangeState(ChaseState.Instance);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			Debug.Log("Player exited the vision range");
			ai.stateMachine.ChangeState(WanderState.Instance);
		}
	}
}
