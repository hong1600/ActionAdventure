using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public UserData curUserData { get; private set; }

    [SerializeField] CombatManager combatManager;
    [SerializeField] PlayerSpawner playerSpawner;
    [SerializeField] MapLoader mapLoader;
    [SerializeField] GameState gameState;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        curUserData = DataManager.instance.UserData.curUserData;

        if (curUserData != null)
        {
            mapLoader.LoadMap(curUserData.worldData.eLastMap);
        }
        else
        {
            mapLoader.LoadMap(EMap.MAP2);
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
    public MapLoader MapLoader { get {  return mapLoader; } }
    public GameState GameState { get {  return gameState; } }
}
