using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : EnemyState
{
    public BossEnemy boss { get; private set; }
    public Rigidbody2D rigid { get; private set; }

    public void InitBoss(BossEnemy _boss)
    {
        boss = _boss;
        enemy = _boss;
        rigid = _boss.movement.rigid;

        SetState(new BossIdleState(this));
    }
}

public abstract class BossAIState : AIState
{
    protected BossState bossState;
    protected BossEnemy boss;
    protected Rigidbody2D rigid;

    public BossAIState(StateMachine _machine) : base(_machine)
    {
        bossState = (BossState)_machine;
        boss = bossState.boss;
        rigid = bossState.rigid;
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

        float hpRatio = boss.CurHp / boss.MaxHp;

        int random = 2;

        //if (hpRatio > 0.5f)
        //{
        //    random = Random.Range(0, 5);
        //}
        //else
        //{
        //    random = Random.Range(0, 6);
        //}

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
            case 4:
                machine.SetState(new BossEdgeDashState(machine));
                break;
            case 5:
                machine.SetState(new BossAreaBurstState(machine));
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

    float timer;
    float dashDir;

    public BossDashState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
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

    private float timer;
    private Vector2 target;
    private bool wasGround;

    public BossSlamState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
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

            boss.SetSpiking(true);
            boss.SpawnSpike();

            CameraManager.instance.CameraShake.ShakeCamera(1f);

            curState = ESlamState.SPIKE;
        }
    }

    private void Spike()
    {
        if (boss.IsSpiking) return;

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

public class BossEdgeDashState : BossAIState
{
    enum EEdgeDashState
    {
        TELEPORT_READY,
        FADEOUT,
        FADEIN,
        DASH_READY,
        DASH,
        RECOVERY
    }

    EEdgeDashState curState;

    float timer;
    float dashDir;

    public BossEdgeDashState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        boss.StopMove();
        boss.SetDashAttackBox(false);

        boss.SetRenderColor(Color.red);

        boss.anim.PlayAnim(EEnemyAnim.TELEPORT_READY);

        timer = boss.EdgeDash.TeleportReadyTime;
        curState = EEdgeDashState.TELEPORT_READY;
    }

    public override void Execute()
    {
        switch (curState)
        {
            case EEdgeDashState.TELEPORT_READY:
                TeleportReady();
                break;

            case EEdgeDashState.FADEOUT:
                FadeOut();
                break;

            case EEdgeDashState.FADEIN:
                FadeIn();
                break;

            case EEdgeDashState.DASH_READY:
                ReadyDash();
                break;

            case EEdgeDashState.DASH:
                Dash();
                break;

            case EEdgeDashState.RECOVERY:
                Recovery();
                break;
        }
    }

    public override void Exit()
    {
        boss.StopMove();
        boss.SetDashAttackBox(false);
        boss.SetRenderColor(Color.red);
    }

    private void TeleportReady()
    {
        if (UpdateColorFade(Color.red, Color.black, boss.EdgeDash.TeleportReadyTime) == false) return;

        timer = boss.EdgeDash.FadeOutTime;
        curState = EEdgeDashState.FADEOUT;
    }

    private void FadeOut()
    {
        Color startColor = Color.black;
        Color endColor = Color.black;
        endColor.a = 0f;

        if (UpdateColorFade(startColor, endColor, boss.EdgeDash.TeleportReadyTime) == false) return;

        SetTeleportPosition();
        SetDashDirection();

        timer = boss.EdgeDash.FadeInTime;
        curState = EEdgeDashState.FADEIN;
    }

    private void FadeIn()
    {
        Color startColor = Color.black;
        startColor.a = 0f;
        Color endColor = Color.black;

        if (UpdateColorFade(startColor, endColor, boss.EdgeDash.TeleportReadyTime) == false) return;

        boss.anim.PlayAnim(EEnemyAnim.DASH_READY);

        timer = boss.EdgeDash.ReadyTime;
        curState = EEdgeDashState.DASH_READY;
    }

    private void ReadyDash()
    {
        if (UpdateColorFade(Color.black, Color.white, boss.EdgeDash.ReadyTime) == false) return;

        boss.anim.PlayAnim(EEnemyAnim.DASH);
        boss.SetDashAttackBox(true);

        timer = boss.EdgeDash.DashTime;
        curState = EEdgeDashState.DASH;
    }

    private void Dash()
    {
        rigid.velocity = new Vector2(dashDir * boss.EdgeDash.DashSpeed, rigid.velocity.y);

        timer -= Time.deltaTime;

        if (timer > 0f) return;

        boss.StopMove();
        boss.SetDashAttackBox(false);

        timer = boss.EdgeDash.RecoveryTime;
        curState = EEdgeDashState.RECOVERY;
    }

    private void Recovery()
    {
        timer -= Time.deltaTime;

        if (timer > 0f) return;

        machine.SetState(new BossPattenState(machine));
    }

    private void SetTeleportPosition()
    {
        float centerX = boss.EdgeDash.CenterPoint.position.x;
        float playerX = boss.target.position.x;

        float teleportX;

        if (playerX < centerX)
        {
            teleportX = playerX - boss.EdgeDash.TeleportOffset;
        }
        else
        {
            teleportX = playerX + boss.EdgeDash.TeleportOffset;
        }

        teleportX = Mathf.Clamp(teleportX, boss.EdgeDash.LeftPoint.position.x, boss.EdgeDash.RightPoint.position.x);

        rigid.position = new Vector2(teleportX, rigid.position.y);
    }

    private void SetDashDirection()
    {
        float centerX = boss.EdgeDash.CenterPoint.position.x;

        dashDir = Mathf.Sign(centerX - rigid.position.x);

        Vector2 dir = new Vector2(dashDir, 0f);

        boss.movement.Turn(dir);
    }

    private bool UpdateColorFade(Color _startColor, Color _endColor, float _duration)
    {
        timer -= Time.deltaTime;

        float ratio = 1f - timer / _duration;

        Color color = Color.Lerp(_startColor, _endColor, ratio);

        boss.SetRenderColor(color);

        return timer <= 0f;
    }
}

