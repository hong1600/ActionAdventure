using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    PlayerStatus playerStatus;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    public PlayerMovement PlayerMovement { get { return playerMovement; } }
    public PlayerCombat PlayerCombat { get { return playerCombat; } }
    public PlayerStatus PlayerStatus { get { return playerStatus; } }
}
