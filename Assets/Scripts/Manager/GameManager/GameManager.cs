using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CombatManager combatManager;
    [SerializeField] PlayerSpawner playerSpawner;

    public CombatManager CombatManager { get { return combatManager; } }
    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
}
