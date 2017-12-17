using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class structureHealth : MonoBehaviour
{
    public float health = 200f;
    public float initHealth = 200f;
    public GameObject Explosion;
    public GameObject destructionSoundPrefab;
    public Slider structureHealthBar;
    public int PointsOnDieTower = 100;
    private Score _score;

    // Use this for initialization
    void Start()
    {
        initHealth = health;
        _score = GameObject.Find("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if (structureHealthBar)
        {
            structureHealthBar.value = health / initHealth;
        }
        if (this.health <= 0)
        {
            _score.AddPoints(PointsOnDieTower);
            var bang = (GameObject)Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(bang, 3.5f);

            if (destructionSoundPrefab)
            {
                GameObject deathSound = Instantiate(
                    destructionSoundPrefab,
                    transform.position,
                    Quaternion.identity);
                Destroy(deathSound, destructionSoundPrefab.gameObject.GetComponent<AudioSource>().clip.length);
            }

            Destroy(gameObject, 0.1f);
        }
    }

    public void DamageOnHit(float damage)
    {
        this.health -= damage;
    }
}
