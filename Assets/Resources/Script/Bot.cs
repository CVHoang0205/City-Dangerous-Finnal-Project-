using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;
    public IState<Bot> currentState;
    public bool isDestination => Vector3.Distance(destination, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) 
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        ChangeState(new IdleState());
        base.OnInit();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDeath()
    {
        ChangeState(null);
        agent.enabled = false;
        base.OnDeath();
    }

    public void ChangeIsAttackBot()
    {
        Invoke(nameof(ResetAttack), 1.5f);
    }

    private void ResetAttack()
    {
        isAttack = false;
    }

    public void ChangeState(IState<Bot> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null) 
        {
            currentState.OnEnter(this); 
        }
    }

    public void SetDestination(Vector3 des)
    {
        agent.enabled = true;
        destination = des;
        agent.SetDestination(des);
        destination.y = 0f;
    }
}
