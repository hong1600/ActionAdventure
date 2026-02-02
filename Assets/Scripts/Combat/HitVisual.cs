using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVisual : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] renders;
    Material[] mats;

    private void Awake()
    {
        mats = new Material[renders.Length];

        for (int i = 0; i < renders.Length; i++)
        {
            mats[i] = renders[i].material;
        }
    }

    public void ChangeColor(Material _whiteMat)
    {
        StartCoroutine(StartChangeColor(renders, mats, _whiteMat));
    }

    IEnumerator StartChangeColor(SpriteRenderer[] _renders, Material[] _originMats, Material _whiteMat)
    {
        for (int i = 0; i < _renders.Length; i++)
        {
            _renders[i].material = _whiteMat;
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < _renders.Length; i++)
        {
            _renders[i].material = _originMats[i];
        }
    }

}
