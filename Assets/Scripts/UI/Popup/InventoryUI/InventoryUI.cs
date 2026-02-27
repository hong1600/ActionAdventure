using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;

    [SerializeField] Transform itemPanel;
    [SerializeField] GameObject itemSlotPrefab;

    private void OnEnable()
    {
        Item.OnItemAcquired += AddItem;
    }

    private void OnDisable()
    {
        Item.OnItemAcquired -= AddItem;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    public void AddItem(TableSkill.Info _skill)
    {
        GameObject itemImg = Instantiate(itemSlotPrefab, itemPanel);

        itemImg.GetComponent<ItemSlot>().Init(_skill);
    }
}
