using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] Image questImg;
    [SerializeField] TextMeshProUGUI questTypeText;

    [SerializeField] Image gradationImg;

    public QuestData questData { get; private set; }

    public void SetQuest(QuestData _data)
    {
        questData = _data;

        questName.text = _data.questName;
        questImg.sprite = _data.questImg;

        questTypeText.text = QuestTypeInfo.GetText(_data.questType);
        gradationImg.color = QuestTypeInfo.GetColor(_data.questType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameUI.instance.BoardUI.UpdateQuestDesc(questData);
        GameUI.instance.QuestUI.UpdateQuestDesc(questData);

        gradationImg.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gradationImg.gameObject.SetActive(false);
    }

    public void OnClickAcceptQuest()
    {
        bool isAccepted = QuestManager.instance.AcceptQuest(questData);

        if (!isAccepted) return;

        Destroy(this.gameObject);

        GameUI.instance.BoardUI.AcceptQuestEffect(questData);
    }
}
