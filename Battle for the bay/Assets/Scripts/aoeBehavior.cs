using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeBehavior : MonoBehaviour
{
    public float DamagePerSecond = 20f;
    public float DamageFrequency = 1f;
    public float Duration = 3f;
    private float DurationTimer;
    private float DamageCooldown;
    private List<GameObject> targets;
    private UpdateEnemyList updateEnemy;

    // Use this for initialization
    void Start()
    {
        targets = new List<GameObject>();
        DurationTimer = 0f;
        DamageCooldown = 0f;
        Object.Destroy(gameObject, Duration);
        StartCoroutine("burn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
            targets.Add(other.gameObject);
        //other.gameObject.SendMessage("DamageOnHit", DamagePerSecond);

        // if (other.gameObject.tag == "Ship" || other.gameObject.tag == "Player")
        // {
        //     StartCoroutine("burn", other);
        //     //Destroy(gameObject);
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")
            targets.Remove(other.gameObject);
    }

    IEnumerator burn()
    {
        while (true)
        {
            UpdateTargetList();
            foreach (GameObject target in targets)
            {
                target.SendMessage("DamageOnHit", DamagePerSecond / 10);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void UpdateTargetList()
    {
        targets.RemoveAll(obj => obj == null);
    }
}
