using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    NONE,
    QUEST,
    SKILL,
    HP,
}

public enum EItemID
{
    NONE,

    //Quest
    HERB,

    //Skill
    DOUBLEJUMP,
    CLIMB,
    DASH,

    //Hp
    HP
}


[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public EItemType ItemType;
    public EItemID ItemID;
    public ESkillID skillID;

    public string itemName;
    public Sprite itemImg;
    public string itemDesc;
}
