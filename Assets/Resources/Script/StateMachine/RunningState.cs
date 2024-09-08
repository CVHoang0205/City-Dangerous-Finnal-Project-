using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunningState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("run");
        bot.SetDestination(SeekTarget());
    }

    public void OnExecute(Bot bot)
    {
        if (bot.isDestination)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }

    private Vector3 SeekTarget() 
    {
        for(int i = 0; i < 10; i++)
        {
            NavMeshHit hit;
            Vector3 target = new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 40f));
            if(NavMesh.SamplePosition(target, out hit, 2.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                Debug.Log("Unavalaible");
            }
        }
        return Vector3.zero;
    }
}
