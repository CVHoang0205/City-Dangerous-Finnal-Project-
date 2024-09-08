using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbtractCharacter : MonoBehaviour
{
    public abstract void OnInit();
    public abstract void OnAttack();
    public abstract void OnDeath();
}
