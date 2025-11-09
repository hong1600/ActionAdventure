using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : StateMachine
{
    public EnemyBase enemy;

    public void Init(EnemyBase _enemy)
    {
        this.enemy = _enemy;
        SetState(new EnemyCreateState(this));
    }
}

public abstract class EnemyAIState : AIState
{
    protected EnemyBase enemy;
    protected EnemyState enemyState;

    public EnemyAIState(StateMachine _machine) : base(_machine)
    {
        enemyState = (EnemyState)_machine;
        this.enemy = enemyState.enemy;
    }
}

public class EnemyCreateState : EnemyAIState
{
    public EnemyCreateState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        machine.SetState(new EnemySearchState(machine));
    }
}

public class EnemySearchState : EnemyAIState
{
    public EnemySearchState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        bool found = enemy.SearchTarget();

        if (found)
            machine.SetState(new EnemyMoveState(machine));
    }
}

public class EnemyMoveState : EnemyAIState
{
    public EnemyMoveState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        if (enemy.target == null)
        {
            machine.SetState(new EnemySearchState(machine));
            return;
        }

        if (enemy.ReadyAttack())
        {
            enemy.StopMove();
            machine.SetState(new EnemyAttackState(machine));
            return;
        }

        enemy.Move();
    }
}

public class EnemyAttackState : EnemyAIState
{
    public EnemyAttackState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
        enemy.Attack();
    }
}

public class EnemySkillState : EnemyAIState
{
    public EnemySkillState(StateMachine _machine) : base(_machine) { }

    public override void Execute()
    {
    }
}

