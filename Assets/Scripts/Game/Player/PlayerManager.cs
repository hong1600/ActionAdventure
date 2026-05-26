using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerCombat playerCombat;
    PlayerStatus playerStatus;
    PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerStatus = GetComponent<PlayerStatus>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public PlayerMovement PlayerMovement { get { return playerMovement; } }
    public PlayerCombat PlayerCombat { get { return playerCombat; } }
    public PlayerStatus PlayerStatus { get { return playerStatus; } }
    public PlayerAnimation PlayerAnimation { get {  return playerAnimation; } }
}
