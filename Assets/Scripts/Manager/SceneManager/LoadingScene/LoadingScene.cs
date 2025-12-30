using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    static EScene nextScene;
    [SerializeField] Image sliderValue;

    float sceneProgress = 0f;

    private void Start()
    {
        StartCoroutine(StartLoadScene());
    }

    public static void SetNextScene(EScene _eScene)
    {
        nextScene = _eScene;
    }

    public static void LoadScene(EScene _eScene)
    {
        nextScene = _eScene;

        SceneManager.LoadScene("Loading");
    }

    IEnumerator StartLoadScene()
    {
        yield return null;

        AsyncOperation sceneOp = SceneManager.LoadSceneAsync((int)nextScene);
        sceneOp.allowSceneActivation = false;

        float timer = 0f;

        while (sceneOp.progress < 0.9f)
        {
            sceneProgress = Mathf.Clamp01(sceneOp.progress / 0.9f);
            //float totalProgress = (sceneProgress) / 2f;
            //sliderValue.fillAmount = Mathf.MoveTowards(sliderValue.fillAmount, totalProgress, Time.deltaTime * 2f);
            yield return null;
        }

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //while (sliderValue.fillAmount <= 0.999f)
        //{
        //    sliderValue.fillAmount = Mathf.MoveTowards(sliderValue.fillAmount, 1f, Time.deltaTime * 2f);
        //    yield return null;
        //}

        sceneOp.allowSceneActivation = true;
    }
}
