using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyAnim
{
    IDLE,
    MOVE,
    ATTACK,
    DASH,
    DASH_READY,
    HIT,
    DIE
}

public class EnemyAnim : MonoBehaviour
{
    Animator anim;

    EEnemyAnim curAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim(EEnemyAnim _animType)
    {
        if (curAnim == _animType) return;

        curAnim = _animType;

        anim.Play(_animType.ToString());
    }

    public void DoDashDamage() 
    {
    }
}
