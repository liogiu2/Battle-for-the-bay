using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCamera : MonoBehaviour {
    
    public Transform target;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Health otherScript = GetComponent<Health>();
        transform.rotation = Quaternion.Euler(35, 0, 0);
    }
}
