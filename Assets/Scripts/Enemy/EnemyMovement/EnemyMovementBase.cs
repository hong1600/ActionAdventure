using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementBase : MonoBehaviour
{
    protected EnemyBase enemyBase;

    public Rigidbody2D rigid { get; private set; }
    [SerializeField] Collider2D coll;
    EnemyAnim anim;

    [SerializeField] protected float moveSpeed;

    bool isMoving;

    protected bool isGround;
    public bool IsGround => isGround;

    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();

        anim = GetComponent<EnemyAnim>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();
    }

    public void Move(Vector2 _dir)
    {
        if (!isMoving) 
        {
            isMoving = true;
            anim.PlayAnim(EEnemyAnim.MOVE);
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
            anim.PlayAnim(EEnemyAnim.IDLE);
        }

        rigid.velocity = Vector2.zero;
    }

    public void Turn(Vector2 _dir)
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

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, coll.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));

        isGround = hit.collider != null;
    }
}
