using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    EnemyBase owner;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        owner = GetComponent<EnemyBase>();
    }

    public void StartMove()
    {
        anim.SetBool("isMove", true);
    }

    public void EndMove()
    {
        anim.SetBool("isMove", false);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }

    public void DoDashDamage() 
    {
        owner.OnAttackFinished();
    }
}
