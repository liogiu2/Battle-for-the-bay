using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public int aimMode = 0;
    public Transform areaPointer;
    public float areaPointerRange = 5f;
    Transform rangePointer;
    Transform linePointer;
    SpriteRenderer areaSprite;
    SpriteRenderer rangeSprite;
    SpriteRenderer lineSprite;
    GameObject lineShotAim;
    public GameObject linePrefab;
    public GameObject rangePrefab;
    public GameObject areaPrefab;
    private GameObject player;
    private MoveInput moveInput;

    private bool clickDelay = false;
    // Hardcoded, not good.. we need to modularize the abilities
    public float cooldownQ;
    public float cooldownW;
    public float cooldownE;

    public float cooldownQTimer;
    public float cooldownWTimer;
    public float cooldownETimer;

    void Start()
    {
        cooldownQTimer = 0f;
        cooldownWTimer = 0f;
        cooldownETimer = 0f;

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

    void Update()
    {
        // Toggling aim mode
        if (Input.GetKeyDown("q") && cooldownQTimer == 0f)
        {
            aimMode = (aimMode == 0) ? 1 : 0;
            lineShotAim.SetActive(true);
            moveInput.enabled = !moveInput.enabled;
        }
        else if (Input.GetKeyDown("w") && cooldownWTimer == 0f)
        {
            aimMode = (aimMode == 0) ? 2 : 0;
            lineShotAim.SetActive(true);
            moveInput.enabled = !moveInput.enabled;
        }
        else if (Input.GetKeyDown("e") && cooldownETimer == 0f)
        {
            aimMode = (aimMode == 0) ? 3 : 0;
        }

        if (clickDelay && Input.GetMouseButtonUp(0))
        {
            clickDelay = false;
            moveInput.enabled = true;
        }

        if (aimMode != 0)
        {
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
                            cooldownQTimer = cooldownQ; 
                            fireTowards(lineShotAim.transform.rotation);
                            resetSprites();
                            clickDelay = true;
                        }
                        break;
                    case 2:
                        // Range 
                        cooldownWTimer = cooldownW;
                        rangeSprite.enabled = true;
                        lineShotAim.transform.LookAt(position);

                        if (Input.GetMouseButtonDown(0))
                        {
                            MultiFire(lineShotAim.transform.rotation);
                            resetSprites();
                            clickDelay = true;
                        }
                        break;
                    case 3:
                        cooldownETimer = cooldownE;
                        // Splash Area mode
                        areaSprite.enabled = true;

                        Vector3 centerPosition = transform.localPosition; //center of *black circle*
                        float distance = Vector3.Distance(hit.point, centerPosition); //distance from ~green object~ to *black circle*

                        if (distance > areaPointerRange) //If the distance is less than the radius, it is already within the circle.
                        {
                            Vector3 fromOriginToObject = hit.point - centerPosition; //~GreenPosition~ - *BlackCenter*
                            fromOriginToObject *= areaPointerRange / distance; //Multiply by radius //Divide by Distance
                            areaPointer.position = centerPosition + fromOriginToObject; //*BlackCenter* + all that Math
                        }
                        else
                        {
                            areaPointer.position = new Vector3(hit.point.x, areaPointer.position.y, hit.point.z);
                        }

                        if (Input.GetMouseButtonDown(0))//&& dist < areaPointerRange)
                        {
                            areaAttack(areaPointer.position, lineShotAim.transform.rotation);
                            resetSprites();
                            clickDelay = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            resetSprites();
        }

        // update all the cooldowns
        cooldownQTimer = (cooldownQTimer - Time.deltaTime) > 0f ? (cooldownQTimer - Time.deltaTime) : 0f;   
        cooldownWTimer = (cooldownWTimer - Time.deltaTime) > 0f ? (cooldownWTimer - Time.deltaTime) : 0f;
        cooldownETimer = (cooldownETimer - Time.deltaTime) > 0f ? (cooldownETimer - Time.deltaTime) : 0f;
    }

    private void MultiFire(Quaternion Target)
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //CREATE THE BULLET
        List<GameObject> bullets = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            bullets.Add((GameObject)Instantiate(rangePrefab, bulletPosition, Target));
            bullets[i].GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
        }

        Vector3 dir = bullets[0].transform.forward;
        int iterator = 0;
        for (int j = -10; j <= 10; j += 5)
        {
            bullets[iterator].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, j, 0) * dir * 12, ForceMode.Impulse);
            iterator++;
        }
    }

    private void fireTowards(Quaternion Target)
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            linePrefab,
            bulletPosition,
            Target);
        //Destroy(bullet, 1.0f);

        bullet.GetComponent<LineShotProjectile>().GeneratedTag = gameObject.tag;
        //COLOR THE BULLET
        // bullet.GetComponent<MeshRenderer>().material.color = Color.red;

        //GIVE INITIAL VELOCITY TO THE BULLET
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 12, ForceMode.Impulse);
    }

    private void areaAttack(Vector3 target, Quaternion rotation)
    {
        var bullet = (GameObject)Instantiate(areaPrefab, target, rotation);
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