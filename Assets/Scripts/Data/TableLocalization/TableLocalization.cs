using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELangauge
{
    KR,
    EN
}

public class TableLocalization : TableBase
{
    [Serializable]
    public class Info
    {
        public string ID;
        public string KR;
        public string EN;
    }

    public ELangauge curLanguage;

    public Dictionary<string, Info> Dictionary = new Dictionary<string, Info>();

    public string Get(string _id)
    {
        if (Dictionary.ContainsKey(_id) == false) return _id;

        Info info = Dictionary[_id];

        if(curLanguage == ELangauge.KR) return info.KR;
        else return info.EN;
    }

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<string, Info>>(_Name, ref Dictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, Dictionary);
    }

    public void Init_Csv(ETable _Name, int _StartRow, int _StartCol)
    {
        Dictionary.Clear();
        CSVReader reader = GetCSVReader(_Name);

        for (int row = _StartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            Dictionary.Add(info.ID, info);
        }
    }

    public bool Read(CSVReader _Reader, Info _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.getString(_Row, ref _Info.ID);
        _Reader.getString(_Row, ref _Info.KR);
        _Reader.getString(_Row, ref _Info.EN);

        return true;
    }

}
