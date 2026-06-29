using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPanelAnimType
{
    FADE,
    SCALE
}

public class PanelUI : MonoBehaviour
{
    [SerializeField] FadeUI fade;

    Stack<CanvasGroup> panelStack = new Stack<CanvasGroup>();

    bool isAnim = false;

    public void Init()
    {
        panelStack.Clear();
    }

    public void OpenPanel(CanvasGroup _panel, EPanelAnimType _animType)
    {
        if (isAnim) return;

        isAnim = true;
        StartCoroutine(StartOpenPanel(_panel, _animType));
    }

    IEnumerator StartOpenPanel(CanvasGroup _panel, EPanelAnimType _animType)
    {
        if (panelStack.Count > 0)
        {
            CanvasGroup curPanel = panelStack.Peek();

            switch (_animType) 
            {
                case EPanelAnimType.FADE:
                    yield return StartCoroutine(StartFadeClosePanel(curPanel, 0.3f));
                    break;
                case EPanelAnimType.SCALE:
                    yield return StartCoroutine(StartScaleClosePanel(curPanel, 0.3f));
                    break;
            }
        }

        panelStack.Push(_panel);

        switch (_animType)
        {
            case EPanelAnimType.FADE:
                yield return StartCoroutine(StartFadeOpenPanel(_panel, 0.3f));
                break;
            case EPanelAnimType.SCALE:
                yield return StartCoroutine(StartScaleOpenPanel(_panel, 0.3f));
                break;
        }
    }

    public void ClosePanel(EPanelAnimType _animType, Action _onClose = null)
    {
        if (isAnim) return;

        isAnim = true;
        StartCoroutine(StartClosePanel(_animType, _onClose));
    }

    IEnumerator StartClosePanel(EPanelAnimType _animType, Action _onClose = null)
    {
        if (panelStack.Count == 0) yield break;

        CanvasGroup curPanel = panelStack.Pop();

        switch (_animType)
        {
            case EPanelAnimType.FADE:
                yield return StartCoroutine(StartFadeClosePanel(curPanel, 0.3f));
                break;
            case EPanelAnimType.SCALE:
                yield return StartCoroutine(StartScaleClosePanel(curPanel, 0.3f));
                break;
        }

        _onClose?.Invoke();

        if (panelStack.Count > 0)
        {
            CanvasGroup prevPanel = panelStack.Peek();

            switch (_animType)
            {
                case EPanelAnimType.FADE:
                    yield return StartCoroutine(StartFadeOpenPanel(prevPanel, 0.3f));
                    break;
                case EPanelAnimType.SCALE:
                    yield return StartCoroutine(StartScaleOpenPanel(prevPanel, 0.3f));
                    break;
            }
        }
    }

    private IEnumerator StartFadeOpenPanel(CanvasGroup _panel, float _duration)
    {
        _panel.alpha = 0f;
        _panel.gameObject.SetActive(true);

        Tween tween = _panel.DOFade(1f, _duration).SetUpdate(true).OnComplete(OnOpenComplete);

        yield return tween.WaitForCompletion();

        OnOpenComplete();
    }

    private IEnumerator StartFadeClosePanel(CanvasGroup _panel, float _duration)
    {
        Tween tween = _panel.DOFade(0f, _duration).SetUpdate(true);

        yield return tween.WaitForCompletion();

        OnCloseComplete(_panel);

        _panel.alpha = 0f;
    }

    private IEnumerator StartScaleOpenPanel(CanvasGroup _panel, float _duration)
    {
        _panel.gameObject.SetActive(true);

        _panel.transform.localScale = Vector3.zero;

        Tween tween = _panel.transform.DOScale(Vector3.one, _duration).SetUpdate(true);

        yield return tween.WaitForCompletion();

        OnOpenComplete();
    }

    private IEnumerator StartScaleClosePanel(CanvasGroup _panel, float _duration)
    {
        Tween tween = _panel.transform.DOScale(Vector3.zero, _duration).SetUpdate(true);

        yield return tween.WaitForCompletion();

        _panel.transform.localScale = Vector3.one;

        OnCloseComplete(_panel);
    }

    private void OnOpenComplete()
    {
        isAnim = false;
    }

    private void OnCloseComplete(CanvasGroup _panel)
    {
        _panel.gameObject.SetActive(false);
        isAnim = false;
    }

    public bool HasPanel()
    {
        return panelStack.Count > 0;
    }

    public FadeUI Fade { get { return fade; } }
}
