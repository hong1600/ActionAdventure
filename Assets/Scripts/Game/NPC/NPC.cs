using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] string npcName;

    [SerializeField] DialogueData[] normalDatas;
    [SerializeField] DialogueData[] questStartDatas;
    [SerializeField] DialogueData[] questProgressDatas;
    [SerializeField] DialogueData[] questCompleteDatas;

    [SerializeField] Vector3 offset;

    [SerializeField] EDialogueType type;

    public Vector3 interactionOffset { get { return offset; } }

    public void Interact()
    {
        switch (type) 
        {
            case EDialogueType.NORMAL:
                DialogueManager.instance.StartDialogue(normalDatas, npcName);
                break;
            case EDialogueType.QUESTSTART:
                DialogueManager.instance.StartDialogue(questStartDatas, npcName);
                break;
            case EDialogueType.QUESTPROGRESS:
                DialogueManager.instance.StartDialogue(questProgressDatas, npcName);
                break;
            case EDialogueType.QUESTCOMPLETE:
                DialogueManager.instance.StartDialogue(questCompleteDatas, npcName);
                break;
        }
    }

    public void SetDialogueType(EDialogueType _type)
    {
        type = _type;
    }
}
