using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] CanvasGroup selectCanvasGroup;

    public void ClickStartBtn()
    {
        StartCoroutine(StartShowSelect());
    }

    public void ClickBackBtn()
    {
        StartCoroutine(StartShowMain());
    }

    IEnumerator StartShowSelect()
    {
        mainCanvasGroup.alpha = 1f;
        selectCanvasGroup.alpha = 0f;

        mainCanvasGroup.DOFade(0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        mainCanvasGroup.gameObject.SetActive(false);

        selectCanvasGroup.gameObject.SetActive(true);

        selectCanvasGroup.DOFade(1f, 0.5f);
    }

    IEnumerator StartShowMain()
    {
        mainCanvasGroup.alpha = 0f;
        selectCanvasGroup.alpha = 1f;

        selectCanvasGroup.DOFade(0f, 0.5f);

        yield return new WaitForSeconds(0.5f);

        selectCanvasGroup.gameObject.SetActive(false);

        mainCanvasGroup.gameObject.SetActive(true);

        mainCanvasGroup.DOFade(1f, 0.5f);
    }
}
