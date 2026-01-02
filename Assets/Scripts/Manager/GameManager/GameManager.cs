using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerSpawner playerSpawner;
    [SerializeField] HitStop hitStop;

    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
    public HitStop HitStop { get { return hitStop; } }
}
