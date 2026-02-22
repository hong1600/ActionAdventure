using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    TableItem tableItem;
    TableSkill tableSkill;
    TableLocalization tableLocalization;

    protected override void Awake()
    {
        base.Awake();

        tableItem = new TableItem();
        tableItem.Init_Binary("ItemData");
        TableItem = tableItem;

        tableSkill = new TableSkill();
        tableSkill.Init_Binary("SkillData");
        TableSkill = tableSkill;

        tableLocalization = new TableLocalization();
        tableLocalization.Init_Binary("LocalizationData");
        TableLocalization = tableLocalization;
    }

    public TableItem TableItem { get; private set; }
    public TableSkill TableSkill { get; private set; }
    public TableLocalization TableLocalization { get; private set; }
}

