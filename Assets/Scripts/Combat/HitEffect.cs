using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] float invincibleTime = 0.3f;

    public bool IsKnockBack { get; private set; }
    public bool isInvincible { get; private set; }

    [SerializeField] Material whiteMat;
    Material originMat;

    Coroutine colorRoutine;

    public void Init(Rigidbody2D _rigid, SpriteRenderer _render)
    {
        rigid = _rigid;

        knockBack = new KnockBack();

        knockBack.Init(rigid);

        effectPool = ObjectPoolManager.instance.EffectPool;
        cameraShake = CameraManager.instance.CameraShake;

        render = _render;
        originMat = render.sharedMaterial;
    }

    public void ApplyHitEffect(HitEffectData _data, HitTransformContext _ctx)
    {
        if (_data.isParry) return;

        StartInvincible(invincibleTime);

        if(_data.hitStopTime > 0)
        StartCoroutine(StartHitStopDelay(_data.hitStopTime));

        if(_data.useKnockBack)
        StartCoroutine(StartKnockBack(_ctx.attacker, _ctx.target, _data.knockBackPower));

        effectPool.FindEffect(_data.effect, _ctx.target.position, Quaternion.identity);

        AudioManager.instance.PlaySfx(_data.sfx, _ctx.target.position, _ctx.target);

        if(_data.useCameraShake)
        cameraShake.ShakeCamera();

        ChangeHitColor();
    }

    IEnumerator StartKnockBack(Transform _attacker, Transform _player, float _knockBackPower)
    {
        IsKnockBack = true;

        knockBack.Apply(_player, _attacker, _knockBackPower);

        yield return new WaitForSeconds(knockBackDelay);

        IsKnockBack = false;
    }

    IEnumerator StartHitStopDelay(float _hitStopSec)
    {
        yield return new WaitForSeconds(hitStopDelay);

        hitStop.ApplyHitStop(_hitStopSec);
    }

    private void ChangeHitColor()
    {
        if (colorRoutine != null) StopCoroutine(colorRoutine);

        colorRoutine = StartCoroutine(StartChangeColor());
    }

    IEnumerator StartChangeColor()
    {
        render.material = whiteMat;

        yield return new WaitForSeconds(0.25f);

        render.material = originMat;
    }

    IEnumerator StartInvincible(float _time)
    {
        isInvincible = true;

        yield return new WaitForSeconds(_time);

        isInvincible = false;
    }
}
