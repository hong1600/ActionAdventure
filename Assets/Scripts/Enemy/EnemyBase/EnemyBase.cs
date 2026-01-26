using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, ITakeDmg
{
    Rigidbody2D rigid;
    CapsuleCollider2D cap;
    SpriteRenderer render;
    Material mat;
    public EnemyAnim anim { get; private set; }

    EnemyAttackBase attack;
    EnemyMovementBase movement;

    EnemyState enemyState;
    HitEffect hitEffect;
    HitLevelResolver hitLevelResolver;
    HitEffectTable hitEffectTable;

    [SerializeField] float moveSpeed;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] float searchRadius = 5f;
    [SerializeField] float attackReadyRadius = 1.5f;
    [SerializeField] Vector2 attackBoxSize = new Vector2(2f, 1f);

    [SerializeField] float attackCooldown = 1.2f;
    public bool IsCanAttack { get; private set; } = true;

    [SerializeField] float curHp;
    [SerializeField] float maxHp;

    bool isDie = false;

    public Transform target { get; private set; }


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<EnemyAnim>();
        render = GetComponent<SpriteRenderer>();
        mat = render.material;

        attack = GetComponent<EnemyAttackBase>();
        movement = GetComponent<EnemyMovementBase>();
    }

    private void Start()
    {
        enemyState = new EnemyState();
        enemyState.Init(this);

        curHp = maxHp;

        hitEffect = GameManager.instance.CombatManager.HitEffect;
        hitLevelResolver = new HitLevelResolver();
        hitEffectTable = GameManager.instance.CombatManager.HitEffectTable;
    }

    private void FixedUpdate()
    {
        if (isDie || hitEffect.IsKnockBack) return;

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
        IsCanAttack = false;

        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        Vector2 dir = (target.position - transform.position).normalized;

        yield return new WaitForSeconds(attackCooldown);

        IsCanAttack = true;
    }

    public void OnAttackFinished()
    {
        enemyState.SetState(new EnemyAttackWaitState(enemyState));
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

            anim.Hit();

            HitContext ctx = new HitContext();
            ctx.isCritical = true;
            ctx.isFinish = curHp <= 0;

            EHitLevel level = hitLevelResolver.Resolve(ctx);

            HitEffectData data = hitEffectTable.Get(level);
            if (data != null)
            {
                HitTransformContext trsCtx = new HitTransformContext();

                trsCtx.attacker = _attacker;
                trsCtx.target = transform;

                hitEffect.ApplyHitEffect(data, trsCtx);
            }

            if (curHp <= 0)
            {
                StartCoroutine(StartDie());
            }
        }
    }

    IEnumerator StartDie()
    {
        anim.Die();
        isDie = true;

        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }
}
