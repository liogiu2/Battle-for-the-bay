﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour
{

    public Transform shipPointer;
    public ShipMovement shipMovement;
    public float minMoveRange;
    public SpriteRenderer cursorSprite;

	private AIRootScript rootScript;

    // Use this for initialization
    void Start()
    {
		rootScript = GetComponent<AIRootScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "Terrain")
                {
                    Debug.DrawLine(ray.origin, hit.point);

                    shipPointer.position = new Vector3(hit.point.x, shipPointer.position.y, hit.point.z);

                    if (Vector3.Distance(shipPointer.position, shipMovement.transform.position) > minMoveRange)
                    {
                        shipMovement.moving = true;
                        cursorSprite.enabled = true;
                    }

                }

				if(hit.collider.tag == "Ship"){
					GameObject Ship =  hit.collider.gameObject;
					Ship.SendMessage("ActivateTarget");
					rootScript.TargetEnemy = Ship;
				}

            }
        }
    }
}