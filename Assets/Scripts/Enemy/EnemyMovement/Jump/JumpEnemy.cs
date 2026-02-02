using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : EnemyMovementBase
{
    bool isJumping;

    [SerializeField] float jumpPowerX;
    [SerializeField] float jumpPowerY;

    [SerializeField] float jumpTimer;

    protected override void OnMove(Vector2 _dir)
    {
        if (!isGround || isJumping) return;

        StartCoroutine(StartJump(_dir));
    }

    public override void Stop()
    {
        if(isJumping) return;
        base.Stop();
    }

    IEnumerator StartJump(Vector2 _dir)
    {
        isJumping = true;

        yield return new WaitForSeconds(jumpTimer);

        Vector2 jumpVel;
        jumpVel.x = _dir.x * jumpPowerX;
        jumpVel.y = jumpPowerY;

        rigid.velocity = jumpVel;

        while (!isGround) yield return null;

        isJumping = false;
    }
}
