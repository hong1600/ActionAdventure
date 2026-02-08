using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] renders;

    MaterialPropertyBlock mpb;
    Coroutine routine;

    [SerializeField] float flashTime = 0.15f;

    private void Awake()
    {
        mpb = new MaterialPropertyBlock();
    }

    public void Flash()
    {
        if(routine != null)
        {
            StopCoroutine(routine);
        }

        routine = StartCoroutine(StartFlash());
    }

    IEnumerator StartFlash()
    {
        SetFlash(1f);

        yield return new WaitForSeconds(flashTime);

        SetFlash(0f);

        routine = null;
    }

    private void SetFlash(float _value)
    {
        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].GetPropertyBlock(mpb);

            mpb.SetFloat("_Flash", _value);

            renders[i].SetPropertyBlock(mpb);
        }
    }

}
