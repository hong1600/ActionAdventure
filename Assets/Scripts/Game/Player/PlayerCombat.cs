using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    [SerializeField] float attackDuration = 0.3f;

    public bool isAttack { get; private set; } = false;

    [SerializeField] GameObject attacBox;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
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
        attacBox.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attacBox.SetActive(false);
        isAttack = false;
    }

    public void DoDashDamage() { }
}
