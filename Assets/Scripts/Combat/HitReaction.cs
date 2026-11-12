using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class HitReaction : MonoBehaviour
{
    [SerializeField] float invincibleTime =  0.5f;
    [SerializeField] float knockBackDelay = 0.15f;

    Rigidbody2D rigid;
    KnockBack knockBack;
    Coroutine knockBackRoutine;

    float invincibleEndTime;

    public bool IsInvincible { get { return Time.time < invincibleEndTime; } }
    public bool IsKnockBack { get; private set; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        knockBack = new KnockBack();
    }

    public bool TryBeginHit()
    {
        if (!isActiveAndEnabled) return false;
        if (IsInvincible) return false;

        invincibleEndTime = Time.time + invincibleTime;

        return true;
    }

    public void ApplyKnockBack(Transform _attacker, HitEffectData _data)
    {
        if(!isActiveAndEnabled) return;
        if (_data == null) return;
        if (_data.isParry || !_data.useKnockBack) return;
        if(_attacker == null) return;

        if (knockBackRoutine != null)
        {
            StopCoroutine(knockBackRoutine);
            knockBackRoutine = null;
        }
        knockBackRoutine = StartCoroutine(StartKnockBack(_attacker, _data.knockBackPower));
    }

    IEnumerator StartKnockBack(Transform _attacker, float _power)
    {
        IsKnockBack = true;

        knockBack.Apply(rigid, _attacker, transform, _power);

        yield return new WaitForSeconds(knockBackDelay);

        rigid.velocity = Vector3.zero;

        IsKnockBack = false;
        knockBackRoutine = null;
    }

    private void OnDisable()
    {
        if (knockBackRoutine != null)
        {
            StopCoroutine(knockBackRoutine);
            knockBackRoutine = null;
        }

        IsKnockBack = false;
        invincibleEndTime = 0f;
    }
}
