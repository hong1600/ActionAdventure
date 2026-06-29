using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EQuestID
{
    HUNTBOSS,
    COLLECTHERB,
    DELIVERYBREAD
}

public enum EQuestState
{
    CANSTART,
    PROGRESS,
    COMPLETE
}

public enum EQuestType
{
    KILL,
    COLLECT,
    DELIVERY
}

[CreateAssetMenu(menuName = "Data/Quest")]
public class QuestData : ScriptableObject
{
    public EQuestID questID;
    public EQuestState questState;
    public string questName;
    public Sprite questImg;

    public EQuestType questType;
    public QuestConditon questCondition;

    public int rewardAmount;
    public Sprite rewardImg;

    [TextArea]
    public string questDesc;
}
