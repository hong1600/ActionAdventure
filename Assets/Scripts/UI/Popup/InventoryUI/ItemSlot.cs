using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    TableSkill.Info tableSkill;

    [SerializeField] Image img;

    [SerializeField] ItemSlot up;
    [SerializeField] ItemSlot down;
    [SerializeField] ItemSlot left;
    [SerializeField] ItemSlot right;

    public void Init(TableSkill.Info _skill)
    {
        tableSkill = _skill;

        img.sprite = SpriteManager.instance.GetSprite(_skill.SpriteName);
    }
}
