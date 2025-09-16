using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    bool isAttack = false;

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

        yield return new WaitForSeconds(0.5f);

        isAttack = false;
    }

    public void DoDashDamage() { }
}
