using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : EnemyState
{
    public BossEnemy boss { get; private set; }

    public void InitBoss(BossEnemy _boss)
    {
        boss = _boss;
        enemy = _boss;

        SetState(new BossIdleState(this));
    }
}

public abstract class BossAIState : AIState
{
    protected BossEnemy boss;
    protected BossState bossState;

    public BossAIState(StateMachine _machine) : base(_machine)
    {
        bossState = (BossState)_machine;
        boss = bossState.boss;
    }
}

public class BossIdleState : BossAIState
{
    float idleTimer;

    public BossIdleState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        boss.StopMove();
        idleTimer = boss.IdleTime;

        boss.anim.PlayAnim(EEnemyAnim.IDLE);
    }

    public override void Execute()
    {
        if (boss.target == null)
        {
            boss.IsSearchTarget();
            return;
        }

        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            machine.SetState(new BossPattenState(machine));
        }
    }
}

public class BossMoveState : BossAIState
{
    public BossMoveState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        if (boss.target == null)
        {
            boss.StopMove();
            machine.SetState(new BossIdleState(machine));
            return;
        }

        if (boss.IsCanAttack())
        {
            boss.StopMove();
            machine.SetState(new BossAttackState(machine));
            return;
        }

        boss.MoveToTarget();
    }
}


public class BossPattenState : BossAIState
{
    public BossPattenState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        if (boss.target == null)
        {
            machine.SetState(new BossIdleState(machine));
            return;
        }

        //int random = Random.Range(0, 2);
        int random = 3;

        switch (random)
        {
            case 0:
                machine.SetState(new BossAttackState(machine));
                break;
            case 1:
                machine.SetState(new BossDashState(machine));
                break;
            case 2:
                machine.SetState(new BossSlamState(machine));
                break;
            case 3:
                machine.SetState(new BossProjectileState(machine));
                break;
        }
    }
}

public class BossAttackState : BossAIState
{
    bool isAttackStarted;

    public BossAttackState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        boss.StopMove();
        isAttackStarted = false;

        if (!boss.IsCanAttack())
        {
            machine.SetState(new BossMoveState(machine));
            return;
        }

        boss.TryAttack();
        isAttackStarted = true;
    }

    public override void Execute()
    {
        if (!isAttackStarted) return;

        if (!boss.IsAttacking())
        {
            machine.SetState(new BossIdleState(machine));
        }
    }
}

public class BossDashState : BossAIState
{
    enum EDashState
    {
        READY,
        DASH,
        RECOVERY
    }

    EDashState curState;

    Rigidbody2D rigid;

    float timer;
    float dashDir;

    public BossDashState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        rigid = boss.movement.rigid;

        boss.StopMove();

        Vector2 dir = boss.target.position - boss.transform.position;

        boss.movement.Turn(dir);

        dashDir = Mathf.Sign(dir.x);

        if (dashDir == 0f)
        {
            dashDir = boss.transform.localScale.x > 0f ? 1f : -1f;
        }

        timer = boss.Dash.ReadyTime;
        curState = EDashState.READY;
        boss.anim.PlayAnim(EEnemyAnim.DASH_READY);
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;

        switch (curState)
        {
            case EDashState.READY:
                Ready();
                break;

            case EDashState.DASH:
                Dash();
                break;

            case EDashState.RECOVERY:
                Recovery();
                break;
        }
    }

    public override void Exit()
    {
        StopDash();
    }

    private void Ready()
    {
        rigid.velocity = new Vector2(0f, rigid.velocity.y);

        if (timer > 0f) return;

        curState = EDashState.DASH;
        timer = boss.Dash.DashTime;

        boss.SetDashAttackBox(true);
        boss.anim.PlayAnim(EEnemyAnim.DASH);
    }

    private void Dash()
    {
        rigid.velocity = new Vector2(dashDir * boss.Dash.Speed, rigid.velocity.y);

        if(timer > 0f) return;

        boss.SetDashAttackBox(false);

        StopDash();

        curState = EDashState.RECOVERY;
        timer = boss.Dash.RecoveryTime;
    }

    private void Recovery()
    {
        StopDash();

        if (timer > 0f) return;

        machine.SetState(new BossIdleState(machine));
    }

    private void StopDash()
    {
        rigid.velocity = new Vector2(0f, rigid.velocity.y);
    }
}

public class BossSlamState : BossAIState
{
    private enum ESlamState 
    {
        READY,
        AIR_WAIT,
        SLAM,
        SPIKE,
    }

    private ESlamState curState;

    private Rigidbody2D rigid;
    private float timer;
    private Vector2 target;
    private bool wasGround;

    public BossSlamState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        rigid = boss.movement.rigid;

        boss.StopMove();
        rigid.gravityScale = 0f;
        boss.SetSlamAttackBox(false);

        target = boss.target.position;

        curState = ESlamState.READY;

        boss.anim.PlayAnim(EEnemyAnim.IDLE);
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;

        switch (curState) 
        {
            case ESlamState.READY:
                Ready(); 
                break;
            case ESlamState.AIR_WAIT:
                AirWait(); 
                break;
            case ESlamState.SLAM:
                Slam(); 
                break;
            case ESlamState.SPIKE:
                Spike(); 
                break;
        }
    }

    private void Ready()
    {
        boss.movement.Stop();
        timer = boss.Slam.ReadyTime;
        curState = ESlamState.AIR_WAIT;
    }

    private void AirWait()
    {
        if (timer > 0f) return;

        Vector2 airPos = new Vector2(target.x, target.y + boss.Slam.TeleportHeight);
        CameraManager.instance.CameraShake.ShakeCamera(0.3f);

        boss.transform.position = airPos;

        wasGround = true;
        timer = boss.Slam.AirWaitTime;
        curState = ESlamState.SLAM;
    }

    private void Slam()
    {
        if (timer > 0f) return;

        boss.SetSlamAttackBox(true);
        rigid.velocity = Vector2.down * boss.Slam.Speed;

        if (!boss.movement.IsGround)
        {
            wasGround = false;
            return;
        }

        if (!wasGround && boss.movement.IsGround)
        {
            boss.StopMove();
            boss.SetSlamAttackBox(false);

            boss.SpawnSpike();

            CameraManager.instance.CameraShake.ShakeCamera(1f);

            timer = boss.Slam.SpikeWaitTime;
            curState = ESlamState.SPIKE;
        }
    }

    private void Spike()
    {
        if (timer > 0f) return;

        rigid.gravityScale = 1f;

        machine.SetState(new BossIdleState(machine));
    }
}

public class BossProjectileState : BossAIState
{
    bool isStarted;

    public BossProjectileState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        boss.StopMove();
        isStarted = false;

        if (boss.target == null)
        {
            machine.SetState(new BossIdleState(machine));
            return;
        }

        boss.SweepProjectile();
        isStarted = true;
    }

    public override void Execute()
    {
        if (!isStarted) return;
        if (boss.IsProjectileShooting()) return;

        machine.SetState(new BossIdleState(machine));
    }
}