using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigid { get; private set; }
    CapsuleCollider2D cap;

    PlayerMovementState moveState;

    PlayerManager playerManager;
    GameState gameState;
    SkillManager skillManager;

    public float inputX { get; private set; }
    public float inputY { get; private set; } 

    float curSpeed = 1;
    [SerializeField] float walkSpeed = 1;

    int lastDir = 1;

    public bool isGround { get; private set; }
    bool wasGround;

    [SerializeField] float jumpPower = 5f; 
    [SerializeField] int jumpCount = 0;
    [SerializeField] float jumpHoldTime = 0.2f;
    [SerializeField] float jumpHoldForce = 10f;
    float jumpTimer;
    bool isJumpHolding;

    public bool isDash { get; private set; }
    [SerializeField] float dashSpeed = 10;
    [SerializeField] float dashTime = 0.2f;
    public float DashTime { get { return dashTime; } }
    int dashCount = 0;

    [SerializeField] int wallDir;
    public bool isWall { get; private set; }
    [SerializeField] float wallfallSpeed = -2f;

    [SerializeField] float wallJumpX = 7f;
    [SerializeField] float wallJumpY = 6f;
    float wallJumpTimer;
    bool isWallJump;

    [SerializeField] float gravityScale = -9.81f;
    [SerializeField] float maxFallSpeed = -10f;

    public bool isNearLadder { get; private set; }
    bool isClimbLadder;
    [SerializeField] float climbSpeed = 3f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        cap = GetComponent<CapsuleCollider2D>();

        moveState = new PlayerMovementState();

        playerManager = GetComponent<PlayerManager>();
        gameState = GameManager.instance.GameState;
        skillManager = SkillManager.instance;
    }

    private void Start()
    {
        moveState.Init(this, playerManager);

        GameManager.instance.GameState.OnStateChange += StopMove;
    }

    private void Update()
    {
        if (playerManager.PlayerStatus.IsDie) return;

        if (!gameState.CanPlayerControl()) return;

        InputX();
        CheckWall();
        CheckGround();
        CheckLand();
        CheckWallJumpTimer();

        moveState.Update();
    }

    private void FixedUpdate()
    {
        if (playerManager.PlayerStatus.HitEffect == null) return;

        if (playerManager.PlayerStatus.HitEffect.IsKnockBack || playerManager.PlayerStatus.IsDie) return;

        moveState.FixedUpdate();
    }

    public void Move()
    {
        if (isWallJump) return;

        rigid.velocity = new Vector2(curSpeed, rigid.velocity.y);
    }

    private void StopMove(EGameState _state)
    {
        if (_state == EGameState.DONTMOVE)
        {
            curSpeed = 0;
            playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.IDLE);
        }
    }

    public void InputX()
    {
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
            transform.localScale = new Vector3(-3f, 3f, 1f);

            lastDir = -1;
        }
        else if (inputX > 0 && lastDir != 1)
        {
            transform.localScale = new Vector3(3f, 3f, 1f);

            lastDir = 1;
        }
    }

    public bool CanJump()
    {
        if (jumpCount <= 0) return true;

        if (skillManager.IsUnlocked(ESkillID.DOUBLEJUMP))
        {
            return jumpCount < 2;
        }

        return false;
    }

    public void DoJump()
    {
        jumpCount++;

        rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);

        jumpTimer = jumpHoldTime;
        isJumpHolding = true;

        AudioManager.instance.PlaySfx(ESfx.JUMPUP, transform.position, transform);
    }

    public void JumpHold()
    {
        if (!isJumpHolding) return;

        jumpTimer -= Time.fixedDeltaTime;

        if (jumpTimer <= 0)
        {
            isJumpHolding = false;
            return;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            rigid.velocity += Vector2.up * jumpHoldForce * Time.fixedDeltaTime;
        }
    }

    public void StopJump()
    {
        isJumpHolding = false;

        if(rigid.velocity.y > 0f) 
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
        }
    }

    public bool CanDash()
    {
        if (!isGround && dashCount > 0 || !skillManager.IsUnlocked(ESkillID.DASH))
        {
            return false;
        }

        return true;
    }

    public void SetDash(bool _value)
    {
        isDash = _value;
    }

    public void DoDash()
    {
        rigid.velocity = new Vector2(lastDir * dashSpeed, 0);
    }

    public void AddDashCount()
    {
        dashCount++;
    }

    private void CheckWall()
    {
        isWall = false;
        wallDir = 0;

        if (isGround || !skillManager.IsUnlocked(ESkillID.CLIMB)) return;

        Vector2 origin = new Vector2(cap.bounds.center.x, cap.bounds.center.y - 0.2f);
        float dist = cap.bounds.extents.x + 0.05f;
        int wallMask = LayerMask.GetMask("Wall");

        RaycastHit2D leftHit = Physics2D.Raycast(origin, Vector2.left, dist, wallMask);
        RaycastHit2D rightHit = Physics2D.Raycast(origin, Vector2.right, dist, wallMask);

        if (leftHit.collider != null)
        {
            if (inputX <= 0)
            {
                isWall = true;
                wallDir = -1;
            }
        }
        else if (rightHit.collider != null) 
        {
            if(inputX >= 0)
            {
                isWall = true;
                wallDir = 1;
            }
        }
    }

    public bool CanWallSlide()
    {
        if (isGround) return false;

        if (!isWall) return false;

        if (rigid.velocity.y >= 0) return false;

        return true;
    }

    public void FallWall()
    {
        rigid.velocity = new Vector2(0, wallfallSpeed);
    }

    public void DoWallJump()
    {
        jumpCount = 1;

        isWallJump = true;
        wallJumpTimer = 0.15f;

        float jumpX = -wallDir * wallJumpX;
        float jumpY = wallJumpY;

        rigid.velocity = new Vector2(jumpX, jumpY);

        AudioManager.instance.PlaySfx(ESfx.JUMPUP, transform.position, transform);
    }

    private void CheckWallJumpTimer()
    {
        if(isWallJump) 
        {
            wallJumpTimer -= Time.deltaTime;

            if (wallJumpTimer <= 0)
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

    public void Gravity()
    {
        if (!isGround)
        {
            float newY = rigid.velocity.y + gravityScale * Time.deltaTime;

            if (newY < maxFallSpeed)
            {
                newY = maxFallSpeed;
            }

            rigid.velocity = new Vector2(rigid.velocity.x, newY);
        }
    }

    public void PlayFootStepSfx()
    {
        AudioManager.instance.PlaySfx(ESfx.FOOTSTEP, transform.position, transform);
    }

    public void PlayDashSfx()
    {
        AudioManager.instance.PlaySfx(ESfx.DASH, transform.position, transform);
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(cap.bounds.center, Vector2.down, cap.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground", "Wall"));

        bool nowGround = hit.collider != null;

        if (!isGround && nowGround)
        {
            jumpCount = 0;
            dashCount = 0;
        }

        isGround = nowGround;
    }

    public void SetLadder(bool _isNear)
    {
        isNearLadder = _isNear;

        if (_isNear == false)
        {
            isClimbLadder = false;
        }
    }

    public bool CanClimbLadder()
    {
        InputY();

        return isNearLadder && Mathf.Abs(inputY) > 0;
    }

    public void InputY()
    {
        inputY = Input.GetAxisRaw("Vertical");
    }

    public void StartClimbLadder()
    {
        rigid.gravityScale = 0f;
        rigid.velocity = Vector3.zero;
    }

    public void ClimbLadder()
    {
        rigid.velocity = new Vector2(0f, inputY * climbSpeed);
    }

    public void EndClimbLadder()
    {
        rigid.gravityScale = 1;
    }
}
