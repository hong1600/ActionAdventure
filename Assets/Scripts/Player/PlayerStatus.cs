using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ITakeDmg
{
    public event Action<int, int> onHpEvent;
    public event Action<int, int> onMpEvent;

    Animator anim;
    SpriteRenderer render;

    PlayerSpawner playerSpawner;

    GameObject playerObj;

    [SerializeField] Material whiteMat;
    Material originMat;

    public EPlayerState curState { get; private set; } = EPlayerState.NONE;

    [SerializeField] int curHp = 5;
    int maxHp = 5;

    [SerializeField] int curMp = 0;
    int maxMp = 10;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        playerSpawner = GameManager.instance.PlayerSpawner;

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

    public void TakeDmg(int _dmg)
    {
        if (curHp > 0)
        {
            curHp -= _dmg;

            onHpEvent?.Invoke(curHp, maxHp);


            if(curHp <= 0)
            {
                StartCoroutine(StartDie());
            }
        }
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
        anim.SetTrigger("IsDie");
        curState = EPlayerState.DIE;

        yield return new WaitForSeconds(1.37f);

        GameUI.instance.DieFade();
        playerObj.SetActive(false);
    }
}

