using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] text;

    [SerializeField] Image fadeImg;

    private void Start()
    {
        StartCoroutine(StartTextFadeInOut());
    }

    IEnumerator StartTextFadeInOut()
    {
        for(int i = 0; i < text.Length; i++) 
        {
            yield return StartCoroutine(UIManager.instance.StartFadeIn(text[i], 3f));

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(UIManager.instance.StartFadeIn(fadeImg, 3));

        MSceneManager.Instance.ChangeScene(EScene.GAME, true);
    }
}
