using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossDashData
{
    [Header("Visual")]
    [SerializeField] GameObject attackBox;

    [Header("Condition")]
    [SerializeField] float minDistance = 3f;
    [SerializeField] float maxDistance = 7f;

    [Header("Timing")]
    [SerializeField] float readyTime = 0.5f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float recoveryTime = 0.7f;

    [Header("Movement")]
    [SerializeField] float speed = 8f;

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
    [Header("Visual")]
    [SerializeField] GameObject attackBox;

    [Header("Timing")]
    [SerializeField] float readyTime = 0.3f;
    [SerializeField] float airWaitTime = 0.45f;
    [SerializeField] float spikeWaitTime = 2f;
    [SerializeField] float recoveryTime = 1f;

    [Header("Movement")]
    [SerializeField] float teleportHeight = 4f;
    [SerializeField] float speed = 14f;

    public GameObject AttackBox => attackBox;

    public float ReadyTime => readyTime;
    public float AirWaitTime => airWaitTime;
    public float SpikeWaitTime => spikeWaitTime;
    public float RecoveryTime => recoveryTime;

    public float TeleportHeight => teleportHeight;
    public float Speed => speed;
}
