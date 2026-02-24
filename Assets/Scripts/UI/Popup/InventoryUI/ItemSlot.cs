using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Image img;

    TableSkill.Info tableSkill;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void Init(TableSkill.Info _skill)
    {
        tableSkill = _skill;

        img.sprite = SpriteManager.instance.GetSprite(_skill.SpriteName);
    }
}
