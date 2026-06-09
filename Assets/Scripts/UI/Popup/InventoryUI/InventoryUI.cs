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

    private void OnEnable()
    {
        Item.OnItemAcquired += SetItem;
    }

    private void OnDisable()
    {
        Item.OnItemAcquired -= SetItem;
    }

    private void Start()
    {
        panel = UIManager.instance.GamePanel;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            if (!inventoryPanel.gameObject.activeSelf)
            {
                panel.OpenPanel(inventoryPanel, EPanelAnimType.FADE);
            }
            else
            {
                panel.ClosePanel(EPanelAnimType.FADE);
            }
        }

        if (!inventoryPanel.gameObject.activeSelf) return;
    }

    public void SetItem(TableSkill.Info _skill)
    {
    }
}
