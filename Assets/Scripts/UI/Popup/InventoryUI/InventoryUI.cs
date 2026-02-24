using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;

    [SerializeField] Transform itemPanel;
    [SerializeField] GameObject itemImgPrefab;

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
        GameObject itemImg = Instantiate(itemImgPrefab, itemPanel);

        itemImg.GetComponent<ItemSlot>().Init(_skill);
    }
}
