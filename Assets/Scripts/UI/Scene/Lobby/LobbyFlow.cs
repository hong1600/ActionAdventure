using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LobbyFlow : MonoBehaviour
{
    UserDataLoader userDataLoader;

    private void Start()
    {
        userDataLoader = DataManager.instance.UserData;

        AudioManager.instance.PlayBgm(EBgm.LOBBY);
    }

    public void ClickProfile(int _num)
    {
        UserData userData = userDataLoader.LoadOrCreateUserData(_num);

        if(userData == null) 
        {
            StartCoroutine(StartClickStartBtn(EScene.INTRO));
        }
        else
        {
            StartCoroutine(StartClickStartBtn(EScene.GAME));
        }
    }

    IEnumerator StartClickStartBtn(EScene _eScene)
    {
        yield return StartCoroutine(UIManager.instance.StartFadeIn(UIManager.instance.FadeImg, 3));

        MSceneManager.Instance.ChangeScene(_eScene, true);
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
