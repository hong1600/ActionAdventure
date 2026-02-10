using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FlyEnemy : EnemyMovementBase
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float dirDistance = 1f;
    [SerializeField] float turnSpeed = 6f;

    Vector2 curDir;

    readonly float[] angleSteps = { 0f, 15f, -15f, 30f, -30f, 45f, -45f, 60f, -60f };

    protected override void OnMove(Vector2 _dir)
    {
        if (enemyBase.target == null)
        {
            rigid.velocity = Vector2.zero;
            return;
        }

        Vector2 desireDir = FindBestDir(_dir);

        if(curDir == Vector2.zero) curDir = desireDir;

        curDir = Vector2.Lerp(curDir, desireDir, turnSpeed * Time.deltaTime);
        curDir.Normalize();

        rigid.velocity = curDir * moveSpeed;
    }

    Vector2 FindBestDir(Vector2 _baseDir)
    {
        for (int i = 0; i < angleSteps.Length; i++)
        {
            Vector2 dir = Rotate(_baseDir, angleSteps[i]);

            if (!IsBlocked(dir)) return dir;
        }

        return curDir != Vector2.zero ? curDir : _baseDir;
    }

    bool IsBlocked(Vector2 _dir)
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, _dir, dirDistance, wallLayer);

        return hit.collider != null;
    }

    Vector2 Rotate(Vector2 _v, float _degree)
    {
        float rad = _degree * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2
            (_v.x * cos - _v.y * sin,
            _v.x * sin + _v.y * cos);
    }

    private void OnDrawGizmosSelected()
    {
        if (curDir == Vector2.zero) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            transform.position,
            transform.position + (Vector3)(curDir.normalized * dirDistance)
        );
    }
}
