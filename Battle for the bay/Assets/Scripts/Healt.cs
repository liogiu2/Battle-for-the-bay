using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt : MonoBehaviour {

    public float healt;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageOnHit(float DamageOnHit)
    {
        healt -= DamageOnHit;
        if(healt <= 0)
            healt = 100;
    }
}
