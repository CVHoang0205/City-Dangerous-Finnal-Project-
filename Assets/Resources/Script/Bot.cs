using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;
    public IState<Bot> currentState;
    [SerializeField] GameObject Coin;
    public bool isDestination => Vector3.Distance(destination, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim("idle");
        skin.RandomEquipItems();
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
        GameController.Instance.botInStage.Remove(this);    
        base.OnDeath();
        StartCoroutine(DestroyBot());
    }

    IEnumerator DestroyBot()
    {
        yield return new WaitForSeconds(2f);
        Destroy(indicator.gameObject);
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
        Vector3 coinPoint = new Vector3(0, 1f, 0);
        Instantiate(Coin, transform.position + coinPoint, Quaternion.LookRotation(Vector3.up));
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

    //public void DestroyCoin()
    //{
    //    if(Coin != null)
    //    {
    //        Destroy(Coin.gameObject);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Coin không tồn tại trong scene hoặc đã bị hủy.");
    //    }
    //}
}
