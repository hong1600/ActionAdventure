using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class TableEditor : MonoBehaviour
{
    static public TableManager tableManager;

    static public void Init()
    {
        if (tableManager == null)
        {
            tableManager = new TableManager();
        }
    }

    static public void ParseTableCsv(ETable _tableName)
    {
        Init();
        tableManager.Init(_tableName);
        tableManager.Save(_tableName);
    }

    [MenuItem("CSV_Util/Table/SkillData/Parse Skill CSV &F3", false, 1)]
    static public void ParserSkillCsv()
    {
        ParseTableCsv(ETable.SKILLDATA);
    }

    [MenuItem("CSV_Util/Table/ItemData/Parse Item CSV &F4", false, 1)]
    static public void ParserItemCsv()
    {
        ParseTableCsv(ETable.ITEMDATA);
    }

    [MenuItem("CSV_Util/Table/LocalizationData/Parse Localization CSV &F4", false, 1)]
    static public void ParserLocalizationCsv()
    {
        ParseTableCsv(ETable.LOCALIZATIONDATA);
    }
}
