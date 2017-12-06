using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public List<Transform> SpawnPoints;
    public float SecondForSpawn;
    public GameObject EnemyGameobject;
    private bool _corutineStarted = false;
    private bool _spawning = false;
    private EnemyMainAI EnemyAI;
    // Use this for initialization
    void Start()
    {
        EnemyAI = GetComponent<EnemyMainAI>();
        _spawning = true;
        if (SpawnPoints.Count == 0)
        {
            Debug.LogError("No spawn point in the spawn enemy script on the gamemanager!");
            _spawning = false;
        }
        StartCoroutine(SpawnEnemyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int RandomNumber()
    {
        return Random.Range(0, SpawnPoints.Count - 1);
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        _corutineStarted = true;
        while (_spawning)
        {
            Vector3 EnemyPosition = SpawnPoints[RandomNumber()].position;

            //CREATE THE enemy
            var enemy = (GameObject)Instantiate(
                EnemyGameobject,
                EnemyPosition,
                Quaternion.identity);
            EnemyAI.Enemies.Add(enemy);

            yield return new WaitForSeconds(SecondForSpawn);
        }
        _corutineStarted = false;
    }

    public void StopSpawing()
    {
        _spawning = false;
    }

    public void StartSpawing()
    {
        if (!_corutineStarted)
        {
            _spawning = true;
            StartCoroutine(SpawnEnemyCoroutine());
        }
    }
}
