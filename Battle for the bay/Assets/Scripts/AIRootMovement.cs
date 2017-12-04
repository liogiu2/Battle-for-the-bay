using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRootMovement : MonoBehaviour
{

    public enum STATE
    {
        Idle,
        Wander,
        Chase,
    }
    public STATE currentState;

    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;

    private GameObject target;

    // Use this for initialization
    void Start()
    {
        // wanderTimer = 10f;
        // wanderRadius = 10f;
        agent = gameObject.GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        target = GameObject.FindGameObjectWithTag("Player").gameObject;

        ChangeState(STATE.Wander);
    }

    // Update is called once per frame
    void Update()
    {

        //Let extended class to do something
        OnUpdate();

        switch (currentState)
        {
            case STATE.Idle:
                Idle();
                return;
            case STATE.Wander:
                Wander();
                return;
            case STATE.Chase:
                Chase();
                return;
        }
    }

    //Method to be overrided
    protected virtual void OnUpdate() { }

    public void ChangeState(STATE state)
    {
        currentState = state;
    }

    private void Idle()
    {
        agent.isStopped = true;
    }
    private void Wander()
    {

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            agent.isStopped = false;
            timer = 0;
        }

    }
    private void Chase()
    {
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
    }


    Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        // Debug.Log(owner + " randirection is: " + randDirection);

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}