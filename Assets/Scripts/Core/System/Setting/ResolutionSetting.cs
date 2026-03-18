using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionText;

    Resolution[] allRes;
    List<Resolution> filterResList = new List<Resolution>();

    List<Vector2Int> commonResolutionList = new List<Vector2Int>
    {
        new Vector2Int(1280, 720),   // HD 720p
        new Vector2Int(1920, 1080) // FHD
    };

    int curIndex = 1;
    private void Start()
    {
        allRes = Screen.resolutions;

        List<Vector2Int> uniqueResList = new List<Vector2Int>();

        for(int i = 0; i < allRes.Length; i++) 
        {
            Vector2Int res = new Vector2Int(allRes[i].width, allRes[i].height);

            if(commonResolutionList.Contains(res) && !uniqueResList.Contains(res)) 
            {
                uniqueResList.Add(res);
                
                filterResList.Add(allRes[i]);
            }
        }
    }

    public void Init(int _width, int _height)
    {
        Screen.SetResolution
            (_width, _height, Screen.fullScreen);

        resolutionText.text = _width + " X " + _height;
    }

    public void SetResolution()
    {
        curIndex++;

        if (curIndex >= filterResList.Count)
        {
            curIndex = 0;
        }

        Resolution res = filterResList[curIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

        resolutionText.text = res.width + " X " + res.height;
    }

    public void ReapplyCurrentResolution()
    {
        if (filterResList.Count == 0) return;

        Resolution res = filterResList[curIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
