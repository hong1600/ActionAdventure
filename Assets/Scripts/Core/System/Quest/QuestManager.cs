using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public event Action<QuestData> OnQuestAccepted;
    public event Action<QuestData> OnQuestCompleted;

    Dictionary<EQuestID, QuestData> questDataDic = new Dictionary<EQuestID, QuestData>();

    Dictionary<EQuestID, QuestData> canStartDataDic = new Dictionary<EQuestID, QuestData>();
    Dictionary<EQuestID, QuestData> progressDataDic = new Dictionary<EQuestID, QuestData>();
    Dictionary<EQuestID, QuestData> completeDataDic = new Dictionary<EQuestID, QuestData>();

    [SerializeField] List<QuestData> questDataList = new List<QuestData>();

    BoardUI boardUI;
    ToastUI toastUI;

    protected override void Awake()
    {
        base.Awake();

        InitQuestData();
    }

    private void Start()
    {
        boardUI = GameUI.instance.BoardUI;
        toastUI = GameUI.instance.ToastUI;
    }

    private void InitQuestData()
    {
        for (int i = 0; i < questDataList.Count; i++)
        {
            QuestData data = questDataList[i];

            if (questDataDic.ContainsKey(data.questID)) continue;

            questDataDic.Add(data.questID, data);
        }

        ClassifyQuest();
    }

    private void ClassifyQuest()
    {
        canStartDataDic.Clear();
        progressDataDic.Clear();
        completeDataDic.Clear();

        for (int i = 0; i < questDataList.Count; i++)
        {
            QuestData data = questDataList[i];

            switch (data.questState)
            {
                case EQuestState.CANSTART:
                    canStartDataDic.Add(data.questID, data);
                    break;
                case EQuestState.PROGRESS:
                    progressDataDic.Add(data.questID, data);
                    break;
                case EQuestState.COMPLETE:
                    completeDataDic.Add(data.questID, data);
                    break;
            }
        }
    }

    public List<QuestData> GetCanStartQuestList()
    {
        List<QuestData> list = new List<QuestData>();
        List<EQuestID> keyList = new List<EQuestID>(canStartDataDic.Keys);

        for(int i = 0;i < keyList.Count;i++) 
        {
            EQuestID id = keyList[i];
            list.Add(canStartDataDic[id]);
        }

        if (list.Count <= 0) return null;

        return list;
    }

    public bool AcceptQuest(QuestData _data)
    {
        if (_data == null) return false;

        EQuestID id = _data.questID;

        if (!canStartDataDic.ContainsKey(id)) return false;

        canStartDataDic.Remove(id);

        _data.questState = EQuestState.PROGRESS;

        if (!progressDataDic.ContainsKey(id))
        {
            progressDataDic.Add(id, _data);
        }

        OnQuestAccepted?.Invoke(_data);

        return true;
    }

    public void CheckQuestItem(ItemData _item)
    {
        List<EQuestID> list = new List<EQuestID>(progressDataDic.Keys);

        for(int i = 0; i < list.Count; i++) 
        {
            QuestData questData = progressDataDic[list[i]];

            if (questData.questCondition.itemData.ItemID != _item.ItemID) continue;

            questData.questCondition.curCount++;

            toastUI.ShowUnderToastUI(_item);
            toastUI.ShowTopToastUI(questData);

            if (questData.questCondition.curCount >= questData.questCondition.needCount)
            {
                questData.questState = EQuestState.COMPLETE;

                progressDataDic.Remove(questData.questID);
                completeDataDic.Add(questData.questID, questData);
            }

            return;
        }
    }

    private void CompleteQuest(QuestData _data)
    {
        OnQuestCompleted?.Invoke(_data);
    }
}
