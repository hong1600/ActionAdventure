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

public class OptionData
{
    public float masterVol = 1f;
    public float bgmVol = 1f;
    public float sfxVol = 1f;

    public int resWidth = 1920;
    public int resHeight = 1080;

    public EScreenMode screenMode = EScreenMode.FULLSCREEN;

    public int qualityLevel = 2;

    public ELangauge langauge = ELangauge.KR;
}