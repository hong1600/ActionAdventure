using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataLoader : MonoBehaviour
{
    public UserData curUserData;

    string fileName = "UserData.json";

    string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public void SaveUserData()
    {
        string jsonData = JsonUtility.ToJson(curUserData, prettyPrint: true);

        string path = GetFilePath();

        File.WriteAllText(path, jsonData);
    }

    public UserData LoadUserData()
    {
        string path = GetFilePath();

        if(File.Exists(path)) 
        {
            string jsonData = File.ReadAllText(path);

            UserData userData = JsonUtility.FromJson<UserData>(jsonData);

            curUserData = userData;

            return userData;
        }

        return null;
    }

    public void ClearUserData()
    {
        string path = GetFilePath();

        if(File.Exists(path)) 
        {
            File.Delete(path);
        }
    }
}
