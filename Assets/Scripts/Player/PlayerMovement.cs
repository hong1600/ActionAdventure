using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    CapsuleCollider2D cap;

    PlayerManager playerManager;

    float inputX;
    float curSpeed = 1;
    [SerializeField] float walkSpeed = 1;

    int lastDir = 1;

    [SerializeField] bool isGround;
    bool wasGround;

    [SerializeField] float jumpSpeed = 5f; 
    [SerializeField] int jumpCount = 0;

    [SerializeField] bool isDash = false;
    [SerializeField] float dashSpeed = 10;
    [SerializeField] float dashTime = 0.2f;
    float dashTimer;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float maxFallSpeed = -10f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();

        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        InputX();
        Jump();
        Dash();
        Gravity();
        CheckGround();
        ChangeAnim();
        CheckLand();
    }

    private void FixedUpdate()
    {
        if(isDash) 
        {
            DoDash();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        rigid.velocity = new Vector2(curSpeed, rigid.velocity.y);
    }

    private void InputX()
    {
        if (isDash) return;

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
            transform.localScale = new Vector3(-0.5f, 0.45f, 0.5f);

            lastDir = -1;
        }
        else if (inputX > 0 && lastDir != 1)
        {
            transform.localScale = new Vector3(0.5f, 0.45f, 0.5f);

            lastDir = 1;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (jumpCount < 2)
            {
                DoJump();
            }
        }
    }

    private void DoJump()
    {
        jumpCount++;

        rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);

        anim.SetTrigger("IsJump");
        AudioManager.instance.PlaySfx(ESfx.JUMPUP, transform.position, transform);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(!isDash) 
            {
                StartDash();
            }
        }
    }

    private void StartDash()
    {
        isDash = true;
        dashTimer = dashTime;

        rigid.velocity = new Vector2(lastDir * dashSpeed, 0);
    }

    private void DoDash()
    {
        dashTimer -= Time.fixedDeltaTime;

        rigid.velocity = new Vector2(lastDir * dashSpeed, 0);

        if (dashTimer <= 0)
        {
            isDash = false; 
        }
    }

    private void CheckLand()
    {
        if (!wasGround && isGround)
        {
            AudioManager.instance.PlaySfx(ESfx.JUMPLAND, transform.position, transform);
        }

        wasGround = isGround;
    }

    private void Gravity()
    {
        if (isDash) return;

        if (!isGround)
        {
            float newY = rigid.velocity.y + gravity * Time.deltaTime;

            if (newY < maxFallSpeed)
            {
                newY = maxFallSpeed;
            }

            rigid.velocity = new Vector2(rigid.velocity.x, newY);
        }
    }

    public void PlayFootStep()
    {
        AudioManager.instance.PlaySfx(ESfx.RUN, transform.position, transform);
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(cap.bounds.center, Vector2.down, cap.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));

        bool nowGround = hit.collider != null;

        if (!isGround && nowGround)
        {
            jumpCount = 0;
        }

        isGround = nowGround;
    }

    private void ChangeAnim()
    {
        anim.SetFloat("Horizontal", Mathf.Abs(inputX));
        anim.SetBool("IsGround", isGround);
        anim.SetBool("IsFalling", !isGround && rigid.velocity.y < 0);
    }
}
