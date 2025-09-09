using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D cap;

    PlayerStatus playerStatus;

    float inputX;
    float curSpeed = 1;
    [SerializeField] float walkSpeed = 1;

    int lastDir = 1;

    [SerializeField] bool isGround;

    [SerializeField] float jumpSpeed = 5f; 
    [SerializeField] bool isJump;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float maxFallSpeed = -10f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (playerStatus.curState == EPlayerState.NONE)
        {
            InputX();
            Jump();
            Gravity();
            CheckGround();
            ChangeAnim();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigid.velocity = new Vector2(curSpeed, rigid.velocity.y);
    }

    private void InputX()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        curSpeed = inputX * walkSpeed;

        if (inputX != 0)
        {
            FlipX();
            ChangeAnim();
        }
    }

    private void FlipX()
    {
        if (inputX < 0 && lastDir != -1)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            lastDir = -1;
        }
        else if (inputX > 0 && lastDir != 1)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            lastDir = 1;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (isGround && isJump == false)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
                isJump = true;
                anim.SetTrigger("IsJump");
            }
        }
    }

    private void Gravity()
    {
        if (!isGround)
        {
            float newY = rigid.velocity.y + gravity * Time.deltaTime;

            if (newY < maxFallSpeed)
            {
                newY = maxFallSpeed;
            }

            rigid.velocity = new Vector2(rigid.velocity.x, newY);
        }
        else
        {
            if(rigid.velocity.y <= 0) 
            {
                isJump = false;
            }
        }
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(cap.bounds.center, Vector2.down, cap.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));

        isGround = hit.collider != null;
    }

    private void ChangeAnim()
    {
        anim.SetFloat("Horizontal", Mathf.Abs(inputX));
        anim.SetBool("IsGround", isGround);
        anim.SetBool("IsFalling", !isGround && rigid.velocity.y < 0);
    }
}
