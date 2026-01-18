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
    NONE,
    ATTACK,
    DASH,
    HIT,
    JUMPLAND,
    JUMPUP,
    RUN
}

public enum EBgm
{
    LOBBY,
}

public enum EPlayerState
{
    NONE,
    DIE,
}

public enum EEffect
{
    NONE,
    PLAYERHITEFFECT
}

public enum EHitLevel
{
    NONE,
    LIGHT,
    HEAVY,
    FINISH
}