using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    PanelUI panel;

    [SerializeField] CanvasGroup inventoryPanel;

    [SerializeField] Transform itemPanel;
    [SerializeField] GameObject itemSlotPrefab;

    List<ItemSlot> slotList = new List<ItemSlot>();

    private void Start()
    {
        panel = UIManager.instance.GamePanel;

        SkillItem.OnSkillItemAcquired += SetSkillItem;
        InputManager.instance.OnInputI += OpenInventoryPanel;
    }

    private void OnDestroy()
    {
        SkillItem.OnSkillItemAcquired -= SetSkillItem;
        InputManager.instance.OnInputI -= OpenInventoryPanel;
    }

    private void OpenInventoryPanel()
    {
        if (inventoryPanel.gameObject.activeSelf) return;

        panel.OpenPanel(inventoryPanel, EPanelAnimType.FADE);
    }

    public void SetSkillItem(ItemData _data)
    {


    }
}
