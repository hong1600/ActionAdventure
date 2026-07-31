using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovementState : StateMachine
{
    public PlayerManager playerManager;
    public PlayerMovement player;

    public void Init(PlayerMovement _player, PlayerManager playerManager)
    {
        this.player = _player;
        this.playerManager = playerManager;
        SetState(new PlayerGroundState(this));
    }
}

public abstract class PlayerMovementBaseState : AIState
{
    protected PlayerMovement player;
    protected PlayerManager playerManager;

    protected PlayerMovementState moveState;

    public PlayerMovementBaseState(StateMachine _machine) : base(_machine)
    {
        moveState = (PlayerMovementState)_machine;
        this.player = moveState.player;
        this.playerManager= moveState.playerManager;
    }
}

public class PlayerGroundState : PlayerMovementBaseState
{
    public PlayerGroundState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.IDLE);
    }

    public override void Execute()
    {
        player.InputX();

        if (!playerManager.PlayerCombat.isAttack)
        {
            if (Mathf.Abs(player.inputX) > 0)
            {
                playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.RUN);
            }
            else
            {
                playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.IDLE);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(player.CanJump()) 
            {
                player.DoJump();

                machine.SetState(new PlayerAirState(machine));
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (player.CanDash())
            {
                machine.SetState(new PlayerDashState(machine));
                return;
            }
        }

        if (!player.isGround)
        {
            machine.SetState(new PlayerAirState(machine));
            return;
        }

        if (player.CanClimbLadder())
        {
            machine.SetState(new PlayerClimbLadderState(machine));
            return;
        }
    }

    public override void FixedExecute()
    {
        player.Move();
    }
}

public class PlayerAirState : PlayerMovementBaseState
{
    public PlayerAirState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.JUMP);
    }

    public override void Execute()
    {
        player.InputX();

        if (!playerManager.PlayerCombat.isAttack)
        {
            if (player.rigid.velocity.y > 0)
            {
                playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.JUMP);
            }
            else
            {
                playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.FALL);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (player.CanJump())
                {
                    player.DoJump();
                }
            }

            if (player.isGround)
            {
                machine.SetState(new PlayerGroundState(machine));
                return;
            }

            if (player.CanWallSlide())
            {
                machine.SetState(new PlayerWallSlideState(machine));
                return;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (player.CanDash())
                {
                    machine.SetState(new PlayerDashState(machine));
                    return;
                }
            }

            if (player.CanClimbLadder())
            {
                machine.SetState(new PlayerClimbLadderState(machine));
                return;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.StopJump();
        }
    }

    public override void FixedExecute()
    {
        player.Move();
        player.Gravity();
        player.JumpHold();
    }
}

public class PlayerDashState : PlayerMovementBaseState
{
    float dashTimer;

    public PlayerDashState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        player.SetDash(true);

        dashTimer = player.DashTime;

        if (!player.isGround)
        {
            player.AddDashCount();
        }

        playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.DASH);
    }

    public override void FixedExecute()
    {
        dashTimer -= Time.fixedDeltaTime;

        player.DoDash();

        if (dashTimer <= 0)
        {
            if (player.isGround)
            {
                machine.SetState(new PlayerGroundState(machine));
            }
            else
            {
                machine.SetState(new PlayerAirState(machine));
            }
        }
    }

    public override void Exit()
    {
        player.SetDash(false);
    }
}

public class PlayerWallSlideState : PlayerMovementBaseState
{
    public PlayerWallSlideState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.WALLSLIDE);
    }

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.DoWallJump();

            machine.SetState(new PlayerAirState(machine));
            return;
        }

        if (player.isGround)
        {
            machine.SetState(new PlayerGroundState(machine));
            return;
        }

        if (!player.CanWallSlide())
        {
            machine.SetState(new PlayerAirState(machine));
            return;
        }
    }

    public override void FixedExecute()
    {
        player.FallWall();
    }
}

public class PlayerClimbLadderState : PlayerMovementBaseState
{
    public PlayerClimbLadderState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        player.StartClimbLadder();
        playerManager.PlayerAnimation.ChangeAnim(EPlayerAnim.CLIMBLADDER);
    }

    public override void Execute()
    {
        player.InputY();

        if (!player.isNearLadder)
        {
            machine.SetState(new PlayerAirState(machine));
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.DoJump();
            machine.SetState(new PlayerAirState(machine));
            return;
        }
    }

    public override void FixedExecute()
    {
        player.ClimbLadder();
    }

    public override void Exit()
    {
        player.EndClimbLadder();
    }
}
