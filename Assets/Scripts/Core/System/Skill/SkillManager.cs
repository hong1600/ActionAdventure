using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum ESkillID
{
    NONE,
    DOUBLEJUMP,
    CLIMB,
    DASH
}

public class SkillManager : Singleton<SkillManager>
{
    public event Action<ItemData> onSkillItemAcquired;

    Dictionary<ESkillID, bool> skillStateDic = new Dictionary<ESkillID, bool>();

    GameState gameState;

    protected override void Awake()
    {
        base.Awake();
    }

    public void AcquireSkill(ItemData _data)
    {
        if (_data.skillID == ESkillID.NONE) return;

        skillStateDic[_data.skillID] = true;

        onSkillItemAcquired?.Invoke(_data);
    }

    public bool IsUnlocked(ESkillID _id)
    {
        bool isUnlocked;

        if(skillStateDic.TryGetValue(_id, out isUnlocked)) return isUnlocked;

        return false;
    }
}
