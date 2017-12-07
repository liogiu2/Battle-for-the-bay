using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour {

	public AIRootMovement ai;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			ai.ChangeState(AIRootMovement.STATE.Chase);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player")
			ai.ChangeState(AIRootMovement.STATE.Wander);
	}
}
