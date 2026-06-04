using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySlotUI : MonoBehaviour
{
    UserDataLoader userDataLoader;
    UserData userData;
    FadeUI fade;

    [SerializeField] int slotIndex;

    [SerializeField] GameObject newGame;
    [SerializeField] GameObject profile;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI playTimeText;

    [SerializeField] CanvasGroup deletePanel;
    [SerializeField] CanvasGroup profilePanel;
    [SerializeField] CanvasGroup newGamePanel;

    private void Start()
    {
        userDataLoader = DataManager.instance.UserData;

        userData = userDataLoader.LoadPreviewData(slotIndex);

        SetProfileUI();

        fade = UIManager.instance.Panel.Fade;
    }

    private void SetProfileUI()
    {
        if (userData == null)
        {
            newGame.SetActive(true);
            profile.SetActive(false);
        }
        else
        {
            goldText.text = userData.playerData.Gold.ToString();
            SetPlayTime();

            newGame.SetActive(false);
            profile.SetActive(true);
        }
    }

    private void SetPlayTime()
    {
        float totalPlayTime = (int)userData.playerData.playTime;

        float hour = totalPlayTime / 3600;
        float min = (totalPlayTime % 3600) / 60;

        playTimeText.text = $"{hour:00}H {min:00}M";
    }

    public void ClickDeleteBtn()
    {
        StartCoroutine(StartShowDelete());
    }

    public void ClickCancleBtn()
    {
        StartCoroutine(StartCancleDelete());
    }

    IEnumerator StartShowDelete()
    {
        profilePanel.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        profilePanel.gameObject.SetActive(false);

        deletePanel.alpha = 0f;
        deletePanel.gameObject.SetActive(true);
        deletePanel.DOFade(1, 0.5f);
    }

    IEnumerator StartCancleDelete()
    {
        deletePanel.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        deletePanel.gameObject.SetActive(false);

        profilePanel.alpha = 0f;
        profilePanel.gameObject.SetActive(true);
        profilePanel.DOFade(1, 0.5f);
    }

    public void ClickAgreeDeleteBtn(int _index)
    {
        StartCoroutine(StartDeleteData(_index));
    }

    IEnumerator StartDeleteData(int _index)
    {
        userDataLoader.ClearUserData(_index);
        deletePanel.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);

        deletePanel.gameObject.SetActive(false);

        newGamePanel.alpha = 0f;
        newGamePanel.gameObject.SetActive(true);
        newGamePanel.DOFade(1, 0.5f);
    }
}
