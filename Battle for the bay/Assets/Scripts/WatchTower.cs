using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTower : MonoBehaviour
{

    public List<GameObject> attackList;
    public GameObject bulletPrefab;
    public bool friendly = false;
    public bool MultiAttack = false;
    public int damage = 20;

    // Use this for initialization
    void Start()
    {
        attackList = new List<GameObject>();
        StartCoroutine("Fire");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (friendly && other.tag == "EnemyMinion") attackList.Add(other.gameObject);
        if (!friendly && other.tag == "PlayerMinion") attackList.Add(other.gameObject);
        if (friendly && other.tag == "Enemy") attackList.Add(other.gameObject);
        if (!friendly && other.tag == "Player") attackList.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (friendly && other.tag == "EnemyMinion") attackList.Remove(other.gameObject);
        if (!friendly && other.tag == "PlayerMinion") attackList.Remove(other.gameObject);
        if (friendly && other.tag == "Enemy") attackList.Remove(other.gameObject);
        if (!friendly && other.tag == "Player") attackList.Remove(other.gameObject);
    }


    private IEnumerator Fire()
    {
        while (true)
        {
            attackList.RemoveAll(item => item == null);
            if (MultiAttack)
            {
                foreach (GameObject Target in attackList)
                {
                    FireFunction(Target);
                }
            }
            else
            {
                if (attackList.Count > 0)
                {
                    FireFunction(attackList[0]);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void FireFunction(GameObject Target)
    {
        Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletPosition,
            Quaternion.identity);
        Destroy(bullet, 3.0f);

        bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
        bullet.GetComponent<BulletsBehaviour>().DamageOnHit = damage;
        //COLOR THE BULLET
        bullet.GetComponent<MeshRenderer>().material.color = Color.black;

        //GIVE INITIAL VELOCITY TO THE BULLET
        bullet.GetComponent<Rigidbody>().velocity = (Target.transform.position - bulletPosition).normalized * 12;
    }

}
