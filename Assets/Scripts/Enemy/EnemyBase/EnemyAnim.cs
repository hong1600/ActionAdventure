using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartMove()
    {
        anim.SetBool("isMove", true);
    }

    public void StopMove()
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
    }
}
