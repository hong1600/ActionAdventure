using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    SpriteRenderer render;

    [SerializeField] string bossName;

    [Header("Boss Common")]
    [SerializeField] float idleTime = 0.5f;

    [Header("Boss Dash")]
    [SerializeField] BossDashData dashData;

    [Header("Boss Slam")]
    [SerializeField] BossSlamData slamData;
    [SerializeField] BossSpikeSpawner spikeSpawner;
    public bool IsSpiking { get; private set; }

    [Header("Boss Projectile")]
    [SerializeField] BossProjectileShooter projectileShooter;

    [Header("Boss Edge Dash")]
    [SerializeField] BossEdgeDashData edgeDashData;

    [Header("Boss Area Burst")]
    [SerializeField] BossAreaBurstData areaBurstData;
    [SerializeField] BossAreaBurstSpawner areaBurstSpawner;

    [Header("Battle Active")]
    bool isBattleActive;

    public string BossName => bossName;

    public float IdleTime => idleTime;

    public BossDashData Dash => dashData;
    public BossSlamData Slam => slamData;
    public BossEdgeDashData EdgeDash => edgeDashData;
    public BossAreaBurstData AreaBurst => areaBurstData;

    protected override void InitStateMachine()
    {
        BossState bossState = new BossState();
        bossState.InitBoss(this);

        enemyState = bossState;

        render = GetComponent<SpriteRenderer>();
    }

    //Dash
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

    //Slam
    public void SetSlamAttackBox(bool _active)
    {
        slamData.AttackBox.SetActive(_active);
    }

    //Spike
    public void SpawnSpike()
    {
        spikeSpawner.SpawnSpike();
    }

    public void SetSpiking(bool _isSpiking)
    {
        IsSpiking = _isSpiking;
    }

    //Projectile
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

    //Teleport
    public void SetRenderColor(Color _color)
    {
        render.color = _color;
    }

    public void SetRenderVisible(bool _visible)
    {
        render.enabled = _visible;
    }

    //AreaBurst
    public int GetAreaBurstPointCount()
    {
        return areaBurstSpawner.PointCount;
    }

    public void SpawnAreaWarning(int _index)
    {
        areaBurstSpawner.SpawnWarning(_index);
    }

    public void ExplodeArea(int _index)
    {
        areaBurstSpawner.Explode(_index);
    }

    public void ClearAreaBurst()
    {
        areaBurstSpawner.Clear();
    }

    protected override bool CanUpdateState()
    {
        return isBattleActive;
    }

    public void StartBattle()
    {
        isBattleActive = true;
    }

    public void StopBattle()
    {
        isBattleActive = false;
        StopMove();
    }
}
