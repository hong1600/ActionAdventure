using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySlotUI : MonoBehaviour
{
    UserDataLoader userDataLoader;
    UserData userData;

    [SerializeField] int slotIndex;

    [SerializeField] GameObject newGameText;
    [SerializeField] GameObject mapImg;
    [SerializeField] GameObject deleteBtn;
    [SerializeField] GameObject mapNameText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI playTimeText;


    private void Start()
    {
        userDataLoader = DataManager.instance.UserData;

        userData = userDataLoader.LoadPreviewData(slotIndex);

        SetProfileUI();
    }

    private void SetProfileUI()
    {
        if (userData == null)
        {
            newGameText.SetActive(true);
            mapImg.SetActive(false);
            goldText.gameObject.SetActive(false);
            playTimeText.gameObject.SetActive(false);
            deleteBtn.SetActive(false);
            mapNameText.SetActive(false);
        }
        else
        {
            goldText.text = userData.playerData.Gold.ToString();
            SetPlayTime();

            newGameText.SetActive(false);
            mapImg.SetActive(true);
            goldText.gameObject.SetActive(true);
            playTimeText.gameObject.SetActive(true);
            deleteBtn.SetActive(true);
            mapNameText.SetActive(true);
        }
    }

    private void SetPlayTime()
    {
        float totalPlayTime = (int)userData.playerData.playTime;

        float hour = totalPlayTime / 3600;
        float min = (totalPlayTime % 3600) / 60;

        playTimeText.text = $"{hour:00}H {min:00}M";
    }
}
