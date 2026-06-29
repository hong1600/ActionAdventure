using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum EEnemyType
{
    NONE,
    FLY,
    JUMPER,
    WALKER,
    ELITE,
    BOSS
}

public abstract class EnemyBase : MonoBehaviour, ITakeDmg
{
    public EnemyAnim anim { get; private set; }
    EnemyState enemyState;

    public EnemyAttackBase attack { get; private set; }
    public EnemyMovementBase movement { get; private set; }

    HitEffect hitEffect;
    HitLevelResolver hitLevelResolver;
    HitEffectTable hitEffectTable;
    HitFlash hitFlash;
    EnemyDrop enemyDrop;

    [SerializeField] LayerMask targetLayer;
    [SerializeField] float searchRadius = 5f;

    [SerializeField] float curHp;
    [SerializeField] float maxHp;

    bool isDie = false;

    public Transform target { get; private set; }


    [SerializeField] EEnemyType enemyType;
    public EEnemyType EnemyType { get { return enemyType; } }


    private void Awake()
    {
        anim = GetComponent<EnemyAnim>();

        attack = GetComponent<EnemyAttackBase>();
        movement = GetComponent<EnemyMovementBase>();
        enemyDrop = GetComponent<EnemyDrop>();
    }

    private void Start()
    {
        enemyState = new EnemyState();
        enemyState.Init(this);

        curHp = maxHp;

        hitEffect = GameManager.instance.CombatManager.HitEffect;
        hitLevelResolver = new HitLevelResolver();
        hitEffectTable = GameManager.instance.CombatManager.HitEffectTable;
        hitFlash = GetComponent<HitFlash>();
    }

    private void FixedUpdate()
    {
        if (isDie || hitEffect.IsKnockBack) return;

        if (enemyState != null)
        {
            enemyState.Update();
        }
    }

    public void MoveToTarget()
    {
        if (target == null) 
        {
            movement.Stop();
            return;
        }

        Vector2 dir = (target.position - transform.position).normalized;
        movement.Move(dir);
    }

    public void StopMove()
    {
        movement.Stop();
    }

    public void TryAttack()
    {
        attack.Attack(target);
    }

    public bool IsCanAttack()
    {
        return attack.CanAttack(target);
    }

    public bool IsAttacking()
    {
        return attack.isAttacking;
    }

    public virtual bool HasAttackState()
    {
        return true;
    }

    public bool IsSearchTarget()
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

    public void TakeDmg(int _dmg, Transform _attacker)
    {
        if (hitEffect.isInvincible) return;

        hitEffect.Invincible();

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
                trsCtx.hitFlash = hitFlash;

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
        enemyDrop.Drop(transform.position);

        yield return new WaitForSeconds(1f);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
