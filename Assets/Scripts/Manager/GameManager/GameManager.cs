using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerSpawner playerSpawner;
    [SerializeField] HitEffect hitEffect;

    public PlayerSpawner PlayerSpawner { get { return playerSpawner; } }
    public HitEffect HitEffect { get { return hitEffect; } }
}
