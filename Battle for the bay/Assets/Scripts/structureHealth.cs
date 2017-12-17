using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class structureHealth : MonoBehaviour
{
    public float health = 200f;
    public float initHealth = 200f;
    public GameObject Explosion;
    public Slider structureHealthBar;
    public int PointsOnDieTower = 100;
    private Score _score;
    private GameObject _allertImage;

    // Use this for initialization
    void Start()
    {
        initHealth = health;
        _score = GameObject.Find("Score").GetComponent<Score>();
        _allertImage = GameObject.Find("HUD").transform.Find("Canvas").transform.Find("Panel").transform.Find("StatusBar").transform.Find("ContainerPlayer").transform.Find("BaseUnderAttack").gameObject;
        _allertImage.SetActive(false);
    }

    void OnEnable()
    {

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
            Destroy(gameObject, 0.1f);
        }
    }

    public void DamageOnHit(float damage)
    {
        this.health -= damage;
        if (!_allertImage.activeSelf && (name == "PlayerBase" || transform.parent.name == "PlayerBase"))
        {
            _allertImage.SetActive(true);
            StartCoroutine(DeactivateAfterSecond());
        }
    }

    private IEnumerator DeactivateAfterSecond()
    {
        yield return new WaitForSeconds(5);
        _allertImage.SetActive(false);
    }
}
