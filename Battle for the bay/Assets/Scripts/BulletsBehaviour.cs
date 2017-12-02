using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehaviour : MonoBehaviour
{
    public string GeneratedTag;
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
            other.gameObject.SendMessage("DamageOnHit");
            Debug.Log("HIT Enemy");
            Destroy(gameObject, 0.3f);
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("DamageOnHit");
            Debug.Log("HIT Player");
            Destroy(gameObject, 0.3f);
        }
    }

}
