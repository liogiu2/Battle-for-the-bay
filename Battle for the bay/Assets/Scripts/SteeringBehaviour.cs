using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour {

	public float maxSpeed;
	public float maxForce;

	private Rigidbody rb;

	private Vector3 target;
	private Vector3 desired;
	private Vector3 steer;

	// Use this for initialization
	void Start () {
		target = transform.position;
		rb = transform.GetComponent<Rigidbody>();
	}
	
	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit[] hits;
			hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity);

			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit hit = hits[i];
				if(hit.transform.tag == "Ground"){
					target = hit.point;
					break;
				}
			}
		}	
	}

	// Update is called once per frame
    void FixedUpdate()
    {
		if(target != transform.position){
			desired = target - transform.position;
			desired.Normalize();
			desired *= maxSpeed;

			steer = desired - rb.velocity;
			steer = Vector3.ClampMagnitude(steer, maxForce);
			
			// float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
			// rb.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			rb.AddForce(steer, ForceMode.Acceleration);
		}
    }
}
