using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField] float timeScale;

    bool isHitStop;

    public void ApplyHitStop(float _sec)
    {
        if (isHitStop) return;

        StartCoroutine(StartHitStop(_sec));
    }

    IEnumerator StartHitStop(float _sec)
    {
        isHitStop = true;
        Time.timeScale = timeScale;

        yield return new WaitForSecondsRealtime(_sec);

        Time.timeScale = 1f;
        isHitStop = false;
    }
}
