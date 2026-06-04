using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] PanelUI panel;

    protected override void Awake()
    {
        base.Awake();
    }

    public PanelUI Panel { get {  return panel; } }
}
