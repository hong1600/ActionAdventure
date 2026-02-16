using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    TableSkill.Info skillData;

    public static  event Action<TableSkill.Info> OnItemAcquired;

    public void Interact(PlayerInteraction _player)
    {
        OnItemAcquired?.Invoke(skillData);

        Destroy(gameObject);
    }
}
