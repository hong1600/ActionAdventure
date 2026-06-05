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

public enum EInteractionResult
{
    NONE,
    OPENSHOP,
}

[System.Serializable]
public class DialogueData
{
    [TextArea]
    public string[] lines;

    public EDialogueType dialogueType;

    public EInteractionResult resultType;
}