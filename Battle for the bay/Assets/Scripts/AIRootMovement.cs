using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIRootMovement : MonoBehaviour
{

    public enum STATE
    {
        Idle,
        OrbitAround,
        Wander,
        GoToOpponentBase,
        Chase,
    }

    public enum DESTINATION
    {
        None,
        OpponentBase,
        Opponent
    }

    private DESTINATION dest;
    public STATE currentState;

    // WANDERING BEHAVIOUR
    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;


    // CHASE BEHAVIOUR
    public List<GameObject> targetsInVision;
    public GameObject target;
    private GameObject targetBase;
    // IDLE BEHAVIOUR
    public float maxIdleTime;
    private float idleTime;
    private float idleTimer;

    // ORBIT BEHAVIOUR
    private Vector3 axis = Vector3.up;
    private Vector3 desiredPosition;
    private Transform desiredRotation;
    public float orbitRadius;
    public float orbitRadiusSpeed;
    public float rotationSpeed;
    private int orbitDirection;
    public float maxOrbitTime;
    private float orbitTime;
    private float orbitTimer;


    private NavMeshPath path;
    // Use this for initialization
    void Start()
    {
        orbitDirection = 1; // clockwise
        orbitRadius = 2.0f;
        orbitTimer = 0f;
        // wanderTimer = 10f;
        // wanderRadius = 10f;
        agent = gameObject.GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        if (gameObject.tag == "EnemyMinion")
        {
            target = GameObject.FindGameObjectWithTag("Player").gameObject;
            targetBase = GameObject.FindGameObjectWithTag("PlayerBase").gameObject;
        }
        else if (gameObject.tag == "PlayerMinion")
        {

            targetBase = GameObject.FindGameObjectWithTag("EnemyBase").gameObject;
        }
        ChangeState(STATE.GoToOpponentBase);
        dest = DESTINATION.None;

        path = new NavMeshPath();
        NavMeshHit navHit;
        Vector3 testTarget = new Vector3(targetBase.transform.position.x, transform.position.y, targetBase.transform.position.z);
        bool pointFound = NavMesh.SamplePosition(testTarget, out navHit, 10.0f, NavMesh.AllAreas);
        // Debug.Log("pointFound: " + pointFound + " testTarget: " + testTarget +  " navHit: " + navHit.position);
        bool pathFound = NavMesh.CalculatePath(transform.position, navHit.position, NavMesh.AllAreas, path);
        // Debug.Log("pathFound: " + pathFound);
        // foreach (Vector3 corner in path.corners) Debug.Log(corner);
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
            case STATE.OrbitAround:
                OrbitAround();
                return;
            case STATE.Wander:
                Wander();
                return;
            case STATE.GoToOpponentBase:
                GoToOpponentBase();
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
            orbitTime = Random.Range(0f, maxOrbitTime);
            idleTimer = 0f;
            ChangeState(STATE.OrbitAround);
        }
        idleTimer += Time.deltaTime;

    }
    private void OrbitAround()
    {
        agent.isStopped = true;

        transform.RotateAround(target.transform.position, axis, orbitDirection * rotationSpeed * Time.deltaTime);
        //desiredRotation = new GameObject().transform;
        //desiredRotation.LookAt(target.transform.position);
        Quaternion neededRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, neededRotation, Time.deltaTime);
        //desiredPosition = (transform.position - target.transform.position).normalized * orbitRadius + target.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * orbitRadiusSpeed);

        if (orbitTimer >= orbitTime)
        {
            idleTime = Random.Range(0f, maxIdleTime);
            orbitTimer = 0f;
            Random random = new Random();
            do
            {
                orbitDirection = Random.Range(-1, 2);
            } while (orbitDirection == 0);
            Debug.Log("direction: " + orbitDirection);
            ChangeState(STATE.Idle);
        }
        orbitTimer += Time.deltaTime;
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
    private void GoToOpponentBase()
    {

        // if (path == null) return;
        // float distanceToTravel = 1f;
        // for (int i = 0; i < path.corners.Length - 1; i++)
        // {
        //     Debug.Log("GoToOpponentBase");
        //     float distance = Vector3.Distance(path.corners[i], path.corners[i + 1]);
        //     if (distance < distanceToTravel)
        //     {
        //         distanceToTravel -= distance;
        //         continue;
        //     }
        //     else
        //     {
        //         Vector3 pos = Vector3.Lerp(path.corners[i], path.corners[i + 1], distanceToTravel / distance);
        //         transform.position = pos;
        //         break;
        //     }
        // }

        if (agent == null || targetBase == null) return;
        if (dest != DESTINATION.OpponentBase)
        {
            agent.SetDestination(targetBase.transform.position);
            dest = DESTINATION.OpponentBase;
            Debug.Log("Setting destination - opponent base");
        }
        agent.isStopped = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.Log("OnDrawGizmosSelectedForLoop");
            Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
        }
    }

    private void Chase()
    {
        targetsInVision.RemoveAll(item => item == null);
        GameObject opponent = (gameObject.tag == "EnemyMinion") ? targetsInVision.Find(item => item.tag == "Player") : targetsInVision.Find(item => item.tag == "Enemy");
        GameObject opponentMinion = (gameObject.tag == "EnemyMinion") ? targetsInVision.Find(item => item.tag == "PlayerMinion") : targetsInVision.Find(item => item.tag == "EnemyMinion");

        if (opponent != null)
        {
            target = opponent;
        }
        else if(opponentMinion != null){
            target = opponentMinion;
        }
        else if (targetsInVision.Count > 0)
        {
            target = targetsInVision[0];
        }
        else
        {
            ChangeState(AIRootMovement.STATE.GoToOpponentBase);
        }
        if (target != null)
        {
            if (agent.destination != target.transform.position) agent.SetDestination(target.transform.position);
            agent.isStopped = false;
        }
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

    public void SetDestination(DESTINATION destination){
        dest = destination;
    }
    public DESTINATION GetDestination(){
        return dest;
    }
}