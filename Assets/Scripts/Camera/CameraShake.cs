using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin perlin;

    [SerializeField] float shakeDuration = 0.3f;
    [SerializeField] float amp = 0.3f;
    [SerializeField] float freq = 0.11f;

    private void Awake()
    {
        perlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShakeCamera();
        }
    }

    private void ShakeCamera()
    {
        StartCoroutine(StartShakeCamera(freq));
    }

    IEnumerator StartShakeCamera(float _freq)
    {
        float t = 0f;

        perlin.m_AmplitudeGain = amp;
        perlin.m_FrequencyGain = freq;

        while (t < shakeDuration) 
        {
            t += Time.deltaTime;

            float norm = t / shakeDuration;

            perlin.m_AmplitudeGain = Mathf.Lerp(amp, 0, norm);

            yield return null;
        }

        perlin.m_AmplitudeGain = 0f;
    }
}
