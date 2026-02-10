using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableManager
{
    public TableItem item = new TableItem();
    public TableSkill skill = new TableSkill();

    public void Init(ETable _name)
    {
        switch (_name)
        {
            case ETable.ITEMDATA:
#if UNITY_EDITOR
                item.Init_Csv(ETable.ITEMDATA, 1, 0);
#else
        item.Init_Binary($"{_name}");
#endif
                break;
            case ETable.SKILLDATA:
#if UNITY_EDITOR
                skill.Init_Csv(ETable.SKILLDATA, 1, 0);
#else
        unit.Init_Binary($"{_name}");
#endif
                break;
        }
    }

    public void Save(ETable _name)
    {
        switch (_name) 
        {
            case ETable.ITEMDATA:
                item.Save_Binary("ItemData");
                break;
            case ETable.SKILLDATA:
                skill.Save_Binary("SkillData");
                break;
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
