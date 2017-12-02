using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRootScript : MonoBehaviour
{

    public enum STATE
    {
        Idle,
        Combat
    }
    public STATE currentState;
    public List<AIRootScript> detected, enemies;
    public GameObject TargetEnemy;
    public GameObject bulletPrefab;
    // Use this for initialization
    void Start()
    {
        ChangeState(STATE.Idle);
        detected = new List<AIRootScript>();
        enemies = new List<AIRootScript>();
    }

    // Update is called once per frame
    void Update()
    {

        //Let extended class to do something
        OnUpdate();

        switch (currentState)
        {
            case STATE.Combat:
                if (TargetEnemy == null)
                {
                    return;
                }
                Fire(TargetEnemy);
                return;
        }
    }

    //Method to be overrided
    protected virtual void OnUpdate() { }

    public void ChangeState(STATE state)
    {
        currentState = state;
    }
    void Fire(GameObject Target)
    {
        //bulletPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

        //CREATE THE BULLET
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            transform.position + transform.forward,
            Quaternion.Euler(-10, transform.rotation.y - 90, 0));

        //COLOR THE BULLET
        bullet.GetComponent<MeshRenderer>().material.color = Color.black;

        //GIVE INITIAL VELOCITY TO THE BULLET
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;

        Destroy(bullet, 13.0f);
    }

}
