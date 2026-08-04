using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    Rigidbody rigid;

    [SerializeField] float lifeTime = 5f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector2 _dir, float _speed)
    {
        Vector2 dir = _dir.normalized;

        rigid.velocity = dir * _speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(gameObject, lifeTime);
    }
}
