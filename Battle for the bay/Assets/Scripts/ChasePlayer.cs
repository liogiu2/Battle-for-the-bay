using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour {

	public NavMeshAgent agent;
	private GameObject target;
	private bool shouldChase;
	// Use this for initialization
	void Start () {
		shouldChase = false;
		target = GameObject.FindGameObjectWithTag("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(shouldChase == true){
			agent.SetDestination(target.transform.position);
			agent.isStopped = false;
		}
		// else agent.isStopped = true;

	}

	void OnTriggerEnter(Collider other){
		Debug.Log("OnTriggerEnter");
		if(other.tag == "Player"){
			shouldChase = true;
		}
	}

	void OnTriggerExit(Collider other){
		Debug.Log("OnTriggerExit");
		if(other.tag == "Player"){
			shouldChase = false;
		}
	}
}
