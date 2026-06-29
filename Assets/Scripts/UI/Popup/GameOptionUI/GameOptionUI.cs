using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameOptionUI : MonoBehaviour
{
    [SerializeField] CanvasGroup gameOptionPanel;
    [SerializeField] CanvasGroup ExitPanel;

    PanelUI systemPanel;
    PanelUI gamePanel;

    private void Start()
    {
        systemPanel = UIManager.instance.SystemPanel;
        gamePanel = UIManager.instance.GamePanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (systemPanel.HasPanel())
            {
                systemPanel.ClosePanel(EPanelAnimType.FADE);
                return;
            }

            if(gamePanel.HasPanel()) 
            {
                gamePanel.ClosePanel(EPanelAnimType.FADE, ReturnPlayState);
                return;
            }

            systemPanel.OpenPanel(gameOptionPanel, EPanelAnimType.FADE);
        }
    }

    private void ReturnPlayState()
    {
        GameManager.instance.GameState.SetState(EGameState.PLAY);
    }

    public void ClickContinue()
    {
        systemPanel.ClosePanel(EPanelAnimType.FADE);
    }

    public void ClickOption() 
    { 
        OptionManager.instance.ClickOption();
    }

    public void ClickExit()
    {
        systemPanel.OpenPanel(ExitPanel, EPanelAnimType.FADE);
    }

    public void ClickAgree()
    {
        DataManager.instance.UserData.SaveUserData();

        MSceneManager.Instance.ChangeScene(EScene.LOBBY);
    }

    public void ClickCancle()
    {
        systemPanel.ClosePanel(EPanelAnimType.FADE);
    }
}
