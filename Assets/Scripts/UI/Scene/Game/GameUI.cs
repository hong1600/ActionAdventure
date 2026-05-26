using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    FadeUI fade;
    PanelUI panel;

    [SerializeField] InteractionUI interactionUI;
    public InteractionUI InteractionUI { get { return interactionUI; } }
    [SerializeField] ItemAcquireUI itemAcquireUI;
    public ItemAcquireUI ItemAcquireUI { get { return itemAcquireUI; } }

    private void Start()
    {
        panel = UIManager.instance.Panel;
        panel.Init();

        fade = UIManager.instance.Fade;

        StartCoroutine(fade.StartFadeOut(fade.FadeImg, 4f));
    }

    public void DieFade()
    {
        StartCoroutine(StartDieFade());
    }

    IEnumerator StartDieFade()
    {
        yield return StartCoroutine(fade.StartFadeIn(fade.FadeImg, 2f));

        yield return StartCoroutine(fade.StartFadeOut(fade.FadeImg, 2f));
    }

}
