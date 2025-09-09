using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    PlayerStatus playerStatus;

    [SerializeField] List<Image> hpImg = new List<Image>();

    private void Start()
    {
        playerStatus = GameManager.instance.PlayerSpawner.playerObj.GetComponent<PlayerStatus>();

        playerStatus.onHpEvent += UpdateHp;
    }


    private void UpdateHp(int _curHp, int _maxHp)
    {
        for(int i = 0; i < hpImg.Count; i++) 
        {
            hpImg[i].enabled = i < _curHp;
        }
    }
}
