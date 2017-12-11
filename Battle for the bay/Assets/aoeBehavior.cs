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
        if (other.tag == "EnemyMinion") targets.Add(other.gameObject);
        //other.gameObject.SendMessage("DamageOnHit", DamagePerSecond);

        // if (other.gameObject.tag == "Ship" || other.gameObject.tag == "Player")
        // {
        //     StartCoroutine("burn", other);
        //     //Destroy(gameObject);
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyMinion") targets.Remove(other.gameObject);
    }

    IEnumerator burn()
    {

        while (true)
        {
            while (DamageCooldown < DamageFrequency)
            {
                DamageCooldown += Time.deltaTime;
                yield return null;
            }

            foreach (GameObject target in targets)
            {
                // Debug.Log("damage to: " + target);
                target.SendMessage("DamageOnHit", DamagePerSecond);
            }
            DamageCooldown = 0f;
            yield return null;
        }

    }

}
