using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    CREATE,
    MOVE,
    STAY,
    ATTACK,
    DIE
}

public enum EScene
{
    LOBBY,
    LOADING,
    INTRO,
    GAME,
}

public enum ESfx
{
    ATTACK,
    DASH,
    HIT,
    JUMPLAND,
    JUMPUP,
    FOOTSTEP,
    UICLICK,
    DEATH,
    ACQUIRE
}

public enum EBgm
{
    LOBBY,
}

public enum EPlayerState
{
    PLAY,
    DIE,
}

public enum EEffect
{
    PLAYERHITEFFECT
}

public enum EHitLevel
{
    NONE,
    PARRY,
    LIGHT,
    HEAVY,
    FINISH
}