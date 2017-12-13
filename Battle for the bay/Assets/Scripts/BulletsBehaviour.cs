using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehaviour : MonoBehaviour
{
    public string GeneratedTag;
    public float DamageOnHit = 20f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GeneratedTag)
        {
            return;
        }
        if (other.gameObject.tag == "PlayerBase" && GeneratedTag == "PlayerTower")
        {
            return;
        }



        if (other.gameObject.tag == TagCostants.Destructable && !GeneratedTag.Contains("Tower"))
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
            return;
        }

        if (other.gameObject.tag == TagCostants.EnemyMinion)
        {
            if (!GeneratedTag.Contains("Enemy"))
            {
                other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
                Destroy(gameObject, 0.1f);
                return;

            }
        }
        else if (other.gameObject.tag == TagCostants.PlayerMinion)
        {
            if (!GeneratedTag.Contains("Player"))
            {
                other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
                Destroy(gameObject, 0.1f);
                return;

            }
        }
        else if (other.gameObject.tag == TagCostants.Player)
        {
            if (!GeneratedTag.Contains("Player"))
            {
                other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
                Destroy(gameObject, 0.1f);
                return;

            }
        }
        else if (other.gameObject.tag == TagCostants.PlayerBase)
        {
            if (!GeneratedTag.Contains("Player"))
            {
                other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
                Destroy(gameObject, 0.1f);
                return;

            }
        }
        else if (other.gameObject.tag == TagCostants.EnemyBase)
        {
            if (!GeneratedTag.Contains("Enemy"))
            {
                other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
                Destroy(gameObject, 0.1f);
                return;

            }
        }
        // else if (other.gameObject.tag == TagCostants.EnemyTower)
        // {
        //     if (!GeneratedTag.Contains("Enemy"))
        //     {
        //         other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
        //         Destroy(gameObject, 0.1f);
        //         return;

        //     }
        // }
        // else if (other.gameObject.tag == TagCostants.PlayerTower)
        // {
        //     if (!GeneratedTag.Contains("Player"))
        //     {
        //         other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
        //         Destroy(gameObject, 0.1f);
        //         return;

        //     }
        // }

        if (other.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
            return;

        }
    }

    // void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.tag == "PlayerBase")
    //     {
    //         other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
    //         Debug.Log("Hit player base");
    //         Destroy(gameObject);
    //     }
    // }

}
