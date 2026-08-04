using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCam;
    CinemachineBasicMultiChannelPerlin perlin;

    [SerializeField] float amp = 0.3f;
    [SerializeField] float freq = 0.11f;

    private void Awake()
    {
        perlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float _duration)
    {
        StartCoroutine(StartShakeCamera(freq, _duration));
    }

    IEnumerator StartShakeCamera(float _freq, float _duration)
    {
        float t = 0f;

        perlin.m_AmplitudeGain = amp;
        perlin.m_FrequencyGain = freq;

        while (t < _duration) 
        {
            t += Time.deltaTime;

            float norm = t / _duration;

            perlin.m_AmplitudeGain = Mathf.Lerp(amp, 0, norm);

            yield return null;
        }

        perlin.m_AmplitudeGain = 0f;
    }
}
