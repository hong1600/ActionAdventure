using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionUI : MonoBehaviour
{
    [SerializeField] CanvasGroup gameOptionPanel;
    [SerializeField] CanvasGroup ExitPanel;

    PanelUI panel;

    private void Start()
    {
        panel = UIManager.instance.SystemPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.HasPanel())
            {
                panel.ClosePanel(EPanelAnimType.FADE);
            }
            else
            {
                panel.OpenPanel(gameOptionPanel, EPanelAnimType.FADE);
            }
        }
    }

    public void ClickContinue()
    {
        panel.ClosePanel(EPanelAnimType.FADE);
    }

    public void ClickOption() 
    { 
        OptionManager.instance.ClickOption();
    }

    public void ClickExit()
    {
        panel.OpenPanel(ExitPanel, EPanelAnimType.FADE);
    }

    public void ClickAgree()
    {
        DataManager.instance.UserData.SaveUserData();

        MSceneManager.Instance.ChangeScene(EScene.LOBBY);
    }

    public void ClickCancle()
    {
        panel.ClosePanel(EPanelAnimType.FADE);
    }
}
