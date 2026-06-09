using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardQuestPrefab : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] Image questImg;

    QuestData questData;

    public void SetQuest(QuestData _data)
    {
        questData = _data;

        questName.text = _data.questName;
        questImg.sprite = _data.questImg;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameUI.instance.BoardUI.UpdateQuestDesc(questData);
    }
}
