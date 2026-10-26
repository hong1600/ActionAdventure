using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Spike : MonoBehaviour
{
    [SerializeField] BossSpikeSpawner spawner;

    [Header("Spike")]
    [SerializeField] Transform visual;
    [SerializeField] GameObject attackBox;

    [SerializeField] float hiddenOffsetY = -2f;
    [SerializeField] float riseStep1OffsetY = -1f;

    [SerializeField] float riseStepTime = 0.03f;
    [SerializeField] float activeTime = 0.5f;
    [SerializeField] float hideStepTime = 0.04f;

    [Header("Light")]
    [SerializeField] Light2D warningLight;

    [SerializeField] float startRadius = 1f;
    [SerializeField] float endRadius = 2f;

    [SerializeField] float expandTime = 0.4f;

    float timer;


    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(StartRise());

        timer = 0f;

        if (warningLight == null) return;

        warningLight.pointLightOuterRadius = startRadius;
    }

    private void Update()
    {
        if (warningLight == null) return;
        if (timer >= expandTime) return;

        timer += Time.deltaTime;

        float ratio = Mathf.Clamp01(timer / expandTime);

        warningLight.pointLightOuterRadius = Mathf.Lerp(startRadius, endRadius, ratio);
    }

    public void SetSpawner(BossSpikeSpawner _spanwer)
    {
        spawner = _spanwer;
    }

    IEnumerator StartRise()
    {
        attackBox.SetActive(false);

        visual.localPosition = new Vector3(0f, hiddenOffsetY, 0f);

        yield return new WaitForSeconds(riseStepTime);

        visual.localPosition = new Vector3(0f, riseStep1OffsetY, 0f);

        yield return new WaitForSeconds(riseStepTime);

        visual.localPosition = new Vector3(0f, 0f, 0f);
        attackBox.SetActive(true);

        yield return new WaitForSeconds(activeTime);

        attackBox.SetActive(false);

        visual.localPosition = new Vector3(0f, riseStep1OffsetY, 0f);

        yield return new WaitForSeconds(hideStepTime);

        visual.localPosition =  new Vector3(0f, hiddenOffsetY, 0f);

        spawner.OnSpikeFinished();

        Destroy(this.gameObject);
    }
}
