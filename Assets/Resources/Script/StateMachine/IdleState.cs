using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    float timer = 0f;
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnim("idle");
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if(timer > 2f)
        {
            bot.ChangeState(new RunningState());
        }

        bot.range.RemoveNullTarget();
        if(bot.range.charsInCircle.Count > 0 && !bot.isAttack)
        {
            bot.ChangeState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {
        
    }
}
