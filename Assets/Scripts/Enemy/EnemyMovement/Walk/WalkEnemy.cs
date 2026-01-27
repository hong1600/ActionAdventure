using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyMovementBase
{
    protected override void OnMove(Vector2 _dir)
    {
        rigid.velocity = new Vector2(_dir.x * moveSpeed, rigid.velocity.y);
    }
}
