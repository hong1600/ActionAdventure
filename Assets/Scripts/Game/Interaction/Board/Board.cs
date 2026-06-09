using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour, IInteractable
{
    BoardUI boardUI;

    [SerializeField] List<QuestData> questList;

    [SerializeField] Vector3 offset;

    public Vector3 interactionOffset { get { return offset; } }

    private void Start()
    {
        boardUI = GameUI.instance.BoardUI;

        boardUI.InitBoard(questList);
    }

    public void Interact()
    {
        boardUI.OpenPanel();
    }

    public void UpdateBoard(GameObject _questPrefab)
    {
        Destroy(_questPrefab);
    }
}
