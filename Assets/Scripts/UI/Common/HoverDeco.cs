using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverDeco : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform hoverDeco;
    [SerializeField] RectTransform leftHoverDeco;
    [SerializeField] RectTransform rightHoverDeco;

    [SerializeField] float spacing = 20f;

    private void Update()
    {
        if (!hoverDeco.gameObject.activeSelf) return;

        if(Input.GetMouseButtonDown(0)) 
        {
            hoverDeco.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Transform target = eventData.pointerEnter.transform;

        RectTransform btnRect = target.GetComponent<RectTransform>();

        hoverDeco.position = target.position;

        float halfWidth = btnRect.rect.width * 0.5f;

        leftHoverDeco.anchoredPosition = new Vector2(-halfWidth - spacing, 0f);

        rightHoverDeco.anchoredPosition = new Vector2(halfWidth + spacing, 0f);

        hoverDeco.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverDeco.gameObject.SetActive(false);
    }

    public void HideHover()
    {
        hoverDeco.gameObject.SetActive(false);
    }
}
