using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public float HealthRecovery;
    public AudioClip DeathSound;
    public GameObject Explosion;
    public GameObject Smoke;
    public GameObject Fire;
    public int MoneyOnDie;
    public GameObject HealthBar;
    public Slider minionHealthBar;
    public bool GodMode = false;

    private UpdateEnemyList updateEnemyList;
    private bool _coroutineStarted = false;
    private bool _coroutineStartedRespawn = false;

    private Image _bar;
    private Vector3 _startPosition;


    // Use this for initialization
    void Start()
    {
        updateEnemyList = GameObject.Find("GameManager").GetComponent<UpdateEnemyList>();
        if (HealthBar)
        {
            _bar = HealthBar.transform.Find("bar").Find("Image").GetComponent<Image>();
            _bar.fillAmount = 1;
        }
        _startPosition = new Vector3(-73.4f, 0.02f, -14.4f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeathBar();
        if (minionHealthBar)
        {
            minionHealthBar.value = health / MaxHealth;
        }
        if (tag == TagCostants.Player)
        {
            CheckSmokeAndFire();
        }
        if (gameObject.tag == "Player" && health < MaxHealth && !_coroutineStarted)
        {
            StartCoroutine(HealthRecoveryRoutine());
        }
    }

    private void CheckSmokeAndFire()
    {
        float percentage = (health / MaxHealth) * 100;
        if (percentage <= 50)
        {
            if (!Smoke.activeSelf)
            {
                Smoke.SetActive(true);
            }
            if (percentage <= 30)
            {
                if (!Fire.activeSelf)
                {
                    Fire.SetActive(true);
                }
            }
            else if (Fire.activeSelf)
            {
                Fire.SetActive(false);
            }
        }
        else if (Smoke.activeSelf)
        {
            Smoke.SetActive(false);
            if (Fire.activeSelf)
            {
                Fire.SetActive(false);
            }
        }
    }
    public void DamageOnHit(float DamageOnHit)
    {
        if (!_coroutineStartedRespawn)
            health -= DamageOnHit;
        if (gameObject.tag == "Player" && HealthBar)
        {
            UpdateHeathBar();
        }
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                // SceneManager.LoadScene(3);
                if (GodMode)
                {
                    health = 100;
                    return;
                }
                if (!_coroutineStartedRespawn)
                {
                    GetComponent<CollectResources>().Money = 0;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponent<MoveInput>().enabled = false;
                    GetComponent<ShipMovement>().enabled = false;
                    Dictionary<string, bool> old = new Dictionary<string, bool>();
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        GameObject child = transform.GetChild(i).gameObject;
                        old.Add(child.name, child.activeSelf);
                        child.SetActive(false);
                    }
                    StartCoroutine(Respawn(old));
                    if (Explosion)
                    {
                        Explosion.SetActive(true);
                    }
                }
            }
            else
            {
                if (Explosion)
                {
                    Explosion.SetActive(true);
                }
                updateEnemyList.AddDestroyingItem(gameObject.GetComponent<AIRootScript>());
                GameObject.FindGameObjectWithTag("Player").SendMessage("AddMoney", MoneyOnDie);

                // Spawn the sound object
                GameObject explosionSound = new GameObject("bulletSound");
                AudioSource audioSource = explosionSound.AddComponent<AudioSource>();
                Destroy(explosionSound, DeathSound.length);
                audioSource.PlayOneShot(DeathSound);

                Destroy(gameObject, 0.2f);
            }
        }
    }

    private IEnumerator Respawn(Dictionary<string, bool> old)
    {
        _coroutineStartedRespawn = true;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("respawn");
        Explosion.SetActive(false);
        Vector3 pos = GameObject.Find("PlayerBase").transform.Find("Spawner").transform.position;
        transform.position = pos;
        health = MaxHealth;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(old[child.name]);
        }
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<MoveInput>().enabled = true;
        GetComponent<MoveInput>().shipPointer.position = pos;
        GetComponent<ShipMovement>().enabled = true;
        _coroutineStartedRespawn = false;

    }

    private IEnumerator HealthRecoveryRoutine()
    {
        _coroutineStarted = true;
        while (health < MaxHealth && _coroutineStarted)
        {
            health += HealthRecovery;
            UpdateHeathBar();
            yield return new WaitForSeconds(0.5f);
        }
        _coroutineStarted = false;
        if (health > MaxHealth)
        {
            health = MaxHealth;
            UpdateHeathBar();
        }

    }

    private void UpdateHeathBar()
    {
        if (_bar == null) return;
        _bar.fillAmount = health / MaxHealth;
    }

    public void upgradeHealthBar()
    {
        _bar = HealthBar.transform.Find("bar upgraded").Find("Image").GetComponent<Image>();
    }
}
