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
        panel = UIManager.instance.Panel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.HasPanel())
            {
                panel.ClosePanel();
            }
            else
            {
                panel.OpenPanel(gameOptionPanel);
            }
        }
    }

    public void ClickContinue()
    {
        panel.ClosePanel();
    }

    public void ClickOption() 
    { 
        OptionManager.instance.ClickOption();
    }

    public void ClickExit()
    {
        panel.OpenPanel(ExitPanel);
    }

    public void ClickAgree()
    {
        DataManager.instance.UserData.SaveUserData();

        MSceneManager.Instance.ChangeScene(EScene.LOBBY);
    }

    public void ClickCancle()
    {
        panel.ClosePanel();
    }
}
