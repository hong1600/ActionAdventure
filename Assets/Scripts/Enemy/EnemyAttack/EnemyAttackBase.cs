using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    protected EnemyBase enemy;

    public bool isCooldown { get; private set; }
    public bool isAttacking { get; private set; }

    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float attackCoolTime = 1.2f;


    protected virtual void Awake()
    {
        enemy = GetComponent<EnemyBase>();
    }

    public virtual bool CanAttack(Transform _target)
    {
        if (_target == null) return false;

        float distance = Vector2.Distance(transform.position, _target.position);

        return distance <= attackRange;
    }

    public void Attack(Transform _target)
    {
        if (isAttacking || isCooldown) return;
        if(!CanAttack(_target)) return;

        StartCoroutine(AttackRoutine(_target));
    }

    IEnumerator AttackRoutine(Transform _target)
    {
        isAttacking = true;

        yield return StartAttack(_target);

        isAttacking = false;

        StartCoroutine(StartCooldown(attackCoolTime));
    }

    protected virtual IEnumerator StartAttack(Transform _target)
    {
        enemy.anim.Attack();

        yield return null;
    }

    protected IEnumerator StartCooldown(float _cooldown)
    {
        isCooldown = true;

        yield return new WaitForSeconds(_cooldown);

        isCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
