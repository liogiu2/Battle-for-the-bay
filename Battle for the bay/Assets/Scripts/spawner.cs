using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public float SecondForSpawn;
    public GameObject EnemyGameobject;
    public int spawnCount = 1;

    private Vector3 initPos;


    // Use this for initialization
    void Start()
    {
        initPos = transform.position;
        StartCoroutine(SpawnEnemyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if (EnemyGameobject)
                {
                    GameObject enemy = (GameObject)Instantiate(EnemyGameobject);
                    enemy.transform.position = initPos;
                    enemy.transform.rotation = Quaternion.identity;
                }
                yield return new WaitForSeconds(.5f);
            }
            yield return new WaitForSeconds(SecondForSpawn);
        }
    }
}
