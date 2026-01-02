using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField] EffectPool effectPool;

    protected override void Awake()
    {
        base.Awake();

        effectPool.Init();
    }

    public EffectPool EffectPool { get { return effectPool; } }

}
