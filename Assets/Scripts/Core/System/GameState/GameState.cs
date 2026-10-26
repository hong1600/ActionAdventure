using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    PLAY,
    STOP,
    DONTMOVE,
    DIALOGUE,
}

public class GameState : MonoBehaviour
{
    public event Action<EGameState> OnStateChange;

    public EGameState curState { get; private set; }


    private void Awake()
    {
        curState = EGameState.PLAY;
    }

    public void SetState(EGameState _state)
    {
        curState = _state;

        OnStateChange?.Invoke(_state);
    }

    public bool CanPlayerControl()
    {
        return curState == EGameState.PLAY;
    }
}
