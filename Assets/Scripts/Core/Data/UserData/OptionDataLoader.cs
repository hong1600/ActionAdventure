using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionDataLoader
{
    OptionData optionData;
    public OptionData OptionData { get; private set; }

    string path;

    public void Init()
    {
        path = Path.Combine(Application.persistentDataPath, "OptionData.json");

        optionData = LoadOrCreate();

        OptionData = optionData;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(optionData, true);
        File.WriteAllText(path, json);
    }

    public OptionData LoadOrCreate()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            optionData = JsonUtility.FromJson<OptionData>(json);
        }
        else
        {
            optionData = new OptionData();
            Save();
        }

        return optionData;
    }
}
