using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public event Action OnInputSpace;
    public event Action OnInputQ;
    public event Action OnInputI;

    public bool isInputLock { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (isInputLock) return;

        InputSpace();
        InputQ();
        InputI();
    }

    private void InputSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnInputSpace?.Invoke();
        }
    }

    private void InputQ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnInputQ?.Invoke();
        }
    }

    private void InputI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnInputI?.Invoke();
        }
    }

    public void LockInput() { isInputLock = true; }
    public void UnlockInput() { isInputLock = false; }
}
