using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LineShotProjectile : MonoBehaviour
{

    public string GeneratedTag;
    public float DamageOnHit = 20f;
    public float lineShotProjectileRange;

    private Vector3 startingPosition;
    private float travelDistance;
    // Use this for initialization
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        travelDistance = Vector3.Distance(transform.position, startingPosition);
        if (travelDistance >= lineShotProjectileRange) Destroy(gameObject);

        //edit: to draw ray also//
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 10, Color.green);
        //end edit//
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0) * 10, out hit, 10))
        {
            if (hit.collider.tag == "Terrain")
            {
                NavMeshHit navHit;
				// Debug.Log("Hit point: " + hit.point);
				// Debug.Log("Bullet transform: " + transform.position);
                // Debug.Log("this point is walkable? " + NavMesh.SamplePosition(hit.point, out navHit, 0.1f, -1));
				if(NavMesh.SamplePosition(hit.point, out navHit, 0.1f, -1) == false ) Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GeneratedTag)
        {
            return;
        }
        if (other.gameObject.tag == "EnemyMinion")
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        if (other.gameObject.tag == "NeutralNPC")
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        if (other.gameObject.tag == "destructable")
        { 
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
    }
}
