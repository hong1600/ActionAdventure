using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public UserProgresssData playerData;
    public WorldProgressData worldData;
    public InventoryData inventoryData;
}

public class UserProgresssData
{
    public int curHp;
    public int curMp;
}

public class WorldProgressData
{

}

public class InventoryData
{
    public List<int> equipItem;
}