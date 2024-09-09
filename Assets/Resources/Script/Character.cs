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

    public Bullet bulletPrefabs;

    //public Transform mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInit()
    {
        
    }

    public override void OnAttack()
    {
        Throw();
    }

    public override void OnDeath()
    {
        isDeath = true;
        ChangeAnim("dead");
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
