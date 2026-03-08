using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    PanelUI panel;

    [SerializeField] CanvasGroup settingPanel;
    public CanvasGroup SettingPanel { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        panel = UIManager.instance.Panel;
    }

    public void ClickOption()
    {
        panel.OpenPanel(settingPanel);
    }

    public void ClickBack()
    {
        panel.ClosePanel();
    }
}
