using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt : MonoBehaviour {

    public int healt;

	// Use this for initialization
	void Start () {
        healt = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageOnHit()
    {
        healt -= 20;
        if(healt <= 0)
            Destroy(gameObject);
    }
}
