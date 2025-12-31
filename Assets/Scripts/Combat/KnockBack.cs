using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack
{
    Rigidbody2D rigid;

    public void Init(Rigidbody2D _rigid)
    {
        rigid = _rigid;
    }

    public void Apply(Transform _target, Transform _attacker, float _power)
    {
        Vector2 dir = (_target.transform.position - _attacker.transform.position).normalized;
        dir.y = 0.3f;

        rigid.velocity = Vector2.zero;

        rigid.AddForce(dir * _power, ForceMode2D.Impulse);
    }
}
