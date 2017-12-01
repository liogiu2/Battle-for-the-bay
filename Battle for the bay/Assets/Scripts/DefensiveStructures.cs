using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveStructures : MonoBehaviour {

    public GameObject bulletPrefab;

    float y;

	// Use this for initialization
	void Start () {
        y = transform.rotation.y;
    }
	
	// Update is called once per frame
	void Update () {
        //int layerMask = 1 << 8;
        //RaycastHit hitInfo;
        //if(Physics.SphereCast(transform.position, 5f, transform.forward, out hitInfo, layerMask))
        //{
        //    Fire();
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        //FOLLOW THE TARGET
        transform.LookAt(other.transform);
        Debug.DrawRay(transform.position, transform.forward, Color.green);
    }

    void Fire()
    {
        //bulletPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + transform.forward,
            Quaternion.Euler(0, y, 0));

        //COLOR THE BULLET
        bullet.GetComponent<MeshRenderer>().material.color = Color.red;

        //GIVE INITIAL VELOCITY TO THE BULLET
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;

        Destroy(bullet, 3.0f);
    }
}
