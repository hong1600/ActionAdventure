using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerAnim
{
    NONE,
    IDLE,
    RUN,
    JUMP,
    FALL,
    WALLSLIDE,
    DASH,
    CLIMBLADDER
}


public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    EPlayerAnim curAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnim(EPlayerAnim _eAnim)
    {
        if (curAnim == _eAnim) return;

        curAnim = _eAnim;

        anim.Play(_eAnim.ToString());
    }

    public void PlayAttack()
    {
        anim.SetTrigger("IsAttack");

        curAnim = EPlayerAnim.NONE;
    }
}
