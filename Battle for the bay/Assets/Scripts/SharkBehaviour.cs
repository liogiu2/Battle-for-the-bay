using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SharkBehaviour : MonoBehaviour
{

    public enum STATE
    {
        Idle,
        Wander,
        Chase,
    }

    public STATE currentState;

    // WANDERING BEHAVIOUR
    public float wanderRadius;
    public float maxWanderTime;
    private float wanderTime;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;


    // CHASE BEHAVIOUR
    public List<GameObject> targetsInVision;
    public List<GameObject> damageTargets;
    public GameObject target;
    // IDLE BEHAVIOUR
    public float maxIdleTime;
    private float idleTime;
    private float idleTimer;

    private SharkAttack sharkAttack;

    private NavMeshPath path;
    // Use this for initialization
    void Start()
    {
        sharkAttack = gameObject.transform.Find("AttackRange").GetComponent<SharkAttack>();
        wanderTimer = maxWanderTime;
        // wanderRadius = 10f;
        agent = gameObject.GetComponent<NavMeshAgent>();

        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
        agent.isStopped = false;
        timer = 0;

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

        if (idleTimer >= idleTime)
        {
            wanderTime = Random.Range(0f, maxWanderTime);
            idleTimer = 0f;

            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            agent.isStopped = false;

            ChangeState(STATE.Wander);
        }
        idleTimer += Time.deltaTime;
    }

    private void Wander()
    {

        // timer += Time.deltaTime;

        // if (timer >= wanderTimer)
        // {
        //     Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        //     agent.SetDestination(newPos);
        //     agent.isStopped = false;
        //     timer = 0;
        // }

        if (wanderTimer >= wanderTime)
        {
            idleTime = Random.Range(0f, maxIdleTime);
            wanderTimer = 0f;
            Random random = new Random();
            ChangeState(STATE.Idle);
        }
        wanderTimer += Time.deltaTime;
    }

    private void Chase()
    {
        targetsInVision.RemoveAll(item => item == null);

        if (targetsInVision.Count > 0)
        {
            target = targetsInVision[0];
        }
        if (target != null)
        {
            if (agent.destination != target.transform.position) agent.SetDestination(target.transform.position);
            agent.isStopped = false;
        }

        // GameObject opponent = (gameObject.tag == "EnemyMinion") ? targetsInVision.Find(item => item.tag == "Player") : targetsInVision.Find(item => item.tag == "Enemy");

        // if (opponent != null)
        // {
        //     target = opponent;
        // }
        // else if (targetsInVision.Count > 0)
        // {
        //     target = targetsInVision[0];
        // }
        // else
        // {
        //     ChangeState(STATE.Wander);
        // }
        // if (target != null)
        // {
        //     if (agent.destination != target.transform.position) agent.SetDestination(target.transform.position);
        //     agent.isStopped = false;
        // }
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

    void OnCollisionEnter(Collision other)
    {
		Debug.Log("colliding with " + other.gameObject.tag);
        if (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyMinion")
        {
            damageTargets.Add(other.gameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "PlayerMinion" || other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyMinion")
        {
            damageTargets.Remove(other.gameObject);
        }
    }


    public void Damage(int dmg)
    {
        Debug.Log("Damage: " + dmg);
        foreach(GameObject target in damageTargets){
			target.GetComponent<Health>().DamageOnHit(dmg);
		}
		//sharkAttack.canDealDamage = true;
    }

}
