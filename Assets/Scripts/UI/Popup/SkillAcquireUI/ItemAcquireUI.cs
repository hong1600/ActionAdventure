using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAcquireUI : MonoBehaviour
{
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
        //skillImg.sprite = _skill.Img;
        skillNameText.text = $"{_skill.Name}";
        skillDescText.text = $"{_skill.Desc}";

        gameObject.SetActive(true);
    }
}
