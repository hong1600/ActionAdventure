using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    PanelUI panel;

    [SerializeField] CanvasGroup mainPanel;
    [SerializeField] CanvasGroup selectPanel;

    private void Start()
    {
        panel = UIManager.instance.Panel;
        panel.Init(mainPanel);
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
        SettingManager.instance.ClickOption();
    }
}
