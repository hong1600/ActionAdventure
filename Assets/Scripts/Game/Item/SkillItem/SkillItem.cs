using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : Item
{
    public static event Action<ItemData> OnSkillItemAcquired;

    [SerializeField] ItemData itemData;

    //protected TableSkill.Info skillData;
    //[SerializeField] int skillID;


    private void Start()
    {
        //skillData = DataManager.instance.TableSkill.Get(skillID);
    }

    protected override void InteractItem()
    {
        OnSkillItemAcquired?.Invoke(itemData);

        base.InteractItem();
    }
}
