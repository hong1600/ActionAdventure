using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAcquireUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Image skillImg;
    [SerializeField] TextMeshProUGUI skillNameText;
    [SerializeField] TextMeshProUGUI skillDescText;
    [SerializeField] TextMeshProUGUI skillPushText;

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
        skillImg.sprite = Resources.Load<Sprite>(_skill.ImgPath);
        skillNameText.text = $"{_skill.NameKey}";
        skillDescText.text = $"{_skill.DescKey}";

        panel.SetActive(true);
    }
}
