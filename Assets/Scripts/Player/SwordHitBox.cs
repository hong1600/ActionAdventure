using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class SwordHitBox : MonoBehaviour
{
    BoxCollider2D hitBox;

    [SerializeField] int damage = 1;
    [SerializeField] LayerMask targetLayer;

    private void Awake()
    {
        hitBox = GetComponent<BoxCollider2D>();
        hitBox.enabled = false;
    }

    public void EnableHitBox()
    {
        hitBox.enabled = true;
    }

    public void DisableHitBox()
    {
        hitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (((1 << coll.gameObject.layer) & targetLayer) != 0)
        {
            ITakeDmg iTakeDmg = coll.GetComponent<ITakeDmg>();

            if (iTakeDmg != null)
            {
                iTakeDmg.TakeDmg(damage);
            }
        }
    }
}
