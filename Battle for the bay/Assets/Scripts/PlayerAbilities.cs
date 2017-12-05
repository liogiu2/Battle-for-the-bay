using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {
    public int aimMode = 0;
    public Transform areaPointer;
    Transform rangePointer;
    Transform linePointer;

    SpriteRenderer areaSprite;
    SpriteRenderer rangeSprite;
    SpriteRenderer lineSprite;

    GameObject lineShotAim;
    public GameObject bulletPrefab;

    private GameObject player;
    private MoveInput moveInput;

    private bool clickDelay = false;

    void Start() {
        lineShotAim = this.gameObject.transform.Find("Aim").gameObject;
        rangePointer = lineShotAim.transform.Find("RangeAbility");
        linePointer = lineShotAim.transform.Find("LineAbility");

        lineSprite = linePointer.GetComponent<SpriteRenderer>();
        rangeSprite = rangePointer.GetComponent<SpriteRenderer>();
        areaSprite = areaPointer.GetComponent<SpriteRenderer>();

        lineShotAim.SetActive(false);
        player = GameObject.Find("Player").gameObject;
        moveInput = player.GetComponent<MoveInput>();
    }

    void Update() {
        // Toggling aim mode
        if (Input.GetKeyDown("q"))
        {
            aimMode = (aimMode == 0) ? 1 : 0;
            lineShotAim.SetActive(true);
            moveInput.enabled = false;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 position = new Vector3(hit.point.x, lineShotAim.transform.position.y, hit.point.z);
                lineShotAim.transform.LookAt(position);
                switch (aimMode)
                {
                    case 1:
                        lineSprite.enabled = true;
                        lineShotAim.transform.LookAt(position);
                        if (Input.GetMouseButtonDown(0))
                        {
                            FireTowards(lineShotAim.transform.rotation);
                            resetSprites();
                            clickDelay = true;
                        }
                        break;
                    case 2:
                        // Range 
                        rangeSprite.enabled = true;
                        lineShotAim.transform.LookAt(position);
                        if (Input.GetMouseButtonDown(0))
                        {
                            FireTowards(lineShotAim.transform.rotation);
                            resetSprites();
                            clickDelay = true;
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
            if (clickDelay && Input.GetMouseButtonDown(0))
            {
                clickDelay = false;
                moveInput.enabled = true;
            }
            resetSprites();
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

    private void resetSprites()
    {
        aimMode = 0;
        areaSprite.enabled = false;
        rangeSprite.enabled = false;
        lineSprite.enabled = false;
        lineShotAim.SetActive(false);
    }
}