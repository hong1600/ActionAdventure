using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    SpriteRenderer render;

    [Header("Boss Common")]
    [SerializeField] float idleTime = 0.5f;

    [Header("Boss Dash")]
    [SerializeField] private BossDashData dashData;

    [Header("Boss Slam")]
    [SerializeField] private BossSlamData slamData;
    [SerializeField] SpikeSpawner spikeSpawner;

    [Header("Boss Projectile")]
    [SerializeField] BossProjectileShooter projectileShooter;

    public float IdleTime => idleTime;

    public BossDashData Dash => dashData;
    public BossSlamData Slam => slamData;

    protected override void InitStateMachine()
    {
        BossState bossState = new BossState();
        bossState.InitBoss(this);

        enemyState = bossState;

        render = GetComponent<SpriteRenderer>();
    }

    public bool CanDash()
    {
        if (target == null) return false;

        float distance = Vector2.Distance(transform.position, target.position);

        return distance >= dashData.MinDistance && distance <= dashData.MaxDistance;
    }

    public void SetDashAttackBox(bool _active)
    {
        dashData.AttackBox.SetActive(_active);
    }

    public void SetSlamAttackBox(bool _active)
    {
        slamData.AttackBox.SetActive(_active);
    }

    public void SpawnSpike()
    {
        spikeSpawner.SpawnSpike();
    }

    public bool IsProjectileShooting()
    {
        return projectileShooter.IsShooting;
    }

    public void SweepProjectile()
    {
        if(target == null) return;

        float dirX = target.position.x - transform.position.x;

        movement.Turn(new Vector2(dirX, 0f));
        projectileShooter.Sweep(dirX);
    }
}
