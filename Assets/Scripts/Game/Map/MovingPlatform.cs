using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float moveSpeed;

    int curIndex = 0;
    int dir = 1;

    private void Start()
    {
        if(wayPoints.Length < 2)
        {
            enabled = false;
        }

        transform.position = wayPoints[curIndex].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Transform target = wayPoints[curIndex];

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            if(curIndex == wayPoints.Length - 1) 
            {
                dir = -1;
            }
            else if (curIndex == 0)
            {
                dir = 1;
            }

            curIndex += dir;
        }
    }

    private void OnCollisionEnter2D(Collision2D _coll)
    {
        if (_coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _coll.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D _coll)
    {
        if (_coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _coll.transform.SetParent(null);
        }
    }
}
