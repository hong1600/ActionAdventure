using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : Singleton<OptionManager>
{
    OptionDataLoader loader;
    OptionData optionData;
    PanelUI panel;

    [SerializeField] CanvasGroup optionPanel;
    public CanvasGroup OptionPanel { get; private set; }
    [SerializeField] CanvasGroup audioPanel;
    [SerializeField] CanvasGroup videoPanel;

    [SerializeField] ResolutionSetting resSet;
    [SerializeField] ScreenModeSetting screenSet;
    [SerializeField] QualitySetting qualitySet;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        loader = DataManager.instance.OptionDataLoader;
        optionData = DataManager.instance.OptionDataLoader.OptionData;
        panel = UIManager.instance.Panel;

        resSet.Init(optionData.resWidth, optionData.resHeight);
        screenSet.Init(optionData.screenMode);
        qualitySet.Init(optionData.qualityLevel);

        AudioManager.instance.SetMasterVolume(optionData.masterVol);
        AudioManager.instance.SetSfxVolume(optionData.sfxVol);
        AudioManager.instance.SetBgmVolume(optionData.bgmVol);
    }

    public void ClickOption()
    {
        panel.OpenPanel(optionPanel);
    }

    public void ClickBack()
    {
        panel.ClosePanel();
    }

    public void ClickAudio()
    {
        panel.OpenPanel(audioPanel);
    }

    public void ClickVideo()
    {
        panel.OpenPanel(videoPanel);
    }

    public void ClickLanguage()
    {

    }

    public void ApplySave()
    {
        loader.Save();
    }
}
