using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class WanderState : State<AI>
{
    private static WanderState _instance;

    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent agent;
    private float timer;


    private WanderState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static WanderState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WanderState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log(_owner.gameObject.name + " Entering WanderState State");
        wanderTimer = 10f;
        wanderRadius = 10f;
        agent = _owner.gameObject.GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting WanderState State");
    }

    public override void UpdateState(AI _owner)
    {
        // Debug.Log(_owner.gameObject.name +  " is inside WanderState State");
        Debug.Log(_owner.gameObject.name + " destination is: " + agent.destination);
        // Debug.Log(_owner.gameObject.name +  " position is: " + _owner.transform.position);

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(_owner, _owner.transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            agent.isStopped = false;
            timer = 0;
        }
    }

    Vector3 RandomNavSphere(AI owner, Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        // Debug.Log(owner + " randirection is: " + randDirection);

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
