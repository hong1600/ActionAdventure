using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    Animator anim;

    [SerializeField] Vector3 offset;
    public Vector3 interactionOffset { get { return offset; } }

    [SerializeField] ItemData itemData;
    [SerializeField] GameObject dropItemPrefab;

    public bool isOpened { get; private set; } = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        Open();
    }

    private void Open()
    {
        anim.Play("Open");

        GameObject obj = Instantiate(dropItemPrefab, transform.position, Quaternion.identity);

        DropItem dropItem = obj.GetComponent<DropItem>();

        dropItem.SetItem(itemData);

        isOpened = true;
    }
}
