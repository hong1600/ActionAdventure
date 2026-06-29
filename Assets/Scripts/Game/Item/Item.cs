using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    [SerializeField] Vector3 offset;
    public Vector3 interactionOffset { get { return offset; } }

    public void Interact()
    {
        InteractItem();
    }

    protected virtual void InteractItem()
    {
        Destroy(gameObject);
    }
}
