using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.Common;
using System;

public class Elevator : MonoBehaviour
{
    [SerializeField] Rigidbody2D platform; 
    [SerializeField] Transform[] movePoints;

    [SerializeField] float moveSpeed = 3f;

    float targetY;
    float destinationY;
    int curPoint;

    [SerializeField] SpriteRenderer chainL;
    [SerializeField] SpriteRenderer chainR;

    float startPlatformY;
    float startChainLength;

    public bool isMoving { get; private set; }

    SecondOrderDynamics secondOrderDynamics = new SecondOrderDynamics(4f, 0.3f, -0.3f);

    private void Start()
    {
        curPoint = 0;

        Vector2 startPos = platform.position;
        startPos.y = movePoints[curPoint].position.y;

        platform.position = startPos;

        targetY = platform.position.y;
        destinationY = targetY;

        startPlatformY = platform.transform.localPosition.y;
        startChainLength = chainL.size.y;

        secondOrderDynamics.Reset(targetY);
    }

    private void FixedUpdate()
    {
        if(isMoving == false) return;

        targetY = Mathf.MoveTowards(targetY, destinationY, moveSpeed * Time.fixedDeltaTime);

        float y = secondOrderDynamics.Update(targetY, Time.fixedDeltaTime);

        platform.MovePosition(new Vector2(platform.position.x, y));

        UpdateChain(y);

        if (Mathf.Approximately(targetY, destinationY) &&
            Mathf.Abs(platform.position.y - destinationY) <= 0.02f)
        {
            platform.MovePosition(new Vector2(platform.position.x, destinationY));
            isMoving = false;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveNext();
        }
    }

    public void MoveNext()
    {
        if (isMoving) return;

        curPoint++;

        if (curPoint >= movePoints.Length)
        {
            curPoint = 0;
        }

        destinationY = movePoints[curPoint].position.y;
        isMoving = true;
    }

    private void UpdateChain(float _platformY)
    {
        Vector3 worldPos = platform.transform.position;
        worldPos.y = _platformY;

        float localY = platform.transform.parent.InverseTransformPoint(worldPos).y;

        float moveDistance = startPlatformY - localY;
        float chainLength = startChainLength + moveDistance;

        chainL.size = new Vector2(chainL.size.x, chainLength);
        chainR.size = new Vector2(chainR.size.x, chainLength);
    }
}
