using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDialogueType
{
    NORMAL,
    QUESTSTART,
    QUESTCOMPLETE,
    QUESTPROGRESS,
}

[System.Serializable]
public class DialogueData
{
    public string npcName;
    [TextArea]
    public string[] lines;
    public EDialogueType dialogueType;
}