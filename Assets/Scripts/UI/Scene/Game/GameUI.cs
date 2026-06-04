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
    [SerializeField] ItemAcquireUI itemAcquireUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] PlayerStatUI playerStatUI;

    private void Start()
    {
        panel = UIManager.instance.Panel;
        panel.Init();

        fade = UIManager.instance.Panel.Fade;

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

    public InteractionUI InteractionUI { get { return interactionUI; } }
    public ItemAcquireUI ItemAcquireUI { get { return itemAcquireUI; } }
    public DialogueUI DialogueUI { get { return dialogueUI; } }
    public PlayerStatUI PlayerStatUI { get { return playerStatUI;} }
}
