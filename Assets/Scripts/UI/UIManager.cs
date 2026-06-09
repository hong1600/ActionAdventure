using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] PanelUI systemPanel;
    [SerializeField] PanelUI gamePanel;

    protected override void Awake()
    {
        base.Awake();
    }

    public PanelUI SystemPanel { get {  return systemPanel; } }
    public PanelUI GamePanel { get {  return gamePanel; } }
}
