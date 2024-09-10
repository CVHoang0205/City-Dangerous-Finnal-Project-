using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbtractCharacter
{
    public Animator animator;
    public string currentAnimName = "idle";
    public CharacterRange range;
    public bool isAttack = false;
    public bool isDeath = false;
    public TargetIndicator indicator;
    public int level = 1;

    public Bullet bulletPrefabs;

    //public Transform mesh;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInit()
    {
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
    }

    public override void OnAttack()
    {
        Throw();
    }

    public override void OnDeath()
    {
        GameController.Instance.CharacterDead();
        isDeath = true;
        ChangeAnim("dead");
        gameObject.tag = "Untagged";
    }

    public void SetBodyScale()
    {
        transform.localScale = (1 + (level - 1) * 0.1f) * Vector3.one;
    }

    public void GainLevel()
    {
        if (!isDeath)
        {
            level++;
            SetBodyScale();
            indicator.InitTarget(level);
        }
    }

    public void ChangeAnim(string animName)
    {
        if(currentAnimName != animName)
        {
            animator.ResetTrigger(animName);
            currentAnimName = animName;
            animator.SetTrigger(currentAnimName);
        }
    }

    public void Throw() 
    {
        range.RemoveNullTarget();
        if (range.charsInCircle.Count > 0) 
        {
            Bullet bullet = Instantiate(bulletPrefabs);
            bullet.transform.position = transform.position;
            //bullet.transform.position = transform.position + Vector3.up * 1;
            bullet.self = this;
            Vector3 direction = (range.GetNearestTarget().position - transform.position).normalized;
            bullet.transform.forward = direction;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
            transform.forward = direction;
        }
    }
}
