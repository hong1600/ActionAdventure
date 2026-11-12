using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    BossRoomController bossRoom;

    private void Awake()
    {
        bossRoom = GetComponent<BossRoomController>();
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (_coll.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;


        PlayerMovement movement = _coll.gameObject.GetComponent<PlayerMovement>();
        movement.rigid.velocity = Vector3.zero;
        bossRoom.BossBattle();
    }
}
