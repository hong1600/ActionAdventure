using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    public event Action<ItemData> onSkillItemAcquired;

    GameState gameState;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AcquireSkill(ItemData _data)
    {
        onSkillItemAcquired?.Invoke(_data);
    }
}
