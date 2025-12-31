using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, ITakeDmg
{
    Rigidbody2D rigid;
    CapsuleCollider2D cap;
    SpriteRenderer sprite;
    Material mat;

    EnemyState enemyState;
    KnockBack knockBack;

    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] float searchRadius = 5f;
    [SerializeField] float attackReadyRadius = 1.5f;
    [SerializeField] Vector2 attackBoxSize = new Vector2(2f, 1f);

    [SerializeField] float curHp;
    [SerializeField] float maxHp;

    [SerializeField] float knockBackPower;

    bool isDie = false;

    public Transform target { get; private set; }

    bool isKnockBack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        mat = sprite.material;

        enemyState = new EnemyState();
        knockBack = new KnockBack();

        enemyState.Init(this);
        knockBack.Init(rigid);
    }

    private void Start()
    {
        curHp = maxHp;
    }

    private void FixedUpdate()
    {
        if (isDie || isKnockBack) return;

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

    public void TakeDmg(int _dmg, Transform _attacker)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;

            StartCoroutine(StartKnockBack(_attacker));

            if (curHp <= 0)
            {
                StartCoroutine(StartDie());
            }
        }
    }

    IEnumerator StartKnockBack(Transform _attacker)
    {
        isKnockBack = true;

        knockBack.Apply(transform, _attacker, knockBackPower);

        yield return new WaitForSeconds(0.15f);

        rigid.velocity = Vector2.zero;

        isKnockBack = false;
    }

    IEnumerator StartDie()
    {
        isDie = true;

        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }
}
