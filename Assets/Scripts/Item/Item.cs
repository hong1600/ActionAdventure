using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    TableSkill.Info skillData;

    public static  event Action<TableSkill.Info> OnItemAcquired;

    [SerializeField] int skillID;

    private void Start()
    {
        skillData = DataManager.instance.TableSkill.Get(skillID);
    }

    public void Interact()
    {
        OnItemAcquired?.Invoke(skillData);

        Destroy(gameObject);
    }
}
