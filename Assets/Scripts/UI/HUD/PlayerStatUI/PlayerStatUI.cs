using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    PlayerStatus playerStatus;

    [SerializeField] GameObject statPanel;
    [SerializeField] List<Image> hpImg = new List<Image>();

    [SerializeField] Image mpImg;
    Material mpMat;

    private void Start()
    {
        GameManager.instance.PlayerSpawner.onSpawnEvent += Init;

        mpMat = mpImg.material;

        UpdateMp(0, 10);
    }

    private void Init()
    {
        playerStatus = GameManager.instance.PlayerSpawner.playerObj.GetComponent<PlayerStatus>();

        playerStatus.onHpEvent += UpdateHp;
        playerStatus.onMpEvent += UpdateMp;
    }

    private void UpdateHp(int _curHp, int _maxHp)
    {
        for(int i = 0; i < hpImg.Count; i++) 
        {
            hpImg[i].enabled = i < _curHp;
        }
    }

    private void UpdateMp(int _curMp, int _maxMp)
    {
        if (mpMat == null) return;

        float ratio = (float)_curMp / _maxMp;

        mpMat.SetFloat("_Fill", ratio);
    }

    public void ShowStatUI()
    {
        statPanel.transform.DOScale(Vector3.one, 0.3f).SetUpdate(true);
    }

    public void HideStatUI()
    {
        statPanel.transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true);
    }
}
