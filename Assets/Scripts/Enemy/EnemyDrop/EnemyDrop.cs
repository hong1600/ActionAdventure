using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] QuestData questData;
    [SerializeField] ItemData itemData;
    [SerializeField] DropItem dropPrefab;

    [SerializeField] int amount;

    public void Drop(Vector2 _pos)
    {
        if (itemData == null) return;

        if (questData != null)
        {
            if (questData.questState != EQuestState.PROGRESS) return;

            if (questData.questCondition.curCount >= questData.questCondition.needCount) return;
        }

        for (int i = 0; amount > i; i++)
        {
            DropItem dropItem = Instantiate(dropPrefab, _pos, Quaternion.identity);
            dropItem.SetItem(itemData);
        }
    }
}
