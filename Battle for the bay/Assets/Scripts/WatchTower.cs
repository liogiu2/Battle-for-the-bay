using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTower : MonoBehaviour
{

    public List<GameObject> attackList;
    public GameObject bulletPrefab;
    public bool friendly = false;

    // Use this for initialization
    void Start()
    {
        attackList = new List<GameObject>();
        StartCoroutine("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (GameObject Target in attackList)
        // {

        //     Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        //     //CREATE THE BULLET
        //     var bullet = (GameObject)Instantiate(
        //         bulletPrefab,
        //         bulletPosition,
        //         //Quaternion.Euler(-10, transform.rotation.y - 90, 0));
        //         Quaternion.identity);
        //     Destroy(bullet, 3.0f);

        //     bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
        //     //COLOR THE BULLET
        //     bullet.GetComponent<MeshRenderer>().material.color = Color.black;

        //     //GIVE INITIAL VELOCITY TO THE BULLET
        //     //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
        //     bullet.GetComponent<Rigidbody>().velocity = (Target.transform.position - bulletPosition).normalized * 12;

        //     new return WaitForSeconds(1.0f);
        // }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (friendly && other.tag == "Ship") attackList.Add(other.gameObject);
        if (!friendly && other.tag == "Player") attackList.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (friendly && other.tag == "Ship") attackList.Remove(other.gameObject);
        if (!friendly && other.tag == "Player") attackList.Remove(other.gameObject);
    }


    private IEnumerator Fire()
    {
        while (true)
        {
			attackList.RemoveAll(item => item == null);
            foreach (GameObject Target in attackList)
            {
                // if (Target != null)
                // {
                    Vector3 bulletPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

                    //CREATE THE BULLET
                    var bullet = (GameObject)Instantiate(
                        bulletPrefab,
                        bulletPosition,
                        //Quaternion.Euler(-10, transform.rotation.y - 90, 0));
                        Quaternion.identity);
                    Destroy(bullet, 3.0f);

                    bullet.GetComponent<BulletsBehaviour>().GeneratedTag = gameObject.tag;
                    //COLOR THE BULLET
                    bullet.GetComponent<MeshRenderer>().material.color = Color.black;

                    //GIVE INITIAL VELOCITY TO THE BULLET
                    //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;
                    bullet.GetComponent<Rigidbody>().velocity = (Target.transform.position - bulletPosition).normalized * 12;
                // }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

}
