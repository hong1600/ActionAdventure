using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D cap;

    EnemyState enemyState;

    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] float searchRadius = 5f;
    [SerializeField] float attackReadyRadius = 1.5f;
    [SerializeField] Vector2 attackBoxSize = new Vector2(2f, 1f);

    public Transform target { get; private set; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();

        enemyState = new EnemyState();

        enemyState.Init(this);
    }

    private void FixedUpdate()
    {
        if (enemyState != null)
        {
            enemyState.Update();
        }
    }

    public bool SearchTarget()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, searchRadius, Vector2.zero, 0f, targetLayer);

        if (hit.collider != null)
        {
            target = hit.transform;
            return true;
        }

        target = null;
        return false;
    }

    public void Move()
    {
        if(target == null) 
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        Vector2 dir = (target.position - transform.position).normalized;
        Turn(dir);

        rigid.velocity = dir * moveSpeed;
    }

    private void Turn(Vector2 _dir)
    {
        if (_dir.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_dir.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public bool ReadyAttack()
    {
        if (target == null)
            return false;

        float distance = Vector2.Distance(transform.position, target.position);

        return distance <= attackReadyRadius;
    }

    public void StopMove()
    {
        rigid.velocity = Vector2.zero;
    }

    public void Attack()
    {
        Vector2 dir = (target.position - transform.position).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackReadyRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, attackBoxSize);
    }
}
