using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : Item
{
    public static event Action<ItemData> OnQuestItemAcquired;

    [SerializeField] ItemData itemData;

    protected override void InteractItem()
    {
        OnQuestItemAcquired?.Invoke(itemData);

        base.InteractItem();
    }
}
