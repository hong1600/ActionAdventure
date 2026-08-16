using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField] float lifeTime = 5f;
    [SerializeField] float rotationOffset = -90f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 _dir, float _speed)
    {
        Vector2 dir = _dir.normalized;

        rigid.velocity = dir * _speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);

        Destroy(gameObject, lifeTime);
    }
}
