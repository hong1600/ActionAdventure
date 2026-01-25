using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CombatManager combatManager;
    [SerializeField] PlayerSpawner playerSpawner;

    protected override void Awake()
    {
        base.Awake();
    }

    public CombatManager CombatManager { get { return combatManager; } }
    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
}
