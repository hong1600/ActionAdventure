using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAcquireUI : MonoBehaviour
{
    TableLocalization localization;
    FadeUI fade;

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
        fade = UIManager.instance.Fade;
    }

    private void OnEnable()
    {
        Item.OnItemAcquired += SetSkillUI;
    }

    private void OnDisable()
    {
        Item.OnItemAcquired -= SetSkillUI;
    }

    public void SetSkillUI(TableSkill.Info _skill)
    {
        skillNameText.text = localization.Get(_skill.NameKey);
        skillDescText.text = localization.Get(_skill.DescKey);

        skillImg.sprite = SpriteManager.instance.GetSprite(_skill.SpriteName);

        StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        SetFade();

        acquirePanel.SetActive(true);

        StartCoroutine(fade.StartFadeIn(acquireImg, 2f, 0.8f));
        StartCoroutine(fade.StartFadeIn(skillImg, 3f));
        yield return StartCoroutine(fade.StartFadeIn(skillNameText, 3f));

        underLineImg.rectTransform.DOScaleX(1f, 1f).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(1f);

        StartCoroutine(fade.StartFadeIn(skillPushText, 3f));
        yield return StartCoroutine(fade.StartFadeIn(skillDescText, 3f));

        yield return new WaitForSeconds(3f);

        canvasGroup.DOFade(0, 2f);

        yield return new WaitForSeconds(2f);

        acquirePanel.SetActive(false);
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
