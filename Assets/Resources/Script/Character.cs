using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    public string currentAnimName = "idle";
    public CharacterRange range;
    public Bullet BulletPrefabs;

    //public Transform mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("aaa");
            Bullet bullet = Instantiate(BulletPrefabs);
            Vector3 direction = (range.GetNearestTargeet().position - transform.position).normalized;
            bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
        }
    }
}
