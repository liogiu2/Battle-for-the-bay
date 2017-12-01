using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
        if (other.gameObject.tag != "Trigger")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Ship")
        {
            other.gameObject.SendMessage("DamageOnHit");
        }
        
    }
    
}
