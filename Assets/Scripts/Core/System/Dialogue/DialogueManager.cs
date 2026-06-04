using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    DialogueData[] curData;

    int curDataIndex;
    int curLineIndex;

    protected override void Awake()
    {
        base.Awake();
    }

    public void StartDialogue(DialogueData[] _data)
    {
        curData = _data;

        curDataIndex = 0;
        curLineIndex = 0;

        GameUI.instance.DialogueUI.OpenDialogue();

        ShowCurLine();
    }

    public void NextLine()
    {
        curLineIndex++;

        if(curLineIndex >= curData[curDataIndex].lines.Length) 
        {
            EndDialogue();
            return;
        }

        ShowCurLine();
    }

    private void ShowCurLine()
    {
        string text = curData[curDataIndex].lines[curLineIndex];

        GameUI.instance.DialogueUI.ShowText(text);
    }

    private void EndDialogue()
    {
        GameUI.instance.DialogueUI.CloseDialogue();
    }
}
