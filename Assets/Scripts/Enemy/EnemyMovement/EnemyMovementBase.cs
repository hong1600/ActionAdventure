using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementBase : MonoBehaviour
{
    EnemyAnim anim;
    protected Rigidbody2D rigid;

    [SerializeField] protected float moveSpeed;

    bool isMoving;

    private void Awake()
    {
        anim = GetComponent<EnemyAnim>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 _dir)
    {
        if (!isMoving) 
        {
            isMoving = true;
            anim.StartMove();
        }

        OnMove(_dir);
        Turn(_dir);
    }

    protected abstract void OnMove(Vector2 _dir);

    public virtual void Stop() 
    {
        if(isMoving) 
        {
            isMoving = false;
            anim.StopMove();
        }

        rigid.velocity = Vector2.zero;
    }

    protected void Turn(Vector2 _dir)
    {
        if (_dir.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_dir.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
