using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ITakeDmg
{
    public event Action<int, int> onHpEvent;

    Animator anim;

    PlayerSpawner playerSpawner;

    GameObject playerObj;

    public EPlayerState curState { get; private set; } = EPlayerState.NONE;

    public int curHp { get; private set; } = 5;
    int maxHp = 5;


    private void Awake()
    {
        anim = GetComponent<Animator>();

        playerSpawner = GameManager.instance.PlayerSpawner;

        playerSpawner.onSpawnEvent += Init;
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDmg(1);
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

