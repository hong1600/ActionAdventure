using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack
{
    public void Apply(Rigidbody2D _rigid, Transform _attacker, Transform _target, float _power)
    {
        Vector2 dir = (_target.transform.position - _attacker.transform.position).normalized;
        dir.y = 0.3f;

        _rigid.velocity = Vector2.zero;

        _rigid.AddForce(dir * _power, ForceMode2D.Impulse);
    }
}
