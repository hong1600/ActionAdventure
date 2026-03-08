using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{
    Stack<CanvasGroup> panelStack = new Stack<CanvasGroup>();

    public void Init(CanvasGroup _startPanel)
    {
        panelStack.Clear();
        OpenPanel(_startPanel);
    }

    public void OpenPanel(CanvasGroup _panel)
    {
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
        StartCoroutine(StartClosePanel());
    }

    IEnumerator StartClosePanel()
    {
        if (panelStack.Count <= 1) yield break;

        CanvasGroup curPanel = panelStack.Pop();
        CanvasGroup prevPanel = panelStack.Peek();

        yield return StartCoroutine(StartHidePanel(curPanel, 0.5f));
        yield return StartCoroutine(StartShowPanel(prevPanel, 0.5f));
    }

    IEnumerator StartShowPanel(CanvasGroup _panel, float _duration)
    {
        _panel.alpha = 0f;
        _panel.gameObject.SetActive(true);

        _panel.DOFade(1f, _duration);

        yield return new WaitForSeconds(_duration);
    }

    IEnumerator StartHidePanel(CanvasGroup _panel, float _duration)
    {
        _panel.DOFade(0f, _duration);

        yield return new WaitForSeconds(_duration);

        _panel.gameObject.SetActive(false);
        _panel.alpha = 0f;
    }
}
