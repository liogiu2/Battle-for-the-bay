using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeBehavior : MonoBehaviour {
    public float DamagePerSecond = 20f;
    public float Duration = 3f;

    // Use this for initialization
    void Start () {
        Object.Destroy(gameObject, Duration);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship" || other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("DamageOnHit", DamagePerSecond);
            //Destroy(gameObject);
        }

    }

}
