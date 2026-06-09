using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    PanelUI panel;
    GameState gameState;

    [SerializeField] CanvasGroup boardPanel;
    [SerializeField] GameObject questPrefab;
    [SerializeField] Transform questParent;

    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI questDesc;
    [SerializeField] TextMeshProUGUI rewardAmount;
    [SerializeField] Image rewardImg;

    private void Start()
    {
        panel = UIManager.instance.GamePanel;
        gameState = GameManager.instance.GameState;
    }

    public void OpenPanel()
    {
        panel.OpenPanel(boardPanel, EPanelAnimType.FADE);
        gameState.SetState(EGameState.DONTMOVE);
    }

    public void UpdateQuestDesc(QuestData _data)
    {
        questName.text = _data.questName;
        questDesc.text = _data.questDesc;
        rewardAmount.text = _data.rewardAmount.ToString();
        rewardImg.sprite = _data.rewardImg;
    }

    public void InitBoard(List<QuestData> _dataList)
    {
        for (int i = 0; i < _dataList.Count; i++)
        {
            if (_dataList[i].questState == EQuestState.CANSTART)
            {
                GameObject go = Instantiate(questPrefab, questParent);

                BoardQuestPrefab boardQuestPrefab = go.GetComponent<BoardQuestPrefab>();
                boardQuestPrefab.SetQuest(_dataList[i]);
            }
        }

        UpdateQuestDesc(_dataList[0]);
    }

}
