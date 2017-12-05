using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    int aimMode = 0;
    public Transform areaPointer;
    public Transform rangePointer;
    SpriteRenderer areaSprite;
    SpriteRenderer rangeSprite;

    public GameObject lineShotAim;
    public GameObject bulletPrefab;
    private GameObject player;
    private MoveInput moveInput;

    void Start() {
        areaSprite = areaPointer.GetComponent<SpriteRenderer>();
        rangeSprite = rangePointer.GetComponent<SpriteRenderer>();

        lineShotAim.SetActive(false);
        player = GameObject.Find("Player").gameObject;
        moveInput = player.GetComponent<MoveInput>();
    }

    void Update() {
        // Toggling aim mode
        if (Input.GetKeyDown("q"))
        {
            aimMode = (aimMode == 0) ? 1 : 0;
        }
        else if (Input.GetKeyDown("w"))
        {
            aimMode = (aimMode == 0) ? 2 : 0;
            lineShotAim.SetActive(true);
            moveInput.enabled = false;
        }
        else if (Input.GetKeyDown("e"))
        {
            aimMode = (aimMode == 0) ? 3 : 0;
        }

        if (aimMode != 0) {
            Debug.Log("in aiming mode");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                switch (aimMode)
                {
                    case 1:

                        break;
                    case 2:
                        // Range 
                        rangeSprite.enabled = true;

                        Debug.DrawLine(ray.origin, hit.point);
                        Vector3 position = new Vector3(hit.point.x, lineShotAim.transform.position.y, hit.point.z);
                        lineShotAim.transform.LookAt(position);
                        if (Input.GetMouseButtonDown(0))
                        {
                            FireTowards(position);
                        }
                        
                        break;
                    case 3:
                        // Splash Area mode
                        areaPointer.position = new Vector3(hit.point.x, areaPointer.position.y, hit.point.z);
                        areaSprite.enabled = true;
                        break;
                    default:
                        break;
                }
            }
        } else
        {
            areaSprite.enabled = false;
            rangeSprite.enabled = false;
            lineShotAim.SetActive(false);
            moveInput.enabled = true;

        }
    }

    private void FireTowards(Vector3 Target)
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletPosition,
            //Quaternion.Euler(-10, transform.rotation.y - 90, 0));
            Quaternion.identity);
        Destroy(bullet, 1.0f);

        bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
        //COLOR THE BULLET
        bullet.GetComponent<MeshRenderer>().material.color = Color.black;

        //GIVE INITIAL VELOCITY TO THE BULLET
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
        bullet.GetComponent<Rigidbody>().velocity = (Target - bulletPosition).normalized * 10f;

    }
}