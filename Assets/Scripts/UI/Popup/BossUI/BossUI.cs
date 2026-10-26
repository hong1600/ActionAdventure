using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class BossUI : MonoBehaviour
{
    BossEnemy boss;

    [SerializeField] TextMeshProUGUI bossNameText;

    [Header("Boss Hp")]
    [SerializeField] GameObject bossHp;
    [SerializeField] Image hpFill;
    [SerializeField] Image smoothHpFill;
    [SerializeField] float smoothSpeed;

    private void OnDisable()
    {
        if (boss != null)
        {
            boss.onTakeDamage -= UpdateHpFill;
        }
    }

    public void InitHp(BossEnemy _boss)
    {
        if(boss != null) 
        {
            boss.onTakeDamage -= UpdateHpFill;
        }

        boss = _boss;
        boss.onTakeDamage += UpdateHpFill;

        float hp = boss.CurHp / boss.MaxHp;

        hpFill.fillAmount = hp;
        smoothHpFill.fillAmount = hp;
    }

    public void UpdateHpFill()
    {
        float curHp = boss.CurHp;
        float maxHp = boss.MaxHp;

        hpFill.fillAmount = curHp / maxHp;

        StopAllCoroutines();
        StartCoroutine(StartSmoothHp());
    }

    IEnumerator StartSmoothHp()
    {
        while (hpFill.fillAmount < smoothHpFill.fillAmount)
        {
            smoothHpFill.fillAmount = Mathf.Lerp(smoothHpFill.fillAmount, hpFill.fillAmount, smoothSpeed * Time.deltaTime);

            if (Mathf.Abs(smoothHpFill.fillAmount - hpFill.fillAmount) < 0.01f)
            {
                smoothHpFill.fillAmount = hpFill.fillAmount;
                yield break;
            }

            yield return null;
        }
    }

    public void ShowHpBar()
    {
        bossHp.SetActive(true);
    }

    public void ShowNameText()
    {
        //bossNameText.text = boss.BossName;

        bossNameText.alpha = 0f;

        bossNameText.gameObject.SetActive(true);

        bossNameText.DOFade(1f, 2f);
    }

    public IEnumerator HideNameText()
    {
        bossNameText.DOFade(0f, 2f);

        yield return new WaitForSeconds(2f);

        bossNameText.gameObject.SetActive(false);
    }
}
