using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyAttackBase
{
    [SerializeField] GameObject attackBox;

    protected override IEnumerator StartAttack(Transform _target)
    {
        yield return base.StartAttack(_target);

        yield return new WaitForSeconds(0.5f);

        attackBox.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        attackBox.SetActive(false);

        yield return new WaitForSeconds(0.55f);
    }
}
