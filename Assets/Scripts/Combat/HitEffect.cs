using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;

    [SerializeField] HitStop hitStop;
    KnockBack knockBack;
    EffectPool effectPool;
    CameraShake cameraShake;

    [SerializeField] float hitStopDelay = 0.1f;
    [SerializeField] float knockBackDelay = 0.15f;

    public bool IsKnockBack { get; private set; }
    float KnockBackPower;

    [SerializeField] Material whiteMat;
    Material originMat;

    Coroutine colorRoutine;

    public void Init(Rigidbody2D _rigid, SpriteRenderer _render, float _knockBackPower)
    {
        rigid = _rigid;

        knockBack = new KnockBack();

        knockBack.Init(rigid);

        effectPool = ObjectPoolManager.instance.EffectPool;
        cameraShake = CameraManager.instance.CameraShake;

        render = _render;
        originMat = render.sharedMaterial;

        KnockBackPower = _knockBackPower;
    }

    public void ApplyHitEffect(Transform _attacker, Transform _player, float _hitStopSec, EEffect _eEffect, ESfx _eSfx)
    {
        StartCoroutine(StartKnockBack(_attacker, _player));
        effectPool.FindEffect(_eEffect, _player.position, Quaternion.identity);
        AudioManager.instance.PlaySfx(_eSfx, _player.position, _player);
        StartCoroutine(StartHitStopDelay(_hitStopSec));
    }

    IEnumerator StartKnockBack(Transform _attacker, Transform _player)
    {
        IsKnockBack = true;

        knockBack.Apply(_player, _attacker, KnockBackPower);

        yield return new WaitForSeconds(knockBackDelay);

        IsKnockBack = false;
    }

    IEnumerator StartHitStopDelay(float _hitStopSec)
    {
        yield return new WaitForSeconds(hitStopDelay);

        hitStop.ApplyHitStop(_hitStopSec);

        cameraShake.ShakeCamera();
    }

    private void ChangeHitColor()
    {
        if (colorRoutine != null) StopCoroutine(colorRoutine);

        colorRoutine = StartCoroutine(StartChangeColor());
    }

    IEnumerator StartChangeColor()
    {
        render.sharedMaterial = whiteMat;

        yield return new WaitForSeconds(0.1f);

        render.sharedMaterial = originMat;
    }
}
