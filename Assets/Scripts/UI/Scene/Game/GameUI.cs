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
    [SerializeField] SkillAcquireUI skillAcquireUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] PlayerStatUI playerStatUI;
    [SerializeField] BoardUI boardUI;
    [SerializeField] QuestUI questUI;
    [SerializeField] ToastUI toastUI;

    private void Start()
    {
        panel = UIManager.instance.SystemPanel;
        panel.Init();

        fade = UIManager.instance.SystemPanel.Fade;

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
    public SkillAcquireUI SkillAcquireUI { get { return skillAcquireUI; } }
    public DialogueUI DialogueUI { get { return dialogueUI; } }
    public PlayerStatUI PlayerStatUI { get { return playerStatUI;} }
    public BoardUI BoardUI { get { return boardUI;} }
    public QuestUI QuestUI { get { return questUI; } }
    public ToastUI ToastUI { get { return toastUI;} }
}
