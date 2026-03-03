using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public UserData curUserData { get; private set; }

    [SerializeField] CombatManager combatManager;
    [SerializeField] PlayerSpawner playerSpawner;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        curUserData = DataManager.instance.UserData.curUserData;

        if (curUserData != null)
        {
            playerSpawner.Spawn(curUserData.worldData.lastCheckPointID);
        }
        else
        {
            playerSpawner.Spawn(0);
        }
    }

    private void Update()
    {
        if (curUserData != null) 
        {
            curUserData.playerData.playTime += Time.deltaTime;
        }
    }

    public CombatManager CombatManager { get { return combatManager; } }
    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
}
