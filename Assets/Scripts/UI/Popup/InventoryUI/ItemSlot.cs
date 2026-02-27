using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image img;

    TableSkill.Info tableSkill;

    public void Init(TableSkill.Info _skill)
    {
        tableSkill = _skill;

        img.sprite = SpriteManager.instance.GetSprite(_skill.SpriteName);
    }
}
