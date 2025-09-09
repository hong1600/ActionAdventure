using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] Image fadeImg;

    private void Start()
    {
        //StartCoroutine(UIManager.instance.StartFadeOut(fadeImg, 4f));
    }

    public void DieFade()
    {
        StartCoroutine(StartDieFade());
    }

    IEnumerator StartDieFade()
    {
        yield return StartCoroutine(UIManager.instance.StartFadeIn(fadeImg, 2f));

        yield return StartCoroutine(UIManager.instance.StartFadeOut(fadeImg, 2f));
    }
}
