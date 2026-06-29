using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    PanelUI panel;

    [SerializeField] CanvasGroup questPanel;
    [SerializeField] Transform questParent;
    [SerializeField] QuestSlot questSlotPrefab;

    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI rewardText;

    private void Start()
    {
        panel = UIManager.instance.GamePanel;

        QuestManager.instance.OnQuestAccepted += AddQuestSlot;
        QuestManager.instance.OnQuestCompleted += RemoveQuestSlot;
        InputManager.instance.OnInputQ += OpenQuestPanel;
    }

    private void OnDestroy()
    {
        QuestManager.instance.OnQuestAccepted -= AddQuestSlot;
        QuestManager.instance.OnQuestCompleted -= RemoveQuestSlot;
        InputManager.instance.OnInputQ -= OpenQuestPanel;
    }

    private void AddQuestSlot(QuestData _data)
    {
        QuestSlot slot = Instantiate(questSlotPrefab, questParent);
        slot.SetQuest(_data);
    }

    private void RemoveQuestSlot(QuestData _data)
    {
        for(int i = questParent.childCount - 1; i >= 0; i--) 
        {
            QuestSlot slot = questParent.GetChild(i).GetComponent<QuestSlot>();

            if (slot.questData.questID == _data.questID)
            {
                Destroy(slot.gameObject);
                break;
            }
        }
    }

    private void OpenQuestPanel()
    {
        if (questPanel.gameObject.activeSelf) return;

        panel.OpenPanel(questPanel, EPanelAnimType.FADE);
    }

    public void UpdateQuestDesc(QuestData _data)
    {
        descText.text = _data.questDesc;
        rewardText.text = _data.rewardAmount.ToString();
    }
}
