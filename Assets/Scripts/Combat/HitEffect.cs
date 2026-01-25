using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
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

    Coroutine colorRoutine;

    private void Start()
    {
        knockBack = new KnockBack();

        effectPool = ObjectPoolManager.instance.EffectPool;
        cameraShake = CameraManager.instance.CameraShake;
    }

    public void ApplyHitEffect(HitEffectData _data, HitTransformContext _ctx)
    {
        if (_data.isParry) return;

        StartInvincible(invincibleTime);

        if(_data.hitStopTime > 0)
        StartCoroutine(StartHitStopDelay(_data.hitStopTime));

        if (_data.useKnockBack)
        {
            Rigidbody2D rigid = _ctx.target.GetComponent<Rigidbody2D>();

            if (rigid != null)
            {
                StartCoroutine(StartKnockBack(rigid, _ctx.attacker, _ctx.target, _data.knockBackPower));
            }
        }

        effectPool.FindEffect(_data.effect, _ctx.target.position, Quaternion.identity);

        AudioManager.instance.PlaySfx(_data.sfx, _ctx.target.position, _ctx.target);

        if(_data.useCameraShake)
        cameraShake.ShakeCamera();

        SpriteRenderer render = _ctx.target.GetComponent<SpriteRenderer>();
        if (render != null)
        {
            Material originMat = render.material;

            StartCoroutine(StartChangeColor(render, originMat));
        }
    }

    IEnumerator StartKnockBack(Rigidbody2D _rigid, Transform _attacker, Transform _target, float _knockBackPower)
    {
        IsKnockBack = true;

        knockBack.Apply(_rigid, _attacker, _target, _knockBackPower);

        yield return new WaitForSeconds(knockBackDelay);

        IsKnockBack = false;
    }

    IEnumerator StartHitStopDelay(float _hitStopSec)
    {
        yield return new WaitForSeconds(hitStopDelay);

        hitStop.ApplyHitStop(_hitStopSec);
    }

    IEnumerator StartChangeColor(SpriteRenderer _render, Material _originMat)
    {
        _render.material = whiteMat;

        yield return new WaitForSeconds(0.25f);

        _render.material = _originMat;
    }

    IEnumerator StartInvincible(float _time)
    {
        isInvincible = true;

        yield return new WaitForSeconds(_time);

        isInvincible = false;
    }
}
