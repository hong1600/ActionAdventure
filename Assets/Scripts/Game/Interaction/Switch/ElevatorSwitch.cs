using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    Elevator elevator;

    SpriteRenderer render;

    private void Awake()
    {
        elevator = GetComponentInParent<Elevator>();

        render = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (elevator.isMoving) return;

        if (_coll.gameObject.layer == LayerMask.NameToLayer("AttackBox"))
        {
            elevator.MoveNext();
            Flip();
        }
    }

    private void Flip()
    {
        if (render.flipX)
        {
            render.flipX = false;
        }
        else
        {
            render.flipX = true;
        }
    }
}
