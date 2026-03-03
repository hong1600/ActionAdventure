using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public UserProgresssData playerData = new UserProgresssData();
    public WorldProgressData worldData = new WorldProgressData();
    public InventoryData inventoryData = new InventoryData();
}

public class UserProgresssData
{
    public int curHp;
    public int curMp;
    public float playTime;
    public int Gold;
}

public class WorldProgressData
{
    public int lastCheckPointID = 0;
}

public class InventoryData
{
    public List<int> equipItem = new List<int>();
}