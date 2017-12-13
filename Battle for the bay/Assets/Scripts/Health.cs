using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public float HealthRecovery;
    public AudioClip DeathSound;
    public GameObject Explosion;
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
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeathBar();
        if (minionHealthBar)
        {
            minionHealthBar.value = health / MaxHealth;
        }
        if (gameObject.tag == "Player" && health < MaxHealth && !_coroutineStarted)
        {
            StartCoroutine(HealthRecoveryRoutine());
        }
    }

    public void DamageOnHit(float DamageOnHit)
    {
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
        health = MaxHealth;        
        transform.position = GameObject.Find("PlayerBase").transform.Find("Spawner").transform.position;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(old[child.name]);
        }
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
