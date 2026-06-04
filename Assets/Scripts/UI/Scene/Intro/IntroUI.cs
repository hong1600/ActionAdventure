using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    FadeUI fade;

    [SerializeField] TextMeshProUGUI[] text;

    private void Start()
    {
        fade = UIManager.instance.Panel.Fade;

        StartCoroutine(StartTextFadeInOut());
    }

    IEnumerator StartTextFadeInOut()
    {
        yield return StartCoroutine(fade.StartFadeOut(fade.FadeImg, 1));

        for (int i = 0; i < text.Length; i++) 
        {
            yield return StartCoroutine(fade.StartFadeIn(text[i], 3.5f));

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(fade.StartFadeIn(fade.FadeImg, 3.5f));

        MSceneManager.Instance.ChangeScene(EScene.GAME, true);
    }
}
