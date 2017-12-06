using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTarget : MonoBehaviour {

	public NavMeshAgent agent;
	private GameObject target;
	private bool shouldAttack;
	// Use this for initialization
	void Start () {
		shouldAttack = false;
		target = GameObject.FindGameObjectWithTag("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(shouldAttack == true){
			//agent.SetDestination(target.transform.position);
			agent.isStopped = true;
		}
		// else agent.isStopped = true;

	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			shouldAttack = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			shouldAttack = false;
		}
	}
}
