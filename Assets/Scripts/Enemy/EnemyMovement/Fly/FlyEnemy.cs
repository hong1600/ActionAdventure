using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyMovementBase
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float dirDistance = 1f;
    [SerializeField] float turnSpeed = 6f;

    Vector2 curDir;

    bool isBypassing;
    int bypassSign = 1;
    float bypassTimer;

    protected override void OnMove(Vector2 _dir)
    {
        if (enemyBase.target == null)
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        bool isBlocked = Physics2D.Raycast(transform.position, _dir, dirDistance, wallLayer);

        if(isBlocked) 
        {
            if (!isBypassing)
            {
                isBypassing = true;
                bypassTimer = 0;
                bypassSign = Random.value > 0.5f ? 1 : -1;
            }
        }
        else
        {
            isBypassing = false;
            bypassTimer = 0;
        }

        Vector2 desireDir;

        if (isBypassing)
        {
            bypassTimer += Time.deltaTime;

            Vector2 sideDir = Vector2.Perpendicular(_dir) * bypassSign;
            desireDir = sideDir.normalized;

            if(bypassTimer > 1) 
            {
                bypassSign *= -1;
                bypassTimer = 0;
            }
        }
        else
        {
            desireDir = _dir;
        }

        if (curDir == Vector2.zero) curDir = desireDir;

        curDir = Vector2.Lerp(curDir, desireDir, turnSpeed * Time.deltaTime);
        curDir.Normalize();

        rigid.velocity = curDir * moveSpeed;
    }
}
