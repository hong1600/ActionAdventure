using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : StateMachine
{
    public PlayerManager player;

    public void Init(PlayerManager _player)
    {
        this.player = _player;
        SetState(new PlayerCreateState(this));
    }
}

public abstract class PlayerAIState : AIState
{
    protected PlayerManager player;
    protected PlayerState playerState;

    public PlayerAIState(StateMachine _machine) : base(_machine)
    {
        playerState = (PlayerState)_machine;
        this.player = playerState.player;
    }
}

public class PlayerCreateState : PlayerAIState
{
    public PlayerCreateState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
        machine.SetState(new PlayerMoveState(machine));
    }
}

public class PlayerMoveState : PlayerAIState
{
    public PlayerMoveState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.A) && !player.PlayerCombat.isAttack)
        {
            machine.SetState(new PlayerAttackState(machine));
            return;
        }
    }
}

public class PlayerAttackState : PlayerAIState
{
    public PlayerAttackState(StateMachine _machine) : base(_machine) { }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (!player.PlayerCombat.isAttack)
        {
            machine.SetState(new PlayerMoveState(machine));
        }
    }
}

public class PlayerSkillState : PlayerAIState
{
    public PlayerSkillState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
    }
}
