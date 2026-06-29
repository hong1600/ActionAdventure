using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] Image fadeImg;
    public Image FadeImg { get { return fadeImg; } }

    public Coroutine FadeIn(Graphic _ui, float _duration, float _alpha = 1)
    {
        return StartCoroutine(StartFadeIn(_ui, _duration, _alpha));
    }

    public Coroutine FadeOut(Graphic _ui, float _duration)
    {
        return StartCoroutine(StartFadeOut(_ui, _duration));
    }

    public IEnumerator StartFadeOut(Graphic _ui, float _duration)
    {
        Color _color = _ui.color;
        float startAlpha = _color.a;
        float time = 0f;

        while (time < _duration)
        {
            time += Time.deltaTime;
            _color.a = Mathf.Lerp(startAlpha, 0, time / _duration);
            _ui.color = _color;

            yield return null;
        }

        _color.a = 0;
        _ui.color = _color;
    }

    public IEnumerator StartFadeIn(Graphic _ui, float _duration, float _alpha = 1f)
    {
        Color _color = _ui.color;
        float startAlpha = _color.a;
        float time = 0f;

        while (time < _duration)
        {
            time += Time.deltaTime;
            _color.a = Mathf.Lerp(startAlpha, _alpha, time / _duration);
            _ui.color = _color;

            yield return null;
        }

        _color.a = _alpha;
        _ui.color = _color;
    }

    public void SetAlpha(Graphic _ui, float _alpha)
    {
        Color color = _ui.color;
        color.a = _alpha;
        _ui.color = color;
    }

}
