using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayBgm(EBgm.LOBBY);
    }

    public void ClickStartBtn()
    {
        StartCoroutine(StartClickStartBtn());
    }

    IEnumerator StartClickStartBtn()
    {
        yield return StartCoroutine(UIManager.instance.StartFadeIn(UIManager.instance.FadeImg, 3));

        MSceneManager.Instance.ChangeScene(EScene.INTRO, true);
    }

    public void ClickOptionBtn()
    {

    }

    public void ClickExitBtn()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
