using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] CanvasGroup selectCanvasGroup;

    private void Start()
    {
        AudioManager.instance.PlayBgm(EBgm.LOBBY);
    }

    public void ClickStartBtn()
    {
        StartCoroutine(StartClickStartBtn());
    }

    public void ClickBackBtn()
    {
        StartCoroutine(StartShowMain());
    }

    IEnumerator StartShowSelect()
    {
        mainCanvasGroup.alpha = 1f;
        selectCanvasGroup.alpha = 0f;

        mainCanvasGroup.DOFade(0f, 2f);

        yield return new WaitForSeconds(2f);

        mainCanvasGroup.gameObject.SetActive(false);

        selectCanvasGroup.DOFade(1f, 2f);
    }

    IEnumerator StartShowMain()
    {
        mainCanvasGroup.alpha = 0f;
        selectCanvasGroup.alpha = 1f;

        selectCanvasGroup.DOFade(0f, 2f);

        yield return new WaitForSeconds(2f);

        mainCanvasGroup.DOFade(1f, 2f);
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
