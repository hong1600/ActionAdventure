using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ITakeDmg
{
    public event Action<int, int> onHpEvent;
    public event Action<int, int> onMpEvent;

    Animator anim;
    SpriteRenderer render;
    Rigidbody2D rigid;

    PlayerSpawner playerSpawner;
    KnockBack knockBack;

    GameObject playerObj;

    [SerializeField] Material whiteMat;
    Material originMat;

    public EPlayerState curState { get; private set; } = EPlayerState.NONE;

    [SerializeField] int curHp = 5;
    int maxHp = 5;

    [SerializeField] int curMp = 0;
    int maxMp = 10;

    [SerializeField] float knockBackPower;
    bool isKnockBack;
    public bool IsKnockBack { get { return isKnockBack; } }

    bool isDie = false;
    public bool IsDie { get {  return isDie; } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        playerSpawner = GameManager.instance.PlayerSpawner;
        knockBack = new KnockBack();

        knockBack.Init(rigid);

        originMat = render.sharedMaterial;
    }

    private void Start()
    {
        playerSpawner.onSpawnEvent += Init;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeHitColor();
        }
    }

    private void Init()
    {
        playerObj = playerSpawner.playerObj;
    }

    public void TakeDmg(int _dmg, Transform _attacker)
    {
        if (isKnockBack) return;

        if (curHp > 0)
        {
            curHp -= _dmg;

            onHpEvent?.Invoke(curHp, maxHp);

            StartCoroutine(StartKnockBack(_attacker));
            ObjectPoolManager.instance.EffectPool.FindEffect(EEffect.PLAYERHITEFFECT, transform.position, Quaternion.identity);

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

        isKnockBack = false;
    }

    private void ChangeHitColor()
    {
        StopAllCoroutines();
        StartCoroutine(StartChangeColor());
    }

    IEnumerator StartChangeColor()
    {
        render.sharedMaterial = whiteMat;

        yield return new WaitForSeconds(0.1f);

        render.sharedMaterial = originMat;
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

