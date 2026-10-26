using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    [SerializeField] BossEnemy boss;
    BossUI bossUI;

    [SerializeField] GameObject particle;

    bool isStarted;

    private void Start()
    {
        bossUI = GameUI.instance.BossUI;
    }

    public void BossBattle()
    {
        if (isStarted)
            return;

        isStarted = true;

        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        GameManager.instance.GameState.SetState(EGameState.DONTMOVE);
        CameraManager.instance.CameraBounds.ChangeBounds(EMap.BOSSROOM);

        boss.StopBattle();

        yield return StartCoroutine(StartBossIntro());

        StartBossBattle();
    }

    IEnumerator StartBossIntro()
    {
        float timer = 0f;
        float moveTime = 2f;

        while (timer < moveTime) 
        {
            boss.movement.Move(Vector2.left);

            timer += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        boss.movement.Stop();

        yield return new WaitForSeconds(2f);

        particle.SetActive(true);

        CameraManager.instance.CameraShake.ShakeCamera(3f);

        boss.anim.PlayAnim(EEnemyAnim.INTRO);

        AudioManager.instance.PlayBgm(EBgm.BOSS);

        bossUI.ShowNameText();

        yield return StartCoroutine(bossUI.HideNameText());
    }

    private void StartBossBattle()
    {
        bossUI.ShowHpBar();
        bossUI.InitHp(boss);

        GameManager.instance.GameState.SetState(EGameState.PLAY);

        boss.StartBattle();

        gameObject.GetComponent<BossRoomController>().enabled = false;
    }
}
