using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector3 nextPoint;
    public LayerMask groundLayer;

    private CounterTime counter = new CounterTime();
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        nextPoint = transform.position + JoystickControl.direct * Time.deltaTime * 5f;
        if(CheckGround(nextPoint) && JoystickControl.direct.magnitude > 0f)
        {
            counter.Cancel();
            transform.position = nextPoint;
            transform.forward = JoystickControl.direct;
            ChangeAnim("run");
        }
        else if(!isAttack)
        {
            ChangeAnim("idle");
            range.RemoveNullTarget();
            if(range.charsInCircle.Count > 0)
            {
                AttackTarget();
            }
        }
        else
        {
            counter.Execute();
        }
    }

    public override void OnInit()
    {
        isDeath = false;
        ChangeAnim("idle");
        base.OnInit();
    }

    public void AttackTarget()
    {
        isAttack = true;
        Invoke(nameof(ChangeIsAttack), 1.5f);
        ChangeAnim("attack");
        counter.Start(OnAttack, 0.3f);
    }

    public void ChangeIsAttack()
    {
        isAttack = false; 
    }

    public override void OnDeath()
    {
        counter.Cancel();
        this.enabled = false;
        base.OnDeath();
    }

    private bool CheckGround(Vector3 points)
    {
        RaycastHit hit;
        return Physics.Raycast(points + Vector3.up * 2, Vector3.down, out hit, groundLayer);
    }

    public void OnDespawn()
    {
        counter.Cancel();
    }
}
