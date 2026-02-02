using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectTable : MonoBehaviour
{
    public HitEffectData parryHit;
    public HitEffectData lightHit;
    public HitEffectData heavyHit;
    public HitEffectData finishHit;

    public HitEffectData Get(EHitLevel _eHitLevel)
    {
        if (_eHitLevel == EHitLevel.PARRY) return parryHit;
        if(_eHitLevel == EHitLevel.LIGHT) return lightHit;
        if(_eHitLevel == EHitLevel.HEAVY) return heavyHit;
        if(_eHitLevel == EHitLevel.FINISH) return finishHit;
        return null;
    }
}

[System.Serializable]
public class HitEffectData
{
    public bool isParry;

    public bool useKnockBack;
    public float knockBackPower;

    public float hitStopTime;

    public bool useCameraShake;

    public EEffect effect;

    public ESfx sfx;
}

public class HitContext
{
    public bool isParry;
    public bool isCritical;
    public bool isFinish;
}

public class HitTransformContext
{
    public Transform attacker;
    public Transform target;
    public HitVisual hitVisual;
}


public class HitLevelResolver
{
    public EHitLevel Resolve(HitContext _ctx)
    {
        if(_ctx.isParry) return EHitLevel.PARRY;
        if (_ctx.isCritical) return EHitLevel.HEAVY;
        if (_ctx.isFinish) return EHitLevel.FINISH;
        return EHitLevel.LIGHT;
    }
}
