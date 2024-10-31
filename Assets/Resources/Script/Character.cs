using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbtractCharacter
{
    private float hp;
    public Animator animator;
    public string currentAnimName = "idle";
    public CharacterRange range;
    public bool isAttack = false;
    public bool isDeath = false;
    public TargetIndicator indicator;
    public int level = 1;
    public InitSkin skin;
    public HealthBar healthBar;
    public bool IsDeath => isDeath || hp <= 0; //chết khi mà hp = 0
    public Bullet bulletPrefabs;

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
        hp = 100;
        level = 1;
        SetBodyScale();
        indicator.InitTarget(level);
        bulletPrefabs = ItemDatabase.Instance.bullets[skin.weaponId];
        //UpdateBulletPrefabs(skin.weaponId);
    }

    //public void UpdateBulletPrefabs(int weaponId)
    //{
    //    if(ItemDatabase.Instance.bullets.Count > weaponId)
    //    {
    //        bulletPrefabs = ItemDatabase.Instance.bullets[weaponId];
    //        Debug.Log("Bullet prefab updated successfully");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Invalid weaponId, cannot update bullet prefab.");
    //    }
    //}

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
            Invoke(nameof(EnableWeapons), 1f);
        }
    }

    public void OnHit(float damage)
    {
        Debug.Log("On Hit");
        if (!IsDeath)
        {
            hp -= damage;
            if (IsDeath)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHp(hp);
        }
    }

    private void EnableWeapons()
    {
        skin.weaponItem.SetActive(true);
    }
}