public class BossAreaBurstState : BossAIState
{
    enum EAreaBurstState
    {
        CHARGE,
        WARNING,
        ATTACK_WAIT,
        EXPLODE,
        RECOVERY
    }

    EAreaBurstState curState;

    float timer;
    int curIndex;

    public BossAreaBurstState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        boss.StopMove();

        curIndex = 0;

        boss.anim.PlayAnim(EEnemyAnim.AREABURST_READY);

        timer = boss.AreaBurst.ChargeTime;
        curState = EAreaBurstState.CHARGE;
    }

    public override void Execute()
    {
        timer -= Time.deltaTime;

        switch (curState)
        {
            case EAreaBurstState.CHARGE:
                Charge();
                break;

            case EAreaBurstState.WARNING:
                Warning();
                break;

            case EAreaBurstState.ATTACK_WAIT:
                AttackWait();
                break;

            case EAreaBurstState.EXPLODE:
                Explode();
                break;

            case EAreaBurstState.RECOVERY:
                Recovery();
                break;
        }
    }

    public override void Exit()
    {
        boss.ClearAreaBurst();
    }

    private void Charge()
    {
        if (timer > 0f)
        {
            return;
        }

        curIndex = 0;
        timer = 0f;

        curState = EAreaBurstState.WARNING;
    }

    private void Warning()
    {
        if (timer > 0f)
        {
            return;
        }

        boss.SpawnAreaWarning(curIndex);

        curIndex++;

        if (curIndex >= boss.GetAreaBurstPointCount())
        {
            timer = boss.AreaBurst.AttackWaitTime;
            curState = EAreaBurstState.ATTACK_WAIT;
            return;
        }

        timer = boss.AreaBurst.WarningInterval;
    }

    private void AttackWait()
    {
        if (timer > 0f)
        {
            return;
        }

        curIndex = 0;

        timer = 0f;
        curState = EAreaBurstState.EXPLODE;
    }

    private void Explode()
    {
        if (timer > 0f)
        {
            return;
        }

        boss.ExplodeArea(curIndex);

        curIndex++;

        if (curIndex >= boss.GetAreaBurstPointCount())
        {
            timer = boss.AreaBurst.RecoveryTime;
            curState = EAreaBurstState.RECOVERY;
            return;
        }

        timer = boss.AreaBurst.ExplodeInterval;
    }

    private void Recovery()
    {
        if (timer > 0f)
        {
            return;
        }

        machine.SetState(new BossIdleState(machine));
    }
}
