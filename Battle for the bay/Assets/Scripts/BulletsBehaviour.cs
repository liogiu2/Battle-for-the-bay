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
        if (other.gameObject.tag == "Ship")
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("DamageOnHit", DamageOnHit);
            Destroy(gameObject, 0.1f);
        }

        if (other.gameObject.tag == "Terrain")
        {
            Debug.Log("hit terrain!");
            Destroy(gameObject);
        }

    }

}
