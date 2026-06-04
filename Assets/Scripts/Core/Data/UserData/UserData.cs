using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public UserProgressData playerData = new UserProgressData();
    public WorldProgressData worldData = new WorldProgressData();
    public InventoryData inventoryData = new InventoryData();
}

[Serializable]
public class UserProgressData
{
    public int curHp;
    public int curMp;
    public float playTime;
    public int Gold;
}

[Serializable]
public class WorldProgressData
{
    public EMap eLastMap = EMap.MAP1;
}

[Serializable]
public class InventoryData
{
    public List<int> equipItem = new List<int>();
}

[Serializable]
public class OptionData
{
    public float masterVol = 0.7f;
    public float bgmVol = 1f;
    public float sfxVol = 1f;

    public int resWidth = 1920;
    public int resHeight = 1080;

    public EScreenMode screenMode = EScreenMode.FULLSCREEN;

    public int qualityLevel = 2;

    public ELangauge langauge = ELangauge.KR;
}