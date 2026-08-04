using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpikeWaitEffect : MonoBehaviour
{
    [SerializeField] float destroyTime = 2f;

    private void OnEnable()
    {
        Destroy(gameObject, destroyTime);
    }

    //[SerializeField] Light2D warningLight;

    //[SerializeField] float startRadius = 1f;
    //[SerializeField] float endRadius = 2f;

    //[SerializeField] float expandTime = 0.4f;

    //float timer;

    //private void OnEnable()
    //{
    //    timer = 0f;

    //    if (warningLight == null) return;

    //    warningLight.pointLightOuterRadius = startRadius;
    //}

    //private void Update()
    //{
    //    if(warningLight == null) return;
    //    if (timer >= expandTime) return;

    //    timer += Time.deltaTime;

    //    float ratio = Mathf.Clamp01(timer / expandTime);

    //    warningLight.pointLightOuterRadius = Mathf.Lerp(startRadius, endRadius, ratio);
    //}
}
