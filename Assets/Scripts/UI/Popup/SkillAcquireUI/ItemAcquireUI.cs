using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAcquireUI : MonoBehaviour
{
    TableLocalization localization;

    [SerializeField] GameObject panel;
    [SerializeField] Image skillImg;
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;
    [SerializeField] TextMeshProUGUI skillPushText;

    private void Start()
    {
        localization = DataManager.instance.TableLocalization;
    }

    private void OnEnable()
    {
        Item.OnItemAcquired += SetSkillUI;
    }

    private void OnDisable()
    {
        Item.OnItemAcquired -= SetSkillUI;
    }

    public void SetSkillUI(TableSkill.Info _skill)
    {
        skillNameText.text = localization.Get(_skill.NameKey);
        skillDescText.text = localization.Get(_skill.DescKey);

        skillImg.sprite = Resources.Load<Sprite>(_skill.ImgPath);

        panel.SetActive(true);
    }
}
