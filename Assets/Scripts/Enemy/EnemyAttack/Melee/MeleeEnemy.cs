using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyAttackBase
{
    [SerializeField] GameObject attackBox;

    protected override IEnumerator StartAttack(Transform _target)
    {
        base.StartAttack(_target);

        attackBox.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        attackBox.SetActive(false);
    }
}
