using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    PanelUI panelUI;
    PlayerStatUI playerStatUI;

    GameState gameState;

    [SerializeField] CanvasGroup dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;

    Coroutine typingRoutine;
    public bool isTyping { get; private set; }

    private void Start()
    {
        panelUI = UIManager.instance.Panel;
        playerStatUI = GameUI.instance.PlayerStatUI;
        gameState = GameManager.instance.GameState;
    }

    public void OpenDialogue()
    {
        gameState.SetState(EGameState.DIALOAGUE);

        playerStatUI.HideStatUI();

        panelUI.OpenPanel(dialoguePanel, EPanelAnimType.SCALE);
    }

    public void CloseDialogue() 
    {
        playerStatUI.ShowStatUI();

        panelUI.ClosePanel(EPanelAnimType.SCALE);

        gameState.SetState(EGameState.PLAY);
    }

    public void ShowText(string _text)
    {
        if (typingRoutine != null)
        {
            StopCoroutine(typingRoutine);
        }

        typingRoutine = StartCoroutine(StartTyping(_text));
    }

    IEnumerator StartTyping(string _text)
    {
        isTyping = true;

        dialogueText.text = _text;
        dialogueText.maxVisibleCharacters = 0;

        dialogueText.ForceMeshUpdate();

        int count = dialogueText.textInfo.characterCount;

        int i = 0;

        while (i <= count)
        {
            dialogueText.maxVisibleCharacters = i;
            i++;

            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }

    public void SkipTyping()
    {
        if(!isTyping) 
        {
            return;
        }

        dialogueText.maxVisibleCharacters = int.MaxValue;
        isTyping = false;

        if (typingRoutine != null)
        {
            StopCoroutine (typingRoutine);
            typingRoutine = null;
        }
    }
}
