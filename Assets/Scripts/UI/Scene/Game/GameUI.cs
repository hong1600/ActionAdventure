using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] InteractionUI interactionUI;
    public InteractionUI InteractionUI { get { return interactionUI; } }
    [SerializeField] ItemAcquireUI itemAcquireUI;
    public ItemAcquireUI ItemAcquireUI { get { return itemAcquireUI; } }

    private void Start()
    {
        StartCoroutine(UIManager.instance.StartFadeOut(UIManager.instance.FadeImg, 4f));
    }

    public void DieFade()
    {
        StartCoroutine(StartDieFade());
    }

    IEnumerator StartDieFade()
    {
        yield return StartCoroutine(UIManager.instance.StartFadeIn(UIManager.instance.FadeImg, 2));

        yield return StartCoroutine(UIManager.instance.StartFadeOut(UIManager.instance.FadeImg, 2));
    }

}
