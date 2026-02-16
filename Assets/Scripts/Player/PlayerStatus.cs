using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ITakeDmg
{
    public event Action<int, int> onHpEvent;
    public event Action<int, int> onMpEvent;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer render;

    PlayerSpawner playerSpawner;
    HitEffect hitEffect;
    public HitEffect HitEffect { get { return hitEffect; } }
    HitLevelResolver hitLevelResolver;
    HitEffectTable hitEffectTable;
    HitFlash hitFlash;

    GameObject playerObj;

    public EPlayerState curState { get; private set; } = EPlayerState.PLAY;

    [SerializeField] int curHp = 5;
    int maxHp = 5;

    [SerializeField] int curMp = 0;
    int maxMp = 10;

    bool isDie = false;
    public bool IsDie { get {  return isDie; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        hitFlash = GetComponent<HitFlash>();
    }

    private void Start()
    {
        playerSpawner = GameManager.instance.PlayerSpawner;
        playerSpawner.onSpawnEvent += Init;

        hitEffect = GameManager.instance.CombatManager.HitEffect;
        hitLevelResolver = new HitLevelResolver();
        hitEffectTable = GameManager.instance.CombatManager.HitEffectTable;
    }

    private void Init()
    {
        playerObj = playerSpawner.playerObj;
    }

    public void TakeDmg(int _dmg, Transform _attacker)
    {
        if (hitEffect.isInvincible) return;

        hitEffect.Invincible();

        if (curHp > 0)
        {
            curHp -= _dmg;

            onHpEvent?.Invoke(curHp, maxHp);

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

    private void FillMp(int _amount)
    {
        if (curMp < 10)
        {
            curMp += _amount;

            onMpEvent?.Invoke(curMp, maxMp);
        }
        else
        {
            return;
        }
    }

    private bool UseMp(int _amount)
    {
        if (curMp > 0)
        {
            curMp -= _amount;

            onMpEvent?.Invoke(curMp, maxMp);

            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator StartDie()
    {
        isDie = true;
        rigid.velocity = Vector3.zero;

        anim.SetTrigger("IsDie");
        curState = EPlayerState.DIE;

        yield return new WaitForSeconds(1.37f);

        GameUI.instance.DieFade();
        gameObject.SetActive(false);
    }
}

