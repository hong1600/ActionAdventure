using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] text;

    private void Start()
    {
        StartCoroutine(StartTextFadeInOut());
    }

    IEnumerator StartTextFadeInOut()
    {
        yield return StartCoroutine(UIManager.instance.StartFadeOut(UIManager.instance.FadeImg, 1));

        for (int i = 0; i < text.Length; i++) 
        {
            yield return StartCoroutine(UIManager.instance.StartFadeIn(text[i], 3.5f));

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(UIManager.instance.StartFadeIn(UIManager.instance.FadeImg, 3.5f));

        MSceneManager.Instance.ChangeScene(EScene.GAME, true);
    }
}
