using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healt : MonoBehaviour
{

    public float healt;
    public float MaxHealth;
    public float HealthRecovery;
    public GameObject Explosion;
    public int MoneyOnDie;
    public GameObject HealthBar;
    private UpdateEnemyList updateEnemyList;
    private bool _coroutineStarted = false;
    private Image _bar;
    // Use this for initialization
    void Start()
    {
        updateEnemyList = GameObject.Find("GameManager").GetComponent<UpdateEnemyList>();
        if (HealthBar)
        {
            _bar = HealthBar.transform.Find("bar").GetComponentInChildren<Image>();
            _bar.fillAmount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player" && healt < MaxHealth && !_coroutineStarted)
        {
            StartCoroutine(HealthRecoveryRoutine());
        }

    }

    public void DamageOnHit(float DamageOnHit)
    {
        healt -= DamageOnHit;
        if (gameObject.tag == "Player" && HealthBar)
        {
            UpdateHeathBar();
        }
        if (healt <= 0)
        {
            if (gameObject.tag == "Player")
            {
                healt = MaxHealth;
            }
            else
            {
                if (Explosion)
                {
                    Explosion.SetActive(true);
                }
                updateEnemyList.AddDestroyingItem(gameObject.GetComponent<AIRootScript>());
                GameObject.FindGameObjectWithTag("Player").SendMessage("AddMoney", MoneyOnDie);
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private IEnumerator HealthRecoveryRoutine()
    {
        _coroutineStarted = true;
        while (healt < MaxHealth && _coroutineStarted)
        {
            healt += HealthRecovery;
            UpdateHeathBar();
            yield return new WaitForSeconds(0.5f);
        }
        _coroutineStarted = false;
        if (healt > MaxHealth)
        {
            healt = MaxHealth;
            UpdateHeathBar();
        }

    }

    private void UpdateHeathBar()
    {
        _bar.fillAmount = healt / MaxHealth;
    }
}
