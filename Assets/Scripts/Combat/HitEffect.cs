using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] HitStop hitStop;
    [SerializeField] float hitStopDelay = 0.1f;

    EffectPool effectPool;
    CameraShake cameraShake;

    private void Start()
    {
        effectPool = ObjectPoolManager.instance.EffectPool;
        cameraShake = CameraManager.instance.CameraShake;
    }

    public void ApplyHitEffect(HitEffectData _data, HitTransformContext _ctx)
    {
        if(_data == null || _ctx == null) return;
        if(_ctx.target == null) return;
        if (_data.isParry) return;

        if (_data.hitStopTime > 0)
        {
            StartCoroutine(StartHitStopDelay(_data.hitStopTime));
        }

        effectPool.FindEffect(_data.effect, _ctx.target.position, Quaternion.identity);

        AudioManager.instance.PlaySfx(_data.sfx, _ctx.target.position, _ctx.target);

        if (_data.useCameraShake)
        {
            cameraShake.ShakeCamera(0.3f);
        }

        if (_ctx.hitFlash != null)
        {
            _ctx.hitFlash.Flash(0.15f);
        }
    }

    IEnumerator StartHitStopDelay(float _hitStopSec)
    {
        yield return new WaitForSeconds(hitStopDelay);

        hitStop.ApplyHitStop(_hitStopSec);
    }
}
