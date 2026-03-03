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
    bool wasGround;

    [SerializeField] float jumpSpeed = 5f; 
    [SerializeField] int jumpCount = 0;

    [SerializeField] bool isDash = false;
    [SerializeField] float dashSpeed = 10;
    [SerializeField] float dashTime = 0.2f;
    float dashTimer;

    [SerializeField] int wallDir;
    [SerializeField] bool isWall;
    [SerializeField] bool isWallJump;
    [SerializeField] float wallfallSpeed = -2f;
    [SerializeField] float wallJumpX = 7f;
    [SerializeField] float wallJumpY = 6f;
    [SerializeField] float wallJumpLockTime = 0.15f;
    float wallJumpTimer;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float maxFallSpeed = -10f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider2D>();

        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (playerStatus.IsDie) return;

        InputX();
        Jump();
        Dash();
        CheckWall();
        Gravity();
        CheckGround();
        ChangeAnim();
        CheckLand();
    }

    private void FixedUpdate()
    {
        if (playerStatus.HitEffect == null) return;

        if (playerStatus.HitEffect.IsKnockBack || playerStatus.IsDie) return;

        if (isDash)
        {
            DoDash();
        }
        else if (isWall)
        {
            WallClimb();
        }
        else if(!isWallJump)
        {
            Move();
        }
        WallClimbJump();
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

    private void CheckWall()
    {
        isWall = false;
        wallDir = 0;

        if (isGround) return;

        Vector2 origin = cap.bounds.center;
        float dist = cap.bounds.extents.x + 0.05f;
        int wallMask = LayerMask.GetMask("Wall");

        RaycastHit2D leftHit = Physics2D.Raycast(origin, Vector2.left, dist, wallMask);
        RaycastHit2D rightHit = Physics2D.Raycast(origin, Vector2.right, dist, wallMask);

        if (leftHit.collider != null)
        {
            isWall = true;
            wallDir = -1;
        }
        else if (rightHit.collider != null) 
        {
            isWall = true;
            wallDir = 1;
        }
    }

    private void WallClimb()
    {
        rigid.velocity = new Vector2(0, wallfallSpeed);
    }

    private void WallClimbJump()
    {
        if(isWall && Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpCount = 2;

            float jumpX = -wallDir * wallJumpX;
            float jumpY = wallJumpY;

            rigid.velocity = new Vector2(jumpX, jumpY);

            isWall = false;
            isWallJump = true;
            wallJumpTimer = wallJumpLockTime;
        }
        if (isWallJump)
        {
            wallJumpTimer -= Time.fixedDeltaTime;

            if(wallJumpTimer < 0) 
            {
                isWallJump = false;
            }
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
        if (isDash || isWall) return;

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
