using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    PlayerManager playerManager;
    GameState gameState;

    public bool isAttack { get; private set; } = false;

    [SerializeField] GameObject attackBox;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        playerManager = GetComponent<PlayerManager>();
        gameState = GameManager.instance.GameState;
    }

    private void Update()
    {
        if (gameState.curState == EGameState.PLAY)
        {
            if (Input.GetKeyDown(KeyCode.A) && CanAttack())
            {
                playerManager.PlayerAnimation.PlayAttack();
            }
        }
    }

    public void AttackStart()
    {
        isAttack = true;
        AudioManager.instance.PlaySfx(ESfx.ATTACK, transform.position, transform);
    }

    public void DoAttack()
    {
        attackBox.SetActive(true);
    }

    public void AttackEnd()
    {
        attackBox.SetActive(false);
        isAttack = false;
    }

    private bool CanAttack()
    {
        if (isAttack) return false;
        if(playerManager.PlayerMovement.isDash) return false;
        if (playerManager.PlayerMovement.isWall) return false;

        return true;
    }

    public void DoDashDamage() { }
}
