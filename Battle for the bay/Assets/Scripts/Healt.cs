using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt : MonoBehaviour
{

    public float healt;
    public GameObject Explosion;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageOnHit(float DamageOnHit)
    {
        healt -= DamageOnHit;
        if (healt <= 0)
        {
            if (gameObject.tag == "Player")
            {
                healt = 100;
            }
            else
            {
                if (Explosion)
                {
                    Explosion.SetActive(true);
                }
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
