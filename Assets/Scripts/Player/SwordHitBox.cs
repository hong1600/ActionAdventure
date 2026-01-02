using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    BoxCollider2D hitBox;

    HitStop hitStop;

    [SerializeField] int damage = 1;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] float hitStopSec;

    private void Awake()
    {
        hitBox = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        hitStop = GameManager.instance.HitStop;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (((1 << coll.gameObject.layer) & targetLayer) != 0)
        {
            ITakeDmg iTakeDmg = coll.GetComponentInParent<EnemyBase>().GetComponent<ITakeDmg>();

            if (iTakeDmg != null)
            {
                iTakeDmg.TakeDmg(damage, transform);

                hitStop.ApplyHitStop(hitStopSec);
            }
        }
    }
}
