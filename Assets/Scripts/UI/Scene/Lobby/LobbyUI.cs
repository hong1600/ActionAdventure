using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    PanelUI panel;
    FadeUI fade;

    [SerializeField] CanvasGroup mainPanel;
    [SerializeField] CanvasGroup selectPanel;

    private void Start()
    {
        fade = UIManager.instance.Fade;

        fade.FadeImg.color = new Color(0, 0, 0, 1);

        StartCoroutine(fade.StartFadeOut(fade.FadeImg, 3f));

        panel = UIManager.instance.Panel;
        panel.Init();

        AudioManager.instance.PlayBgm(EBgm.LOBBY);
    }

    public void ClickStartBtn()
    {
        panel.OpenPanel(selectPanel);
    }

    public void ClickBackBtn()
    {
        panel.ClosePanel();
    }

    public void ClickOptionBtn()
    {
        OptionManager.instance.ClickOption();
    }
}
