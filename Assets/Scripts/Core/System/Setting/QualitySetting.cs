using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI QualityText;

    List<string> qualOptionList = new List<string>()
    {
        "낮음", "중간", "높음", "매우높음"
    };

    int curIndex;

    public void Init(int _index)
    {
        QualitySettings.SetQualityLevel(_index);

        QualityText.text = qualOptionList[_index];
    }

    public void SetQuality()
    {
        curIndex++;

        if(curIndex >= qualOptionList.Count) 
        {
            curIndex = 0;
        }

        QualitySettings.SetQualityLevel(curIndex);

        QualityText.text = qualOptionList[curIndex];
    }
}
