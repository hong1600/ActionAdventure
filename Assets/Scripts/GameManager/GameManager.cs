using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerSpawner playerSpawner;

    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
}
