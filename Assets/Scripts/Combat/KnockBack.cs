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

    public void Apply(Vector2 _dir, float _power)
    {
        rigid.AddForce(_dir * _power, ForceMode2D.Impulse);
    }
}
