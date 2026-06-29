using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour, IInteractable
{
    BoardUI boardUI;

    [SerializeField] Vector3 offset;

    public Vector3 interactionOffset { get { return offset; } }

    private void Start()
    {
        boardUI = GameUI.instance.BoardUI;
    }

    public void Interact()
    {
        List<QuestData> questList = QuestManager.instance.GetCanStartQuestList();

        boardUI.InitBoard(questList);

        boardUI.OpenPanel();
    }

    public void UpdateBoard(GameObject _questPrefab)
    {
        Destroy(_questPrefab);
    }


}
