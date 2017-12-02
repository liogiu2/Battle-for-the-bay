﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementStop : MonoBehaviour {

	public float rotoSpeed;
	public ShipMovement shipMovement;
	public SpriteRenderer cursorSprite; 

	// Use this for initialization
	void Start () {
		cursorSprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * rotoSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		Debug.Log("Sto dentro");
		if(other.tag == shipMovement.tag){
			if(shipMovement.moving == true){
				shipMovement.moving = false;
				cursorSprite.enabled = false;
			}
		}
	}
}
