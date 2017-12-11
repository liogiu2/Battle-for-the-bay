using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    public float SecondForSpawn;
    public GameObject EnemyGameobject;
    public int spawnCount = 1;

    private bool _corutineStarted = false;
    private bool _spawning = true;
    
    // Use this for initialization
    void Start () {
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
            Debug.Log("spawning!");
            GameObject enemy = (GameObject)Instantiate(EnemyGameobject);
            enemy.transform.position = transform.position;
            enemy.transform.rotation = Quaternion.identity;

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
