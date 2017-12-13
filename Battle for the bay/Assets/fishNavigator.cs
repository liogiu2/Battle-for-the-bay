using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fishNavigator : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(transform.position, out navHit, 0.1f, -1) == false) avoid();
        transform.Translate(Vector3.forward * Time.deltaTime);
        if(Random.value > 0.99) {
            steer();
        }
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }

    void steer() {
        Vector3 randomDirection = new Vector3(0, Random.Range(-359, 359), 0);
        transform.Rotate(randomDirection);
    }

    void avoid() {
        Debug.Log("Avoiding");
        transform.eulerAngles += 180f * Vector3.up;
    }
}
