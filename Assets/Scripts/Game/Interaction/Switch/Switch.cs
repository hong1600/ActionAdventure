using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour
{
    SpriteRenderer render;

    protected virtual void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (_coll.gameObject.layer != LayerMask.NameToLayer("AttackBox")) return;

        if (Interaction() == false) return;

        Flip();
    }

    protected abstract bool Interaction();

    private void Flip()
    {
        render.flipX = !render.flipX;
    }
}
