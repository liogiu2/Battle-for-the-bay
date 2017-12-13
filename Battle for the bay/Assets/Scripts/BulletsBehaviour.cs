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
        if (other.gameObject.tag == TagCostants.EnemyMinion)
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == TagCostants.PlayerMinion)
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == TagCostants.Player)
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == TagCostants.PlayerBase)
        {
            other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == TagCostants.EnemyBase)
        {
            other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == TagCostants.EnemyTower)
        {
            other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }
        else if (other.gameObject.tag == TagCostants.PlayerTower)
        {
            other.gameObject.transform.parent.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }

        if (other.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
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
