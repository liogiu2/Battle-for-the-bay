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
    public List<GameObject> detected, enemies;
    public GameObject TargetEnemy;
    public GameObject bulletPrefab;
    public GameObject bulletSoundPrefab;
    public float BulletSpeed;

    public AudioClip FireAudioClip;
    bool _startedFire = false;
    private bool _corutineStarted = false;
    protected UpdateEnemyList _updateEnemyList;
    protected string _currentTag;
    // Use this for initialization
    void Start()
    {
        ChangeState(STATE.Idle);
        detected = new List<GameObject>();
        enemies = new List<GameObject>();
        _updateEnemyList = GameObject.Find("GameManager").GetComponent<UpdateEnemyList>();
    }

    // Update is called once per frame
    void Update()
    {
        detected.RemoveAll(item => item == null);
        enemies.RemoveAll(item => item == null);

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
                        ChooseWhoShot();
                    }
                }
                return;
        }
    }

    //Method to be overrided
    protected virtual void OnUpdate() { }
    protected virtual void ChooseWhoShot() { }

    public void ChangeState(STATE state)
    {
        currentState = state;
    }

    protected IEnumerator Fire(GameObject Target)
    {
        _corutineStarted = true;
        while (_startedFire)
        {
            Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            //CREATE THE BULLET
            GameObject bullet = Instantiate(
                bulletPrefab,
                bulletPosition,
                Quaternion.identity);
            Destroy(bullet, 4.0f);



            // Spawn the sound object
            if (bulletSoundPrefab)
            {
                GameObject bulletSound = Instantiate(
                    bulletSoundPrefab,
                    bulletPosition,
                    Quaternion.identity);
                Destroy(bulletSound, bulletSoundPrefab.gameObject.GetComponent<AudioSource>().clip.length);
            }
            // GameObject bulletSound = new GameObject("bulletSound");
            // AudioSource audioSource = bulletSound.AddComponent<AudioSource>();
            // audioSource.clip = FireAudioClip;
            // audioSource.spatialBlend = 1.0f;
            // audioSource.rolloffMode = AudioRolloffMode.Linear;
            // audioSource.maxDistance = 10;
            // Destroy(bulletSound, FireAudioClip.length);
            // // audioSource.PlayOneShot(FireAudioClip);
            // audioSource.Play();

            bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
            //COLOR THE BULLET
            bullet.GetComponent<MeshRenderer>().material.color = Color.black;

            //GIVE INITIAL VELOCITY TO THE BULLET
            //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
            Vector3 pos = Target.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = (pos - bulletPosition).normalized * BulletSpeed;

            yield return new WaitForSeconds(2f);
        }
        _corutineStarted = false;

    }

    public void DamageOnHit() { }

    public void StopCorutineFire()
    {
        StopCoroutine("Fire");
        _startedFire = false;
    }

    void OnDeleteShip()
    {
        GameObject aI = _updateEnemyList.DestroyingGameObject;
        StopCorutineFire();
        detected.Remove(aI);
        enemies.Remove(aI);
        if (TargetEnemy != null && TargetEnemy.GetComponent<AIRootScript>() == aI)
        {
            TargetEnemy = null;
        }
    }

    void OnEnable()
    {
        UpdateEnemyList.OnDeleteShip += OnDeleteShip;
    }


    void OnDisable()
    {
        UpdateEnemyList.OnDeleteShip -= OnDeleteShip;
    }

}
