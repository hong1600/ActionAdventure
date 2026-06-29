using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItem : MonoBehaviour
{
    Rigidbody2D rigid;
    CircleCollider2D coll;

    ItemData itemData;

    [SerializeField] float popPowerX = 1.5f;
    [SerializeField] float popPowerY = 5f;
    [SerializeField] float acquireDelay = 1f;

    bool isAcquire;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();

        isAcquire = false;

        PopOut();
    }

    public void SetItem(ItemData _data)
    {
        itemData = _data;
    }

    private void PopOut()
    {
        float dirX = Random.Range(-1f, 1);

        rigid.velocity = Vector2.zero;

        rigid.AddForce(new Vector2(dirX * popPowerX, popPowerY), ForceMode2D.Impulse);

        StartCoroutine(StartAcquireDelay());
    }

    IEnumerator StartAcquireDelay()
    {
        yield return new WaitForSeconds(acquireDelay);

        isAcquire = true;
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (!isAcquire) return;

        if (_coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Acquire();
        }
    }

    private void Acquire()
    {
        switch (itemData.ItemType)
        {
            case EItemType.QUEST:
                QuestManager.instance.CheckQuestItem(itemData);
                break;
            case EItemType.SKILL:
                SkillManager.instance.AcquireSkill(itemData);
                break;
            case EItemType.HP:

                break;
        }

        Destroy(gameObject);
    }
}
