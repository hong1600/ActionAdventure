using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LobbyFlow : MonoBehaviour
{
    FadeUI fade;

    UserDataLoader userDataLoader;

    private void Start()
    {
        fade = UIManager.instance.SystemPanel.Fade;

        userDataLoader = DataManager.instance.UserData;

        AudioManager.instance.PlayBgm(EBgm.LOBBY);
    }

    public void ClickProfile(int _num)
    {
        UserData userData = userDataLoader.LoadUserData(_num);

        if(userData == null) 
        {
            userDataLoader.CreateUserData();
            StartCoroutine(StartClickStartBtn(EScene.INTRO));
        }
        else
        {
            StartCoroutine(StartClickStartBtn(EScene.GAME));
        }
    }

    IEnumerator StartClickStartBtn(EScene _eScene)
    {
        yield return StartCoroutine(fade.StartFadeIn(fade.FadeImg, 3));

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
