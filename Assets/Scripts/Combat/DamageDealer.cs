using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int dmg = 1;
    [SerializeField] LayerMask targetLayers;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (((1 << coll.gameObject.layer) & targetLayers) != 0)
        {
            ITakeDmg target = coll.GetComponent<ITakeDmg>();

            if (target != null)
            {
                target.TakeDmg(dmg);
            }
        }
    }
}
