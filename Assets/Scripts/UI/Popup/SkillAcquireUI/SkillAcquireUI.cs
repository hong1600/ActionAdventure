using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillAcquireUI : MonoBehaviour
{
    TableLocalization localization;
    FadeUI fade;
    GameState gameState;

    [SerializeField] GameObject acquirePanel;
    Image acquireImg;
    [SerializeField] Image skillImg;
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;
    [SerializeField] TextMeshProUGUI skillPushText;
    [SerializeField] Image underLineImg;
    CanvasGroup canvasGroup;

    Coroutine fadeIn;
    Coroutine fadeOut;

    private void Awake()
    {
        acquireImg = acquirePanel.GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        localization = DataManager.instance.TableLocalization;
        fade = UIManager.instance.GamePanel.Fade;

        SkillManager.instance.onSkillItemAcquired += SetSkillUI;

        gameState = GameManager.instance.GameState;
    }

    private void OnDestroy()
    {
        SkillManager.instance.onSkillItemAcquired -= SetSkillUI;
    }

    public void SetSkillUI(ItemData _data)
    {
        skillNameText.text = localization.Get(_data.itemName);
        skillDescText.text = localization.Get(_data.itemDesc);

        skillImg.sprite = _data.itemImg;

        gameState.SetState(EGameState.DONTMOVE);

        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        SetFade();

        acquirePanel.SetActive(true);

        StartCoroutine(fade.StartFadeIn(acquireImg, 2f, 0.99f));
        StartCoroutine(fade.StartFadeIn(skillImg, 2f));
        yield return StartCoroutine(fade.StartFadeIn(skillNameText, 2f));

        underLineImg.rectTransform.DOScaleX(1f, 1f).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(1f);

        StartCoroutine(fade.StartFadeIn(skillPushText, 2f));
        yield return StartCoroutine(fade.StartFadeIn(skillDescText, 2f));

        yield return new WaitForSeconds(2f);

        canvasGroup.DOFade(0, 1f);

        yield return new WaitForSeconds(1f);

        acquirePanel.SetActive(false);

        gameState.SetState(EGameState.PLAY);
    }

    private void SetFade()
    {
        acquireImg.color = new Color(0, 0, 0, 0);
        skillImg.color = new Color(1, 1, 1, 0);
        skillNameText.color = new Color(1, 1, 1, 0);
        skillPushText.color = new Color(1, 1, 1, 0);
        skillDescText.color = new Color(1, 1, 1, 0);
        canvasGroup.alpha = 1;

        underLineImg.rectTransform.localScale = new Vector3(0f, 1f, 1f);
    }
}
