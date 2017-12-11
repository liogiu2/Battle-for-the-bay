using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class structureHealth : MonoBehaviour {
    public float health = 200f;
    private float initHealth;
    public GameObject Explosion;
    public Slider structureHealthBar;

    // Use this for initialization
    void Start () {
        initHealth = health;
    }
	
	// Update is called once per frame
	void Update () {
        if (structureHealthBar)
        {
            structureHealthBar.value = health / initHealth;
        }
		if(this.health <= 0)
        {
            var bang = (GameObject)Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(bang, 3.5f);
            Destroy(gameObject, 0.1f);
        }
	}

    public void DamageOnHit(float damage)
    {
        this.health -= damage;
    }
}
