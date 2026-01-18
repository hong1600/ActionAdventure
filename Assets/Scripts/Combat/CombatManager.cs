using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] HitEffect hitEffect;
    [SerializeField] HitEffectTable hitEffectTable;

    public HitEffect HitEffect { get { return hitEffect; } }
    public HitEffectTable HitEffectTable { get {  return hitEffectTable; } }
}
