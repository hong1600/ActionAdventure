using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    PlayerManager playerManager;

    public bool isAttack { get; private set; } = false;

    [SerializeField] GameObject attackBox;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && CanAttack())
        {
            DoAttack();
        }
    }

    private void DoAttack()
    {
        playerManager.PlayerAnimation.PlayAttack();
    }

    public void AttackStart()
    {
        isAttack = true;
        attackBox.SetActive(true);
        AudioManager.instance.PlaySfx(ESfx.ATTACK, transform.position, transform);
    }

    private bool CanAttack()
    {
        if (isAttack) return false;
        if(playerManager.PlayerMovement.isDash) return false;
        if (playerManager.PlayerMovement.isWall) return false;

        return true;
    }

    public void AttackEnd()
    {
        attackBox.SetActive(false);
        isAttack = false;
    }

    public void DoDashDamage() { }
}
