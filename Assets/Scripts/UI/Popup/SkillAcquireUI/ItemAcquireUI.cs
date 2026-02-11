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

    public void SetSkillUI(TableSkill.Info _skill)
    {
        //skillImg.sprite = _skill.Img;
        skillNameText.text = $"{skillNameText}";
        skillDescText.text = $"{skillDescText}";
    }
}
