using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToastUI : MonoBehaviour
{
    [SerializeField] CanvasGroup underToastPanel;
    [SerializeField] Image underToastImg;
    [SerializeField] TextMeshProUGUI underToastText;

    [SerializeField] CanvasGroup topToastPanel;
    [SerializeField] TextMeshProUGUI topToastText;
 
    Coroutine underToastCoroutine;
    Coroutine topToastCoroutine;

    public void ShowUnderToastUI(ItemData _data)
    {
        if (underToastCoroutine != null)
        {
            StopCoroutine(underToastCoroutine);
            underToastCoroutine = null;
        }

        underToastPanel.DOKill();

        underToastPanel.alpha = 0f;
        underToastImg.sprite = _data.itemImg;
        underToastText.text = _data.itemName;

        underToastCoroutine = StartCoroutine(StartShowUnderToast(_data.itemImg, _data.itemName));
    }

    IEnumerator StartShowUnderToast(Sprite _img, string _text)
    {
        underToastPanel.gameObject.SetActive(true);

        underToastPanel.DOFade(1, 0.5f);

        yield return new WaitForSeconds(5f);

        yield return underToastPanel.DOFade(0, 0.5f);

        underToastPanel.gameObject.SetActive(false);
        underToastPanel.alpha = 0f;

        underToastCoroutine = null;
    }

    public void ShowTopToastUI(QuestData _data)
    {
        string text = $"{_data.questCondition.curCount} / {_data.questCondition.needCount}";

        if (topToastCoroutine != null)
        {
            StopCoroutine(topToastCoroutine);
            topToastCoroutine = null;
        }

        topToastPanel.DOKill();

        topToastCoroutine = StartCoroutine(StartShowTopToast(text));
    }

    IEnumerator StartShowTopToast(string _text)
    {
        topToastPanel.alpha = 0f;
        topToastText.text = _text;
        topToastPanel.gameObject.SetActive(true);

        topToastPanel.DOFade(1, 0.5f);

        yield return new WaitForSeconds(5f);

        yield return topToastPanel.DOFade(0, 0.5f);

        topToastPanel.gameObject.SetActive(false);
        topToastPanel.alpha = 0f;

        topToastCoroutine = null;
    }
}
