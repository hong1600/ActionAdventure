using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestTypeInfo
{
    public static string GetText(EQuestType _type)
    {
        switch (_type) 
        {
            case EQuestType.KILL:
                return "사냥";

            case EQuestType.COLLECT:
                return "수집";
        }

        return "";
    }

    public static Color GetColor(EQuestType _type) 
    {
        Color color = Color.white;

        switch (_type) 
        {
            case EQuestType.KILL:
                color = Color.red;
                break;

            case EQuestType.COLLECT:
                color = Color.green;
                break;
        }

        color.a = 0.4f;

        return color;
    }
}
