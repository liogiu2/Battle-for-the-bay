using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRootScript : MonoBehaviour
{

    public enum STATE
    {
        Idle,
        Combat
    }
    public STATE currentState;
    public List<AIRootScript> detected, enemies;
    public GameObject TargetEnemy;
    public GameObject bulletPrefab;
    public float BulletSpeed;

    bool _startedFire = false;
    private bool _corutineStarted = false;
    protected UpdateEnemyList _updateEnemyList;
    // Use this for initialization
    void Start()
    {
        ChangeState(STATE.Idle);
        detected = new List<AIRootScript>();
        enemies = new List<AIRootScript>();
        _updateEnemyList = GameObject.Find("GameManager").GetComponent<UpdateEnemyList>();
    }

    // Update is called once per frame
    void Update()
    {
        //Let extended class to do something
        OnUpdate();

        switch (currentState)
        {
            case STATE.Idle:
                _startedFire = false;
                if (TargetEnemy && TargetEnemy.GetComponent<AiEnemyScript>())
                {
                    TargetEnemy.GetComponent<AiEnemyScript>().SendMessage("DeActivateTarget");
                }
                TargetEnemy = null;
                return;
            case STATE.Combat:
                if (TargetEnemy)
                {
                    if (!_startedFire && !_corutineStarted)
                    {
                        _startedFire = true;
                        StartCoroutine(Fire(TargetEnemy));
                    }
                }
                return;
        }
    }

    //Method to be overrided
    protected virtual void OnUpdate() { }

    public void ChangeState(STATE state)
    {
        currentState = state;
    }
    private IEnumerator Fire(GameObject Target)
    {
        _corutineStarted = true;
        while (_startedFire)
        {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            //CREATE THE BULLET
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletPosition,
                //Quaternion.Euler(-10, transform.rotation.y - 90, 0));
                Quaternion.identity);
            Destroy(bullet, 3.0f);

            bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
            //COLOR THE BULLET
            bullet.GetComponent<MeshRenderer>().material.color = Color.black;

            //GIVE INITIAL VELOCITY TO THE BULLET
            //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
            bullet.GetComponent<Rigidbody>().velocity = (Target.transform.position - bulletPosition).normalized * BulletSpeed;

            yield return new WaitForSeconds(1.0f);
        }
        _corutineStarted = false;

    }

    public void DamageOnHit() { }

    public void StopCorutineFire()
    {
        StopCoroutine("Fire");
        _startedFire = false;
    }

}
