using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    PanelUI panel;
    FadeUI fade;
    GameState gameState;

    [SerializeField] CanvasGroup boardPanel;
    [SerializeField] GameObject questSlotPrefab;
    [SerializeField] Transform questParent;

    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI questDesc;
    [SerializeField] TextMeshProUGUI rewardAmount;
    [SerializeField] Image rewardImg;

    [SerializeField] CanvasGroup acceptQuestPanel;
    [SerializeField] Image acceptEffectPanelImg;
    [SerializeField] Image acceptEffectQuestImg;
    [SerializeField] TextMeshProUGUI acceptText;
    [SerializeField] TextMeshProUGUI questNameText;
    [SerializeField] TextMeshProUGUI pressSpaceText;

    private void Start()
    {
        panel = UIManager.instance.GamePanel;
        fade = panel.Fade;
        gameState = GameManager.instance.GameState;

        InputManager.instance.OnInputSpace += CloseAcceptEffect;
    }

    private void OnDestroy()
    {
        InputManager.instance.OnInputSpace -= CloseAcceptEffect;
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
        if (_dataList == null) return;

        for (int i = 0; i < _dataList.Count; i++)
        {
            if (_dataList[i].questState == EQuestState.CANSTART)
            {
                GameObject go = Instantiate(questSlotPrefab, questParent);

                QuestSlot questSlot = go.GetComponent<QuestSlot>();
                questSlot.SetQuest(_dataList[i]);
            }
        }

        UpdateQuestDesc(_dataList[0]);
    }

    public void AcceptQuestEffect(QuestData _data)
    {
        StartCoroutine(StartAcceptEffect(_data));
    }

    IEnumerator StartAcceptEffect(QuestData _data)
    {
        acceptEffectQuestImg.sprite = _data.questImg;
        questNameText.text = _data.questName;

        acceptEffectQuestImg.transform.localScale = Vector3.zero;
        fade.SetAlpha(acceptEffectPanelImg, 0f);
        fade.SetAlpha(acceptEffectQuestImg, 1f);
        fade.SetAlpha(acceptText, 0f);
        fade.SetAlpha(pressSpaceText, 0f);

        acceptQuestPanel.gameObject.SetActive(true);

        yield return fade.FadeIn(acceptEffectPanelImg, 0.5f, 0.99f);

        Sequence seq = DOTween.Sequence();

        seq.Append(acceptEffectQuestImg.transform.DOScale(1.2f, 0.12f).SetEase(Ease.OutBack));
        seq.Append(acceptEffectQuestImg.transform.DOScale(0.95f, 0.08f));
        seq.Append(acceptEffectQuestImg.transform.DOScale(1f, 0.05f));

        yield return seq.WaitForCompletion();

        yield return fade.FadeIn(acceptText, 0.5f);

        pressSpaceText.DOFade(1f, 0.5f);
    }

    private void CloseAcceptEffect()
    {
        StartCoroutine(StartCloseAcceptPanel());
    }

    IEnumerator StartCloseAcceptPanel()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(acceptEffectQuestImg.transform.DOScale(0.85f, 0.15f).SetEase(Ease.InBack));
        seq.Join(acceptEffectQuestImg.DOFade(0f, 0.15f));

        seq.Join(acceptText.DOFade(0f, 0.15f));
        seq.Join(pressSpaceText.DOFade(0f, 0.15f));

        yield return seq.WaitForCompletion();

        yield return fade.StartFadeOut(acceptEffectPanelImg, 0.5f);

        acceptQuestPanel.gameObject.SetActive(false);
    }
}
