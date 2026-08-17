using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorSwitch : Switch
{
    TrapDoor trapDoor;

    protected override void Awake()
    {
        base.Awake();

        trapDoor = GetComponentInParent<TrapDoor>();
    }

    protected override bool Interaction()
    {
        trapDoor.OpenDoor();
        return true;
    }
}
