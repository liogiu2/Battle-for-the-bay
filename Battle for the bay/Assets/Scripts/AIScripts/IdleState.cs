using UnityEngine;
using StateStuff;
using UnityEngine.AI;

public class IdleState : State<AI>
{
    private static IdleState _instance;

    private NavMeshAgent agent;
    private IdleState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        agent = _owner.GetComponent<NavMeshAgent> ();
    }

    public override void ExitState(AI _owner)
    {

    }

    public override void UpdateState(AI _owner)
    {
        agent.isStopped = true;
    }
}
