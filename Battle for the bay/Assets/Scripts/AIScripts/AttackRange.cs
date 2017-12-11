using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

	public AIRootMovement ai;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player" && ai.gameObject.tag == "EnemyMinion")
        {
            ai.ChangeState(AIRootMovement.STATE.Idle);
        }
        if (other.gameObject.tag == "Enemy" && ai.gameObject.tag == "PlayerMinion")
        {
            ai.ChangeState(AIRootMovement.STATE.Idle);
        }
	}

	void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player" && ai.gameObject.tag == "EnemyMinion")
        {
            ai.ChangeState(AIRootMovement.STATE.Chase);
        }
        if (other.gameObject.tag == "Enemy" && ai.gameObject.tag == "PlayerMinion")
        {
            ai.ChangeState(AIRootMovement.STATE.Chase);
        }
	}
}
