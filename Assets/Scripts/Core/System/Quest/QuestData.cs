using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EQuestState
{
    NONE,
    CANSTART,
    PROGRESS,
    CANCOMPLETE,
    COMPLETE
}


[CreateAssetMenu(menuName = "Data/Quest")]
public class QuestData : ScriptableObject
{
    public int questID;
    public string questName;
    public Sprite questImg;

    [TextArea]
    public string questDesc;

    public int rewardAmount;
    public Sprite rewardImg;

    public EQuestState questState;
}
