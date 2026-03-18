using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EScreenMode
{
    FULLSCREEN,
    WINDOW
}

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] ResolutionSetting resolutionSetting;
    [SerializeField] TextMeshProUGUI resolutionText;

    int curIndex = 0;

    public void Init(EScreenMode _eScreenMode)
    {
        if (_eScreenMode == EScreenMode.FULLSCREEN)
        {
            Screen.fullScreen = true;

            resolutionText.text = "ÄÑÁü";
        }
        else
        {
            Screen.fullScreen = false;

            resolutionText.text = "²¨Áü";
        }
    }

    public void SetScreenMode()
    {
        curIndex++;

        if (curIndex > 1)
        {
            curIndex = 0;
        }

        if (curIndex == 0)
        {
            Screen.fullScreen = true;

            resolutionText.text = "ÄÑÁü";
        }
        else if (curIndex == 1)
        {
            Screen.fullScreen = false;

            resolutionText.text = "²¨Áü";
        }

        StartCoroutine(ApplyResolutionAfterFullscreenChange());
    }

    IEnumerator ApplyResolutionAfterFullscreenChange()
    {
        yield return null;
        resolutionSetting.ReapplyCurrentResolution();
    }
}

