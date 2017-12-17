using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMovement : MonoBehaviour {

	public bool moving;
	public NavMeshAgent agent;
	public Transform pointer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(moving)
		{
			agent.SetDestination(pointer.position);
			agent.isStopped = false;
		}
	}
}
