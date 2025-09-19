using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    [SerializeField] float attackSpeed = 0.3f;
    [SerializeField] LayerMask targetLayer;

    bool isAttack = false;

    [SerializeField] GameObject attackEffect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isAttack)
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

        yield return new WaitForSeconds(attackSpeed);

        attackEffect.SetActive(false);
        isAttack = false;
    }

    public void DoDashDamage() { }
}
