using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossDashData
{
    [Header("Visual")]
    [SerializeField] private GameObject attackBox;

    [Header("Condition")]
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 7f;

    [Header("Timing")]
    [SerializeField] private float readyTime = 0.5f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float recoveryTime = 0.7f;

    [Header("Movement")]
    [SerializeField] private float speed = 8f;

    public GameObject AttackBox => attackBox;

    public float MinDistance => minDistance;
    public float MaxDistance => maxDistance;

    public float ReadyTime => readyTime;
    public float DashTime => dashTime;
    public float RecoveryTime => recoveryTime;

    public float Speed => speed;
}

[System.Serializable]
public class BossSlamData
{
    [Header("Timing")]
    [SerializeField] private float readyTime = 0.3f;
    [SerializeField] private float airWaitTime = 0.45f;
    [SerializeField] private float impactTime = 0.15f;
    [SerializeField] private float recoveryTime = 0.5f;

    [Header("Movement")]
    [SerializeField] private float teleportHeight = 4f;
    [SerializeField] private float slamSpeed = 14f;

    public float ReadyTime => readyTime;
    public float AirWaitTime => airWaitTime;
    public float ImpactTime => impactTime;
    public float RecoveryTime => recoveryTime;

    public float TeleportHeight => teleportHeight;
    public float SlamSpeed => slamSpeed;
}
