using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    Stack<CanvasGroup> panelStack = new Stack<CanvasGroup>();

    bool isAnim = false;

    public void Init()
    {
        panelStack.Clear();
    }

    public void OpenPanel(CanvasGroup _panel)
    {
        if (isAnim) return;

        StartCoroutine(StartOpenPanel(_panel));
    }

    IEnumerator StartOpenPanel(CanvasGroup _panel)
    {
        if (panelStack.Count > 0)
        {
            CanvasGroup curPanel = panelStack.Peek();
            yield return StartCoroutine(StartHidePanel(curPanel, 0.5f));
        }

        panelStack.Push(_panel);
        yield return StartCoroutine(StartShowPanel(_panel, 0.5f));
    }

    public void ClosePanel()
    {
        if (isAnim) return;

        StartCoroutine(StartClosePanel());
    }

    IEnumerator StartClosePanel()
    {
        if (panelStack.Count == 0) yield break;

        CanvasGroup curPanel = panelStack.Pop();

        yield return StartCoroutine(StartHidePanel(curPanel, 0.5f));

        if (panelStack.Count > 0)
        {
            CanvasGroup prevPanel = panelStack.Peek();
            yield return StartCoroutine(StartShowPanel(prevPanel, 0.5f));
        }
    }

    public IEnumerator StartShowPanel(CanvasGroup _panel, float _duration)
    {
        _panel.alpha = 0f;
        _panel.gameObject.SetActive(true);

        Tween tween = _panel.DOFade(1f, _duration).SetUpdate(true).OnComplete(OnOpenComplete);

        yield return tween.WaitForCompletion();

        OnOpenComplete();
    }

    public IEnumerator StartHidePanel(CanvasGroup _panel, float _duration)
    {
        Tween tween = _panel.DOFade(0f, _duration).SetUpdate(true);

        yield return tween.WaitForCompletion();

        OnCloseComplete(_panel);

        _panel.alpha = 0f;
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
}
