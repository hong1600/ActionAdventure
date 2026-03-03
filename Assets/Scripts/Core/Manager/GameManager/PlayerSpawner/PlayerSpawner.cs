using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public event Action onSpawnEvent;

    [SerializeField] List<Transform> checkPointList;
    [SerializeField] GameObject playerPrefab;

    [SerializeField] CinemachineVirtualCamera virtualCam;

    public GameObject playerObj { get; private set; }

    public void Spawn(int _checkPoint)
    {
        SpawnPlayer(checkPointList[_checkPoint].position);
    }

    private void SpawnPlayer(Vector3 _spawnPos)
    {
        GameObject go = Instantiate(playerPrefab, _spawnPos, Quaternion.identity); 

        playerObj = go;

        onSpawnEvent?.Invoke();

        virtualCam.m_Follow = playerObj.transform;
        virtualCam.m_LookAt = playerObj.transform;
    }
}
