using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class ChaseState : State<AI>
{
    private static ChaseState _instance;
    private NavMeshAgent agent;
    private GameObject target;

    private ChaseState()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static ChaseState Instance
    {
        get
        {
            if(_instance == null)
            {
                new ChaseState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        agent = _owner.GetComponent<NavMeshAgent> ();
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    public override void ExitState(AI _owner)
    {
        
    }

    public override void UpdateState(AI _owner)
    {
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
    }

}
