using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataLoader
{
    public UserData curUserData;

    string fileName = "UserData";

    int curSlot = -1;

    string GetFilePath(int _index)
    {
        return Path.Combine(Application.persistentDataPath, fileName + _index + ".json");
    }

    public void SaveUserData()
    {
        if (curSlot < 0 || curUserData == null) return;

        string jsonData = JsonUtility.ToJson(curUserData, prettyPrint: true);

        string path = GetFilePath(curSlot);

        File.WriteAllText(path, jsonData);
    }

    public UserData LoadUserData(int _index)
    {
        curSlot = _index;

        string path = GetFilePath(curSlot);

        if(File.Exists(path)) 
        {
            string jsonData = File.ReadAllText(path);

            curUserData = JsonUtility.FromJson<UserData>(jsonData);

            return curUserData;
        }

        return null;
    }

    public UserData CreateUserData()
    {
        curUserData = new UserData();

        curUserData.playerData.curHp = 5;
        curUserData.playerData.curMp = 5;

        SaveUserData();

        return curUserData;
    }

    public UserData LoadPreviewData(int _index) 
    {
        string path = GetFilePath(_index);

        if (!File.Exists(path)) return null;

        string jsonData = File.ReadAllText(path);
        return JsonUtility.FromJson<UserData>(jsonData);
    }

    public void ClearUserData(int _index)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + _index + ".json");

        if (File.Exists(path)) 
        {
            File.Delete(path);
        }
    }
}
