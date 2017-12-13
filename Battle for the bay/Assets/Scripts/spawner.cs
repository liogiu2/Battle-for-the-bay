using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    public float SecondForSpawn;
    public GameObject EnemyGameobject;
    public int spawnCount = 1;

    private bool _corutineStarted = false;
    private bool _spawning = true;
    private float range = 10.0f;

    private Vector3 initPos;


    // Use this for initialization
    void Start() { 
        initPos = transform.position;
        StartSpawing();

        for (int i = 0; i < spawnCount; i++)
        {
            StartCoroutine(SpawnEnemyCoroutine());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SpawnEnemyCoroutine()
    {
        _corutineStarted = true;
        while (_spawning)
        {
            if (EnemyGameobject)
            {
                GameObject enemy = (GameObject)Instantiate(EnemyGameobject);
                enemy.transform.position = initPos;
                enemy.transform.rotation = Quaternion.identity;
            }
            yield return new WaitForSeconds(SecondForSpawn);
        }
        _corutineStarted = false;
    }

    public void StartSpawing()
    {
        if (!_corutineStarted)
        {
            _spawning = true;
        }
    }
}
