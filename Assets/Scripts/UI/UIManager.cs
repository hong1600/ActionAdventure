using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    FadeUI fade;
    PanelUI panel;

    protected override void Awake()
    {
        base.Awake();

        fade = GetComponent<FadeUI>();
        panel = GetComponent<PanelUI>();
    }

    public FadeUI Fade { get { return fade; } }
    public PanelUI Panel { get {  return panel; } }
}
