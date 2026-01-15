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

    GameObject playerObj;

    public EPlayerState curState { get; private set; } = EPlayerState.NONE;

    [SerializeField] int curHp = 5;
    int maxHp = 5;

    [SerializeField] int curMp = 0;
    int maxMp = 10;

    [SerializeField] float knockBackPower;

    [SerializeField] float hitStopSec;

    bool isDie = false;
    public bool IsDie { get {  return isDie; } }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        playerSpawner = GameManager.instance.PlayerSpawner;
        hitEffect = GameManager.instance.HitEffect;
        hitEffect.Init(rigid, render, knockBackPower);
    }

    private void Start()
    {
        playerSpawner.onSpawnEvent += Init;
    }

    private void Init()
    {
        playerObj = playerSpawner.playerObj;
    }

    public void TakeDmg(int _dmg, Transform _attacker)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;

            onHpEvent?.Invoke(curHp, maxHp);

            hitEffect.ApplyHitEffect(_attacker, transform, hitStopSec, EEffect.PLAYERHITEFFECT, ESfx.HIT);

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

