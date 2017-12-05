using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilities : MonoBehaviour {

	public GameObject lineShotAim;
    public GameObject bulletPrefab;
    private GameObject player;
    private MoveInput moveInput;

	// Use this for initialization
	void Start () {
		lineShotAim.SetActive(false);
        player = GameObject.Find("Player").gameObject;
        moveInput = player.GetComponent<MoveInput>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q) && lineShotAim.activeSelf == false){
			lineShotAim.SetActive(true);
            moveInput.enabled = false;
		} else if(Input.GetKeyDown(KeyCode.Q) && lineShotAim.activeSelf){
			lineShotAim.SetActive(false);
            moveInput.enabled = true;
		}

        if(lineShotAim.activeSelf == true){

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Vector3 position = new Vector3(hit.point.x, lineShotAim.transform.position.y, hit.point.z);
                lineShotAim.transform.LookAt(position);
                if(Input.GetMouseButtonDown(0)){
                    Debug.Log(lineShotAim.transform.rotation);
                    FireTowards(lineShotAim.transform.rotation);
                }
            }
        }
	}


    private void FireTowards(Quaternion Target)
    {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            //CREATE THE BULLET
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletPosition,
                Target);
            //Destroy(bullet, 1.0f);

            bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
            //COLOR THE BULLET
            bullet.GetComponent<MeshRenderer>().material.color = Color.red;

            //GIVE INITIAL VELOCITY TO THE BULLET
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 12, ForceMode.Impulse);

    }
}
