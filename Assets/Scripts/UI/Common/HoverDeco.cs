using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverDeco : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform hoverDeco;
    [SerializeField] RectTransform leftHoverDeco;
    [SerializeField] RectTransform rightHoverDeco;

    [SerializeField] float spacing = 20f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Transform target = eventData.pointerEnter.transform;

        TextMeshProUGUI text = target.GetComponentInChildren<TextMeshProUGUI>();

        RectTransform textRect = text.rectTransform;

        hoverDeco.position = target.position;

        float halfWidth = textRect.rect.width * 0.5f;

        leftHoverDeco.anchoredPosition = new Vector2(-halfWidth - spacing, 0f);

        rightHoverDeco.anchoredPosition = new Vector2(halfWidth + spacing, 0f);

        hoverDeco.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverDeco.gameObject.SetActive(false);
    }
}
