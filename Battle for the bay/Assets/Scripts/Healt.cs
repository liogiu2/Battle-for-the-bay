using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt : MonoBehaviour
{

    public float healt;
    public float MaxHealth;
    public float HealthRecovery;
    public GameObject Explosion;
    public int MoneyOnDie;
    private UpdateEnemyList updateEnemyList;
    private bool _coroutineStarted = false;

    // Use this for initialization
    void Start()
    {
        updateEnemyList = GameObject.Find("GameManager").GetComponent<UpdateEnemyList>();
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
            yield return new WaitForSeconds(0.5f);
        }
        _coroutineStarted = false;
        if(healt > MaxHealth){
            healt = MaxHealth;
        }

    }
}
