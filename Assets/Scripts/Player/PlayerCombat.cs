using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    PlayerManager playerManager;

    [SerializeField] float attackDuration = 0.3f;

    public bool isAttack { get; private set; } = false;

    [SerializeField] GameObject attackEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !playerManager.PlayerCombat.isAttack)
        {
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        isAttack = true;
        anim.SetTrigger("IsAttack");
        AudioManager.instance.PlaySfx(ESfx.ATTACK, transform.position, transform);
        attackEffect.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackEffect.SetActive(false);
        isAttack = false;
    }

    public void DoDashDamage() { }
}
