using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainAI : MonoBehaviour
{

    // Use this for initialization
    public List<GameObject> Enemies;
    public int MaxNumberOfEnemyes;
    public int MinNumberOfEnemyes;

    private UpdateEnemyList _updateEnemy;
    private SpawnEnemies _spawnEnemies;
    void Start()
    {
        _updateEnemy = GetComponent<UpdateEnemyList>();
        _spawnEnemies = GetComponent<SpawnEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_updateEnemy.DestroyingGameObject)
        {
            Enemies.Remove(_updateEnemy.DestroyingGameObject);
        }
        if (Enemies.Count >= MaxNumberOfEnemyes)
        {
            _spawnEnemies.StopSpawing();
        }
        if (Enemies.Count < MinNumberOfEnemyes)
        {
            _spawnEnemies.StartSpawing();
        }
    }
}
