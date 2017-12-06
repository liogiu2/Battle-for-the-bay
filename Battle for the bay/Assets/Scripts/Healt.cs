using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float health;
    public GameObject Explosion;
    private UpdateEnemyList updateEnemyList;

    // Use this for initialization
    void Start()
    {
        updateEnemyList = GameObject.Find("GameManager").GetComponent<UpdateEnemyList>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageOnHit(float DamageOnHit)
    {
        health -= DamageOnHit;
        if (health <= 0)
        {
            if (gameObject.tag == "Player")
            {
                health = 100;
            }
            else
            {
                if (Explosion)
                {
                    Explosion.SetActive(true);
                }
                updateEnemyList.AddDestroyingItem(gameObject.GetComponent<AIRootScript>());
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
