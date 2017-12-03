using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRootMovement : MonoBehaviour
{

    public enum STATE
    {
        Idle,
        Wander,
        Chase,
    }
    public STATE currentState;

    // Use this for initialization
    void Start()
    {
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

    }
    private void Wander()
    {

    }
    private void Chase()
    {

    }
}