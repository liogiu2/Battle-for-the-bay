using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    public int aimMode = 0;
    public float areaPointerRange = 5f;
    Transform rangePointer;
    Transform linePointer;
    Transform areaPointer;

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
    public GameObject cooldownQIndicator;
    public GameObject cooldownWIndicator;
    public GameObject cooldownEIndicator;
    public float cooldownQ;
    public float cooldownW;
    public float cooldownE;
    private Text cooldownQText;
    private Text cooldownWText;
    private Text cooldownEText;
    public float cooldownQTimer;
    public float cooldownWTimer;
    public float cooldownETimer;

    public AudioClip QAudioClip;
    public AudioClip WAudioClip;
    public AudioClip EAudioClip;
    public AudioClip EAudioClip2;

    private int aimingConfig;

    void Start()
    {
        aimingConfig = 0;

        cooldownQTimer = 0f;
        cooldownWTimer = 0f;
        cooldownETimer = 0f;
        cooldownQIndicator.SetActive(false);
        cooldownWIndicator.SetActive(false);
        cooldownEIndicator.SetActive(false);
        cooldownQText = cooldownQIndicator.transform.Find("CoolDownText").GetComponent<Text>();
        cooldownWText = cooldownWIndicator.transform.Find("CoolDownText").GetComponent<Text>();
        cooldownEText = cooldownEIndicator.transform.Find("CoolDownText").GetComponent<Text>();

        lineShotAim = this.gameObject.transform.Find("Aim").gameObject;
        rangePointer = lineShotAim.transform.Find("RangeAbility");
        linePointer = lineShotAim.transform.Find("LineAbility");
        areaPointer = lineShotAim.transform.Find("AreaAbility");

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

        if (aimingConfig == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 position = new Vector3(hit.point.x, lineShotAim.transform.position.y, hit.point.z);
                lineShotAim.transform.LookAt(position);
                if (Input.GetKeyDown("q") && cooldownQTimer == 0f)
                {
                    cooldownQTimer = cooldownQ;
                    fireTowards(lineShotAim.transform.rotation);
                    clickDelay = true;                    
                }
                if (Input.GetKeyDown("w") && cooldownWTimer == 0f)
                {
                    cooldownWTimer = cooldownW;
                    MultiFire(lineShotAim.transform.rotation);
                    clickDelay = true;
                }
                if (Input.GetKeyDown("e") && cooldownETimer == 0f)
                {
                    Vector3 centerPosition = transform.localPosition;
                    float distance = Vector3.Distance(hit.point, centerPosition);

                    if (distance > areaPointerRange) //If the distance is less than the radius, it is already within the circle.
                    {
                        Vector3 fromOriginToObject = hit.point - centerPosition;
                        fromOriginToObject *= areaPointerRange / distance;
                        areaPointer.position = centerPosition + fromOriginToObject;
                        areaPointer.position = new Vector3(areaPointer.position.x, transform.position.y, areaPointer.position.z);
                    }
                    else
                    {
                        areaPointer.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    }
                    cooldownETimer = cooldownE;

                    areaAttack(areaPointer.position, lineShotAim.transform.rotation);
                    clickDelay = true;
                }
            }
        } else
        {
            if (Input.GetKeyDown("q"))
            {
                if (aimMode > 0 && aimMode != 1)
                {
                    resetSprites();
                }
                if (aimMode != 1)
                {
                    aimMode = 1;
                    lineShotAim.SetActive(true);
                }
                else
                {
                    aimMode = 0;
                }   
            }
            else if (Input.GetKeyDown("w"))
            {
                if (aimMode > 0 && aimMode != 2)
                {
                    resetSprites();
                }
                if (aimMode != 2)
                {
                    aimMode = 2;
                    lineShotAim.SetActive(true);
                }
                else
                {
                    aimMode = 0;
                }
            }
            else if (Input.GetKeyDown("e"))
            {
                if (aimMode > 0 && aimMode != 3)
                {
                    resetSprites();
                }
                if (aimMode != 3)
                {
                    aimMode = 3;
                    lineShotAim.SetActive(true);
                }
                else
                {
                    aimMode = 0;
                }
            }
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

                        if (Input.GetMouseButtonDown(0) && cooldownQTimer == 0f)
                        {
                            cooldownQTimer = cooldownQ;
                            fireTowards(lineShotAim.transform.rotation);
                            if (aimingConfig == 0) resetSprites();
                            clickDelay = true;
                        }
                        
                        break;
                    case 2:
                        // Range 
                        rangeSprite.enabled = true;
                        lineShotAim.transform.LookAt(position);

                        if (Input.GetMouseButtonDown(0) && cooldownWTimer == 0f)
                        {
                            cooldownWTimer = cooldownW;
                            MultiFire(lineShotAim.transform.rotation);
                            if (aimingConfig == 0) resetSprites();
                            clickDelay = true;
                        }
                        
                        break;
                    case 3:
                        // Splash Area mode
                        areaSprite.enabled = true;

                        Vector3 centerPosition = transform.localPosition;
                        float distance = Vector3.Distance(hit.point, centerPosition);

                        if (distance > areaPointerRange) //If the distance is less than the radius, it is already within the circle.
                        {
                            Vector3 fromOriginToObject = hit.point - centerPosition;
                            fromOriginToObject *= areaPointerRange / distance;
                            areaPointer.position = centerPosition + fromOriginToObject;
                            areaPointer.position = new Vector3(areaPointer.position.x, transform.position.y, areaPointer.position.z);
                        }
                        else
                        {
                            areaPointer.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        }
                        if (Input.GetMouseButtonDown(0) && cooldownETimer == 0f)//&& dist < areaPointerRange)
                        {
                            cooldownETimer = cooldownE;

                            areaAttack(areaPointer.position, lineShotAim.transform.rotation);
                            if (aimingConfig == 0) resetSprites();
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

        if (cooldownQTimer > 0)
        {
            cooldownQIndicator.SetActive(true);
            cooldownQText.text = cooldownQTimer.ToString("0.0");
            cooldownQIndicator.GetComponent<Image>().fillAmount = cooldownQTimer / cooldownQ;
        }
        else
            cooldownQIndicator.SetActive(false);
        if (cooldownWTimer > 0)
        {
            cooldownWIndicator.SetActive(true);
            cooldownWText.text = cooldownWTimer.ToString("0.0");
            cooldownWIndicator.GetComponent<Image>().fillAmount = cooldownWTimer / cooldownW;
        }
        else
            cooldownWIndicator.SetActive(false);

        if (cooldownETimer > 0)
        {
            cooldownEIndicator.SetActive(true);
            cooldownEText.text = cooldownETimer.ToString("0.0");
            cooldownEIndicator.GetComponent<Image>().fillAmount = cooldownETimer / cooldownE;
        }
        else
            cooldownEIndicator.SetActive(false);

    }

    private void MultiFire(Quaternion Target)
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        //CREATE THE BULLET
        List<GameObject> bullets = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            bullets.Add((GameObject)Instantiate(rangePrefab, bulletPosition, Target));
            bullets[i].GetComponent<LineShotProjectile>().GeneratedTag = gameObject.tag;
        }

        Vector3 dir = bullets[0].transform.forward;
        int iterator = 0;
        for (int j = -10; j <= 10; j += 5)
        {
            bullets[iterator].GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, j, 0) * dir * 12, ForceMode.Impulse);
            iterator++;
        }

        // Spawn the sound object
        playSound(WAudioClip);
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

        // Spawn the sound object
        playSound(QAudioClip);
    }

    private void areaAttack(Vector3 target, Quaternion rotation)
    {
        var bullet = (GameObject)Instantiate(areaPrefab, target, rotation);
        playSound(EAudioClip);

        GameObject explosionSound = new GameObject("bulletSound");
        AudioSource audioSource = explosionSound.AddComponent<AudioSource>();
        Destroy(explosionSound, areaPrefab.GetComponent<aoeBehavior>().Duration);
        audioSource.PlayOneShot(EAudioClip2);
    }

    private void resetSprites()
    {
        Debug.Log("resetting sprites");
        aimMode = 0;
        areaSprite.enabled = false;
        rangeSprite.enabled = false;
        lineSprite.enabled = false;
        lineShotAim.SetActive(false);
    }

    private void playSound(AudioClip audioClip)
    {
        // Spawn the sound object
        GameObject explosionSound = new GameObject("bulletSound");
        AudioSource audioSource = explosionSound.AddComponent<AudioSource>();
        Destroy(explosionSound, audioClip.length);
        audioSource.PlayOneShot(audioClip);
    }

    public void setAimConfig(int conf)
    {
        aimingConfig = conf;
        resetSprites();
    }
}