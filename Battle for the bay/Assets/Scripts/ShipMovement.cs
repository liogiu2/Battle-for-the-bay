using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMovement : MonoBehaviour
{

    public bool moving;
    public NavMeshAgent agent;
    public Transform pointer;
    public GameObject movingEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            agent.SetDestination(pointer.position);
            Invoke("EnableMovingEffect", 0.2f);
            agent.isStopped = false;
        }
        else
        {
            movingEffect.SetActive(false);
        }
    }

	void EnableMovingEffect(){
		movingEffect.SetActive(true);
	}
}
