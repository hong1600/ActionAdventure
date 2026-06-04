using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EInteractionText
{
    INVESTIGATE,
    ACQUIRE
}

public class InteractionUI : MonoBehaviour
{
    [SerializeField] GameObject interactionObj;

    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] Image interactionImg;

    [SerializeField] float effectDuration = 0.25f;

    Dictionary<EInteractionText, string> textDic = new Dictionary<EInteractionText, string>();

    Tween curTween;

    private void Awake()
    {
        textDic.Add(EInteractionText.INVESTIGATE, "Á¶»ç");
        textDic.Add(EInteractionText.ACQUIRE, "È¹µæ");
    }

    public void Show(EInteractionText _type, Transform _target, Vector3 _offset)
    {
        if (curTween != null) curTween.Kill(true);

        interactionText.transform.position = _target.position + _offset;

        string text;
        if (textDic.TryGetValue(_type, out text)) interactionText.text = text;

        interactionObj.SetActive(true);

        interactionText.rectTransform.localScale = new Vector3(0f, 1f, 1f);
        interactionImg.color = new Color(1, 1, 1, 0);
        interactionText.color = new Color(1, 1, 1, 0);

        Sequence seq = DOTween.Sequence();

        seq.Append(interactionText.rectTransform.DOScaleX(1f, effectDuration).SetEase(Ease.OutQuad));

        seq.Join(interactionImg.DOFade(1f, effectDuration));
        seq.Join(interactionText.DOFade(1f, effectDuration));

        curTween = seq;
    }

    public void Hide()
    {
        if (curTween != null) curTween.Kill(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(interactionText.rectTransform.DOScaleX(0f, effectDuration).SetEase(Ease.InBack));

        seq.Join(interactionImg.DOFade(0f, effectDuration));
        seq.Join(interactionText.DOFade(0f, effectDuration));

        seq.OnComplete(CompleteHide);

        curTween = seq;
    }

    private void CompleteHide()
    {
        interactionObj.SetActive(false);
    }
}
