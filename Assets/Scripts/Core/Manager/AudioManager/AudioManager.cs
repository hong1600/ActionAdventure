using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SfxData
{
    public ESfx type;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
}

public class AudioManager : Singleton<AudioManager>
{
    AudioSource[] sfxSources;
    AudioSource bgmSource;
    AudioSource sfx2DSource;

    [Header("Clip")]
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] SfxData[] sfxDatas;

    [Header("Volume")]
    [Range(0f, 1f)] public float masterVolume = 1.0f;
    [Range(0f, 1f)] public float bgmVolume = 1.0f;
    [Range(0f, 1f)] public float sfxVolume = 1.0f;

    int sfxIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        bgmSource = gameObject.AddComponent<AudioSource>();
        sfx2DSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;

        sfx2DSource.spatialBlend = 0f;

        bgmSource.volume = bgmVolume * masterVolume;

        sfxSources = new AudioSource[70];

        for (int i = 0; i < sfxSources.Length; i++)
        {
            GameObject obj = new GameObject("SFX" + i);
            obj.transform.parent = this.transform;
            sfxSources[i] = obj.AddComponent<AudioSource>();
            sfxSources[i].spatialBlend = 1f;
            sfxSources[i].rolloffMode = AudioRolloffMode.Logarithmic;
            sfxSources[i].minDistance = 1f;
            sfxSources[i].maxDistance = 60f;
        }
    }

    public void PlayBgm(EBgm _eBgm)
    {
        int bgmIndex = (int)_eBgm;

        if (bgmIndex >= 0 && bgmIndex < bgmClips.Length)
        {
            bgmSource.clip = bgmClips[bgmIndex];
            bgmSource.Play();
        }
        else return;
    }

    public void StopBgm() 
    {
        bgmSource.Stop();
    }


    public void PlaySfx(ESfx _eSfx, Vector3 _pos, Transform _parent, float _minDist = 1f, float _maxDist = 60f)
    {
        AudioSource source = null;

        for(int i = 0; i < sfxSources.Length; i++) 
        {
            int index = (sfxIndex + i) % sfxSources.Length;

            if (!sfxSources[index].isPlaying)
            {
                source = sfxSources[index];
                sfxIndex = (index + 1) % sfxSources.Length;
                break;
            }
        }

        if (source == null)
        {
            source = sfxSources[sfxIndex];
            source.Stop();
            sfxIndex = (sfxIndex + 1) % sfxSources.Length;
        }

        int indexSfx = (int)_eSfx;

        if (indexSfx < 0 || indexSfx >= sfxDatas.Length) return;

        SfxData data = sfxDatas[indexSfx];

        source.transform.parent = _parent;
        source.transform.position = _pos;
        source.minDistance = _minDist;
        source.maxDistance = _maxDist;

        source.PlayOneShot(data.clip,data.volume * sfxVolume * masterVolume);
    }

    public void PlaySfxUI(ESfx _eSfx)
    {
        int sfxIndex = (int)_eSfx;

        if (sfxIndex < 0 || sfxIndex >= sfxDatas.Length) return;

        SfxData data = sfxDatas[sfxIndex];

        sfx2DSource.PlayOneShot(data.clip, data.volume * sfxVolume * masterVolume);
    }

    public void SetMasterVolume(float _volume)
    {
        masterVolume = Mathf.Clamp(_volume, 0f, 1f);

        bgmSource.volume = bgmVolume * masterVolume;
    }

    public void SetBgmVolume(float _volume)
    {
        bgmVolume = Mathf.Clamp(_volume, 0f, 1f);

        bgmSource.volume = bgmVolume * masterVolume;
    }

    public void SetSfxVolume(float _volume)
    {
        sfxVolume = Mathf.Clamp(_volume, 0f, 1f);
    }
}
